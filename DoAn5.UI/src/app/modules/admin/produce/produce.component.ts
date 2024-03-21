import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Paging, Produces } from 'src/app/model';
import { ProducesService } from 'src/app/service/produces.service';

@Component({
  selector: 'app-produce',
  templateUrl: './produce.component.html',
  styleUrls: ['./produce.component.css']
})
export class ProduceComponent {
  formProduce!: FormGroup
  paging !: Paging
  keyword: string = '';
  ListProduces: Produces[] = [];
  Totalcount!: number;
  loading: boolean = true;
  visible: boolean = false;
  Titile: string = '';
  showSub!: boolean;
  Getid!: string;
  uploadedFiles: any[] = [];
  selectedFile!: File | null;
  formUpload!: FormGroup;
  formData: FormData = new FormData();
  constructor(private ProduceSV: ProducesService, private FB: FormBuilder, private MessageSV: MessageService,
    private confirmationService: ConfirmationService) { }

  ngOnInit() {

    this.formProduce = this.FB.group({
      name: new FormControl('', Validators.required),
      address: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      image: new FormControl('string'),
    })
    this.formUpload = this.FB.group({
      id: [''],
      img: [''],
      image: [''],
    })
  }
  onsubmit = () => {
    this.loading = true;
    this.paging.keyword = this.keyword;
    this.ProduceSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProduces = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });

  }

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.keyword,
    };
    this.ProduceSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProduces = res.data;
        this.Totalcount = res.totalFilter;
        console.log(this.ListProduces)

      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };

  Add() {
    this.formProduce.reset();
    this.visible = true;
    this.Titile = "Thêm mới";
    this.showSub = true;

  }
  Edit(data: any) {
    this.visible = true;
    this.Titile = "Sửa";
    this.showSub = false;
    this.formProduce.controls['name'].setValue(data.name);
    this.formProduce.controls['address'].setValue(data.address);
    this.formProduce.controls['email'].setValue(data.email);
    this.formProduce.controls['phone'].setValue(data.phone);
    this.formProduce.controls['image'].setValue(data.image);
    this.Getid = data.id
  }
  SubmitBtn() {
    if (this.showSub != null) {
      this.showSub == true ? this.SaveAdd() : this.SaveEdit()
    }
  }

  close() {
    this.visible = false;
    this.uploadedFiles = [];
  }
  onUpload(event: any): void {
    const files = event.files;
    if (files.length > 0) {
      this.selectedFile = files[0]; // Gán giá trị của file vào biến file
      for (let file of files) {
        this.uploadedFiles.push(file);
      }
    }
  }

  SaveAdd() {
    if (this.formProduce && this.selectedFile != undefined) {
      this.formProduce.controls['image'].setValue("string");
      this.ProduceSV.create(this.formProduce.value).subscribe({
        next: res => {
          if (res) {
            const updatedData = this.formUpload.value;
            this.formData = new FormData();
            this.formData.append('id', res.id);
            this.formData.append('img', this.selectedFile || '')
            this.formData.append('image', updatedData.image);
            this.ProduceSV.Upload(this.formData).subscribe({
              next: res => {
                if (res) {
                  this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' })
                  this.formProduce.reset();
                  this.selectedFile= null;
                  this.uploadedFiles = [];
                  this.visible = false;
                  this.onsubmit()
                }
              }
            })

          }
        }
      })
    }
    else {
      this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Vui lòng điền đủ thông tin và Upload Ảnh' })
    }
  }
  SaveEdit() {
    if (this.formProduce && this.selectedFile != undefined) {
      this.formProduce.value["id"] = this.Getid
      this.ProduceSV.Update(this.formProduce.value).subscribe({
        next: res => {
          if (res) {
            const updatedData = this.formUpload.value;
            this.formData = new FormData();
            this.formData.append('id', this.Getid);
            this.formData.append('img', this.selectedFile || '')
            this.formData.append('image', updatedData.image);
            this.ProduceSV.Upload(this.formData).subscribe({
              next: res => {
                if (res) {
                  this.MessageSV.add({ severity: 'warn', summary: 'Waning', detail: 'Sửa thành công' })
                  this.visible = false;
                  this.uploadedFiles = [];
                  this.selectedFile= null;
                  this.onsubmit()
                }
              }
            })

          }
        }
      })
    }
    else {
      this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Vui lòng điền đủ thông tin và Upload Ảnh' })
    }
  }
  confirm(event: Event, data: string) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `Bạn có muốn xóa mã ${data}?`,
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-sm',
      accept: () => {
        if (data) {
          this.ProduceSV.Delete(data).subscribe({
            next: res => {
              if (res) {
                this.MessageSV.add({ severity: 'error', summary: 'Error', detail: 'Xóa thành công' })
                this.onsubmit();
              }
            }
          })
        }
      },
      reject: () => {
        this.MessageSV.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected', life: 3000 });
      }
    });
  }
}

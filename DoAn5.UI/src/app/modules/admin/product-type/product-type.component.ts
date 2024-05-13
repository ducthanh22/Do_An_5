import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Paging } from 'src/app/model';
import { Product_typeDto } from 'src/app/model/Product_type';
import { CategoriesService } from 'src/app/service';
import { Product_TypeService } from 'src/app/service/Product_type.service';

@Component({
  selector: 'app-product-type',
  templateUrl: './product-type.component.html',
  styleUrls: ['./product-type.component.css']
})
export class ProductTypeComponent {
  Dscategories: any[] = [];
  loading: boolean = true;
  paging!: Paging;
  Titile: string = ''
  keyword: string = '';
  Categories!: Product_typeDto[];
  datas: Product_typeDto[] = [];
  Totalcount!: number;
  visible: boolean = false;
  FormCategories!: FormGroup;
  messageService: any;
  rowHover: any;
  showSub!: boolean;
  Getid!: number;
  uploadedFiles: any[] = [];
  selectedFile!: File | null;
  formData: FormData = new FormData();


  constructor(
    private Product_typeService: Product_TypeService,
    private fb: FormBuilder, private MessageSV: MessageService,private confirmationService:ConfirmationService,private CategoryService:CategoriesService) {
  }

  ngOnInit() {
    this.LoadCategories();

    this.FormCategories = this.fb.group({
      name: new FormControl('', Validators.required),
      idcategories: new FormControl('', Validators.required)
    });

  }
  LoadCategories() {
    this.CategoryService.getAll().subscribe((data) => {
      this.Dscategories = data
    })
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
    this.Product_typeService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.Totalcount = res.totalFilter;
        console.log(this.datas)
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };
  onsubmit () {
    this.paging.keyword = this.keyword;
    this.Product_typeService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.Totalcount = res.totalFilter;
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
    this.FormCategories.reset();
    this.visible = true;
    this.Titile = "Thêm mới";
    this.showSub = true;

  }
  SubmitBtn() {
    if (this.showSub != null) {
      this.showSub == true ? this.SaveAdd() : this.SaveEdit()
    }
  }
  Edit(data: any) {
    this.visible = true;
    this.Titile = "Sửa";
    this.showSub = false;
    this.FormCategories.controls['name'].setValue(data.name);
    this.FormCategories.controls['idcategories'].setValue(data.idcategories);

    this.Getid = data.id
  }

  close() {
    this.visible = false;
    this.uploadedFiles = [];
  }
  

  SaveAdd() {
    if (this.FormCategories) {
      this.Product_typeService.create(this.FormCategories.value).subscribe({
        next: res => {
          if (res) {
            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' })
            this.FormCategories.reset();
            this.visible = false;
            this.onsubmit()
          }
        }
      })
    }
  }
  SaveEdit() {
    if (this.FormCategories) {
      this.FormCategories.value["id"] = this.Getid
      this.Product_typeService.Update(this.FormCategories.value).subscribe({
        next: res => {
          if (res) {
            this.MessageSV.add({ severity: 'warn', summary: 'Waning', detail: 'Sửa thành công' })
            this.FormCategories.reset();
            this.visible = false;
            this.onsubmit()
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
          this.Product_typeService.Delete(data).subscribe({
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


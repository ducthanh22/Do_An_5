import { Component, ViewChild } from '@angular/core';
import { CategoriesDto, Paging } from 'src/app/model';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CategoriesService } from 'src/app/service/categories.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';






@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent {
  Dscategories: any[] = [];
  loading: boolean = true;
  paging!: Paging;
  Titile: string = ''
  keyword: string = '';
  Categories!: CategoriesDto[];
  datas: CategoriesDto[] = [];
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
    private categoriesService: CategoriesService,
    private fb: FormBuilder, private MessageSV: MessageService,private confirmationService:ConfirmationService, private route:Router) {
  }

  ngOnInit() {
    this.LoadCategories();
    this.FormCategories = this.fb.group({
      name: new FormControl('', Validators.required),
    });

  }
  LoadCategories() {
    this.categoriesService.getAll().subscribe((data) => {
      this.Dscategories = data
    },
  )
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
    this.categoriesService.Search(this.paging).subscribe({
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
    this.categoriesService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 401) {
         this.route.navigate(['/admin/unauthorized'])
        } else if (error.status === 403) {
         this.route.navigate(['/admin/unauthorized'])
        } else {
         this.route.navigate(['/admin/unauthorized'])
        }
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
    this.Getid = data.id
  }

  close() {
    this.visible = false;
    this.uploadedFiles = [];
  }
  

  SaveAdd() {
    if (this.FormCategories) {
      this.categoriesService.create(this.FormCategories.value).subscribe({
        next: (res) => {
          if (res) {
            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' })
            this.FormCategories.reset();
            this.visible = false;
            this.onsubmit()
          }
        },
        error:(error: HttpErrorResponse) => {
          debugger
          if (error.status === 401) {
           this.route.navigate(['/admin/unauthorized'])
          } else if (error.status === 403) {
           this.route.navigate(['/admin/unauthorized'])
          } else {
           this.route.navigate(['/admin/unauthorized'])
          }
        }
      }
    )
    }
  }
  SaveEdit() {
    if (this.FormCategories) {
      this.FormCategories.value["id"] = this.Getid
      this.categoriesService.Update(this.FormCategories.value).subscribe({
        next: res => {
          if (res) {
            this.MessageSV.add({ severity: 'warn', summary: 'Waning', detail: 'Sửa thành công' })
            this.FormCategories.reset();
            this.visible = false;
            this.onsubmit()
          }
        },
        error:(error: HttpErrorResponse) => {
          debugger
          if (error.status === 401) {
           this.route.navigate(['/admin/unauthorized'])
          } else if (error.status === 403) {
           this.route.navigate(['/admin/unauthorized'])
          } else {
           this.route.navigate(['/admin/unauthorized'])
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
          this.categoriesService.Delete(data).subscribe({
            next: res => {
              if (res) {
                this.MessageSV.add({ severity: 'error', summary: 'Error', detail: 'Xóa thành công' })
                this.onsubmit();
              }
            },
            error:(error: HttpErrorResponse) => {
              debugger
              if (error.status === 401) {
               this.route.navigate(['/admin/unauthorized'])
              } else if (error.status === 403) {
               this.route.navigate(['/admin/unauthorized'])
              } else {
               this.route.navigate(['/admin/unauthorized'])
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

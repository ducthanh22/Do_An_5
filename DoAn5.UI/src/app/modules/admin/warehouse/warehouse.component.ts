import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Paging, ProductsDto, SizeDto } from 'src/app/model';
import { GetDetail_warehouseDto } from 'src/app/model/warehouse';
import { WarehousedetailService } from 'src/app/service/warehouse_detail.service';
import { SizeService } from 'src/app/service/size.service';
import { ProductsService } from 'src/app/service/products.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrl: './warehouse.component.css'
})
export class WarehouseComponent {
  loading:boolean=false;
  paging:Paging={keyword:'',pageIndex:1,pageSize:10};
  keyword:string='';
  datas!:GetDetail_warehouseDto[];
  Totalcount!:number;
  visible:boolean=false;
  Titile!:string;
  formWarehouse!:FormGroup;
  Getid!:string;
  listProduct!:ProductsDto[];
  listSize!:SizeDto[];
  idware:string='';
  setdisable:boolean=false;
  constructor(private MessageSV: MessageService,private confirmationService:ConfirmationService, private warehouseService: WarehousedetailService,
    private fb :FormBuilder, private productService:ProductsService, private sizeService: SizeService, private route:Router
  ) {
  }

  ngOnInit() {
    this.formWarehouse=this.fb.group({
      Idproduct: new FormControl('',Validators.required),
      Idsize: new FormControl('',Validators.required),
      Quantity: new FormControl('',Validators.required)
    })
    this.getProduct();

  }

  getProduct(){
this.productService.getAll().subscribe(data=>{
  this.listProduct=data
})
  }
  getSize(id:string){
    this.sizeService.Getbyidproduct(id).subscribe(data=>{
      this.listSize=data;
    },
    (error: HttpErrorResponse) => {
      if (error.status === 401) {
       this.route.navigate(['/admin/unauthorized'])
      } else if (error.status === 403) {
       this.route.navigate(['/admin/unauthorized'])
      } else {
       this.route.navigate(['/admin/unauthorized'])
      }
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
    this.warehouseService.Search(this.paging).subscribe({
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
    this.warehouseService.Search(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.Totalcount = res.totalFilter;
      },
      error:(error: HttpErrorResponse) => {
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



  Edit(data: any) {
    this.visible = true;
    this.setdisable=false;
    this.Titile = "Sửa";
    this.formWarehouse.controls['Idproduct'].setValue(data.idproduct);
    this.formWarehouse.controls['Idsize'].setValue(data.idsize);
    this.formWarehouse.controls['Quantity'].setValue(data.quantity);
    this.Getid = data.id
    this.idware=data.idwarehouse
    this.getSize(data.idproduct)
  }
  view(data: any) {
    this.visible = true;
    this.setdisable=true;
    this.Titile = "Xem";
    this.formWarehouse.controls['Idproduct'].setValue(data.idproduct);
    this.formWarehouse.controls['Idsize'].setValue(data.idsize);
    this.formWarehouse.controls['Quantity'].setValue(data.quantity);
    this.getSize(data.idproduct)
  }

  close() {
    this.visible = false;

  }
  
  SaveEdit() {
    if (this.formWarehouse) {
      this.formWarehouse.value["id"] = this.Getid;
      this.formWarehouse.value["idwarehouse"] = this.idware;
      this.warehouseService.Update(this.formWarehouse.value).subscribe({
        next: res => {
          if (res) {
            this.MessageSV.add({ severity: 'warn', summary: 'Waning', detail: 'Sửa thành công' })
            this.formWarehouse.reset();
            this.visible = false;
            this.onsubmit()
          }
        },
        error:(error: HttpErrorResponse) => {
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
          this.warehouseService.Delete(data).subscribe({
            next: res => {
              if (res) {
                this.MessageSV.add({ severity: 'error', summary: 'Error', detail: 'Xóa thành công' })
                this.onsubmit();
              }
            },
            error:(error: HttpErrorResponse) => {
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

import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, ChangeDetectorRef, Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Paging, ProductsDto, SizeDto } from 'src/app/model';
import { CreateImportbillDto } from 'src/app/model/importBill';
import { importBillService } from 'src/app/service/Importbill.service';
import { AccountService } from 'src/app/service/account.service';
import { ProductsService } from 'src/app/service/products.service';
import { SizeService } from 'src/app/service/size.service';

@Component({
  selector: 'app-import-bill',
  templateUrl: './import-bill.component.html',
  styleUrls: ['./import-bill.component.css']
})
export class ImportBillComponent implements AfterViewInit {

  loading: boolean = false;
  paging: Paging = { keyword: "", pageIndex: 1, pageSize: 10 };
  keyword: string = ""
  listExportBill: CreateImportbillDto[] = [];
  Totalcount!: number;
  visible: boolean = false;
  formImport!:FormGroup;
  listProduct:ProductsDto[]=[];
  infomation:any
  Titile:string='';
  listSize:SizeDto[]=[];
  priceProduct!:number;
  total!:number;
  dataImport!:CreateImportbillDto;
  // listDetail: GetDetail_exportbillDto[] = [];
  constructor (private importbillService:importBillService,private confirmationService:ConfirmationService, private MessageSV: MessageService,
    private fb :FormBuilder,private productService:ProductsService,private accountService:AccountService, private sizeService:SizeService,
    private cd:ChangeDetectorRef, private route: Router
    
  ){}
  ngOnInit(){
    this.infomation= this.accountService.decodeToken();
    this.onsubmit();
    this.formImport=this.fb.group({
      detail_importbill: this.fb.array([])
    })
  }
  ngAfterViewInit(): void {
    this.cd.detectChanges();
  }

  get detail_importbill(): FormArray {
    return this.formImport.get('detail_importbill') as FormArray;
  }
  resetListSize() {
    this.detail_importbill.clear();
  }
  addSize() {
    const formdetail = this.fb.group({
    // idExportbill: new FormControl(''),
    idproduct:new FormControl('',Validators.required),
    idsize: new FormControl('',Validators.required),
    price: new FormControl('',Validators.required),
    quantity: new FormControl('',Validators.required),
    total: new FormControl({value:'',disabled:true}),
    });
    this.detail_importbill.push(formdetail);
  }
  removeSize(index: number) {
    if (this.detail_importbill.length > 1) {
      this.detail_importbill.removeAt(index);
    }

  }
  close() {
    this.visible = false;
    this.formImport.reset();
    this.resetListSize();
    this.onsubmit();
  }
  onsubmit = () => {
    this.paging.keyword = this.keyword;
    this.importbillService.Search(this.paging).subscribe({
      next: (res) => {
        this.listExportBill = res.data;
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
    this.importbillService.Search(this.paging).subscribe({
      next: (res) => {
        this.listExportBill = res.data;
        this.Totalcount = res.totalFilter;
      },
      // error: (e) => {
      //   this.loading = false;
      // },
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

  getProduct(){
    this.productService.getAll().subscribe(data=>{
      this.listProduct=data;
    })
  }
  onChange(event: any, index: number) {
    const quantityValue = event.target.value;
    this.total =this.priceProduct * quantityValue;
    const valueIndex = this.detail_importbill.at(index);
    if (valueIndex && valueIndex.get('total')) {
      valueIndex.get('total')?.setValue(this.total);
    }
  }
  getTotalPrice(): number {
    let total: number = 0;
    this.detail_importbill.controls.forEach((item: any) => {
      total += item.controls.price.value * item.controls.quantity.value;
    });
    return total;
  }
  updatePrice(event: any, index: number) {
    const selectedProductId = event.value;
    const detail = this.detail_importbill.at(index) as FormGroup;

    // Lưu trữ giá trị đã chọn của listSize
    const selectedSizeId = detail.get('idsize')?.value;

    this.getSize(selectedProductId, index, selectedSizeId);
    const selectedProduct = this.listProduct.find(product => product.id === selectedProductId);
    this.priceProduct = selectedProduct ? selectedProduct.price_product : 0;

    if (detail) {
      if (detail.get('price')) {
        detail.get('price')?.setValue(this.priceProduct);
      }
      if (detail.get('quantity')) {
        detail.get('total')?.setValue(this.priceProduct * detail.value.quantity);
      }
    }
  }

  getSize(productId: string, index: number, selectedSizeId?: string) {
    this.sizeService.Getbyidproduct(productId).subscribe(data => {
      // Cập nhật listSize riêng cho từng phần tử trong detail_importbill
      (this.detail_importbill.at(index) as FormGroup).addControl('listSize', this.fb.control(data));
      // Khôi phục giá trị đã chọn của listSize
      if (selectedSizeId) {
        this.restoreSelectedSize(index, selectedSizeId);
      }
    });
  }

  restoreSelectedSize(index: number, selectedSizeId: string) {
    // Khôi phục giá trị đã chọn cho formControlName 'idsize'
    const detail = this.detail_importbill.at(index) as FormGroup;
    if (detail) {
      detail.get('idsize')?.setValue(selectedSizeId);
    }
  }
  
  // GetDetail(id :string){
  //   this.visible= true;
  //   this.importbillService.GetDetail(id).subscribe({
  //     next:(res)=>{
  //       if(res){
  //         this.listDetail=res;
  //       }
  //     }
  
  //   })
  // }
  Add(){
    this.Titile="Thêm hóa đơn nhập";
    this.getProduct();
    this.visible=true;

  }
  Save(){
    this.dataImport={
      price: Number(this.getTotalPrice()),
      status: 1,
      idStaff: this.infomation.Id,
      detail_importbill: this.formImport.value.detail_importbill
    }
    this.importbillService.create(this.dataImport).subscribe({
      next:(res)=>{
        if(res){
          this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' });
          this.visible=false;
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
  confirm(event: Event, data: string) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `Bạn có muốn xóa mã ${data}?`,
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-sm',
      accept: () => {
        if (data) {
          this.importbillService.Delete(data).subscribe({
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

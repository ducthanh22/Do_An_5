import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { format } from 'date-fns';
import { MessageService } from 'primeng/api';
import { ProductsDto } from 'src/app/model';
import { GetSaleDto } from 'src/app/model/sale';
import { ProductsService } from 'src/app/service/products.service';
import { SaleService } from 'src/app/service/sale.service';

@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.css']
})
export class SaleComponent {
  keyword: string = '';
  active: number = 1;
  listSale: GetSaleDto[] = [];
  loading: boolean = false;
  newkeyword: string = '';
  stateOptions: any[] = [{ label: 'Hoạt động', active: 1 },{ label: 'Sắp diễn ra', active: 2}, { label: 'Kết thúc', active: 0 }];
  visible: boolean = false;
  Titile!: string;
  formdata!: FormGroup;
  datetime24h!: Date;
  listProduct: ProductsDto[] = [];
  priceProduct: number = 0;
  percentValue: number = 0;
  saleTime!: number;
  minDate!:Date;
  datasale!:number;

  constructor(private saleService: SaleService, private fb: FormBuilder, private productService: ProductsService,
    private MessageSV:MessageService) { }
  ngOnInit() {
    this.startUpdateSalesPrices()
    this.onsubmit();
    this.formdata = this.fb.group({
      listsale: this.fb.array([])
    });
    this.minDate =new Date;
  }
  get listsale(): FormArray {
    return this.formdata.get('listsale') as FormArray;
  }
  resetListSize() {
    this.listsale.clear();
  }
  addSize() {
    const formSale = this.fb.group({
      idProduct: new FormControl("", Validators.required),
      salePrice: new FormControl("", Validators.required),
      percent: new FormControl("", Validators.required),
      saleTime: new FormControl("", Validators.required),
      created: new FormControl("", Validators.required),
      priceProduct: new FormControl("", Validators.required),
    });
    this.listsale.push(formSale);
  }
  removeSize(index: number) {
    if (this.listsale.length > 1) {
      this.listsale.removeAt(index);
    }

  }
  close() {
    this.visible = false;
    this.formdata.reset();
    this.priceProduct = 0;
    this.percentValue = 0;
    this.resetListSize();
    this.onsubmit();
  }
  updatePrice(event: any, index: number) {
    const selectedProductId = event.value;
    const selectedProduct = this.listProduct.find(product => product.id === selectedProductId);
    selectedProduct != null ? this.priceProduct = selectedProduct.price_product : this.priceProduct = 0
    const formSale = this.listsale.at(index);
    if (formSale && formSale.get('priceProduct')) {
      formSale.get('priceProduct')?.setValue(this.priceProduct);
    }
  }
  onPercentChange(event: any, index: number) {
    const percentValue = event.target.value;
    this.percentValue =this.priceProduct- (this.priceProduct * (percentValue / 100));
    const formSale = this.listsale.at(index);
    if (formSale && formSale.get('salePrice')) {
      formSale.get('salePrice')?.setValue(this.percentValue);
    }
  }

  startUpdateSalesPrices(){
    this.saleService.UpdateSalesPrices().subscribe(data => { // Gọi phương thức UpdateSalesPrices
      if (data) {
        this.datasale = data;
        this.onsubmit();
      } 
    });
  
  }

  getProduct() {
    this.productService.getAll().subscribe(data => {
      this.listProduct = data;
      console.log(this.listProduct)
    })
  }
  onsubmit() {
    this.startUpdateSalesPrices();
    this.keyword = this.newkeyword;
    this.saleService.getSale(this.keyword, this.active).subscribe({
      next: (res) => {
        this.listSale = res
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  }
  Add() {
    this.Titile = "Thêm giảm giá"
    this.visible = true;
    this.getProduct()
  }
  Save(){
    if(this.formdata){
      const data=this.formdata.value.listsale
      data.forEach((x:any)=>{
        x.activeFlag=1;
        x.createdBy= null;
        x.modifiedBy= null;
        x.modified= null;
        x.created= format(this.datetime24h, "yyyy-MM-dd'T'HH:mm:ss.SSS");
        x.saleTime= this.saleTime;
      })
      this.saleService.create(data).subscribe({
        next:(res)=>{
          if(res){
            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' });
            this.close();
            this.onsubmit();
          }
        }
      })
    }
  }
}

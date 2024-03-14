import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CategoriesDto, Paging, Produces, ProductsDto } from 'src/app/model';
import { CategoriesService } from 'src/app/service';
import { ProducesService } from 'src/app/service/produces.service';
import { ProductsService } from 'src/app/service/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  loading: boolean=false;
  customers: any;
  keyword: string='' ;
  visible: boolean = false;
  ListProducts!: ProductsDto[];
  paging: Paging={keyword:"",pageIndex:1,pageSize:10};
  Totalcount!: number;
  FormProduct!:FormGroup;
  Titile!: string;
  showSub!:boolean;
  SelectionCategory!:CategoriesDto[]
  SelectionProduces!:Produces[]

  constructor(private ProductSV: ProductsService, private FB : FormBuilder,private MessageSV: MessageService,
    private CategorySV:CategoriesService, private changeDetector: ChangeDetectorRef,
    private ProducesSv:ProducesService) { }
  ngOnInit() { 
    
    this.FormProduct= this.FB.group({
      // id : new FormControl(""),
      name:new FormControl ("",Validators.required),
      idcategories:new FormControl ("",Validators.required),
      idproduces: new FormControl ("",Validators.required),
      describe: new FormControl ("",Validators.required),
      price_product:new FormControl ("",Validators.required),
      nameSize:new FormControl ("",Validators.required),
      image:new FormControl ("",Validators.required),

    })
  }
  ngAfterContentChecked(): void {
    this.changeDetector.detectChanges();
  }
  GetCategory(){
    this.CategorySV.getAll().subscribe(data=>{
      this.SelectionCategory=data;
    })
  }
  Getproduces(){
    this.ProducesSv.getAll().subscribe(data=>{
      this.SelectionProduces=data;
    })
  }

  onsubmit=()=>{
    this.paging.keyword = this.keyword;
    this.ProductSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProducts = res.data;
        this.Totalcount = res.totalFilter;
        console.log(this.ListProducts)
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
    this.ProductSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProducts = res.data;
        this.Totalcount = res.totalFilter;
        console.log(this.ListProducts)
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
    this.FormProduct.reset();
    this.visible = true;
    this.Titile="Thêm mới";
    this.GetCategory();
    this.Getproduces();
    this.showSub= true;
    
  }
  Edit(data:any){
    this.visible = true;
    this.Titile="Sửa";
    this.showSub= false;
    this.GetCategory();
    this.Getproduces();
    this.FormProduct.controls['name'].setValue(data.name);
    this.FormProduct.controls['idcategories'].setValue(data.idcategories);
    this.FormProduct.controls['idproduces'].setValue(data.idproduces);
    this.FormProduct.controls['describe'].setValue(data.describe);
    this.FormProduct.controls['price_product'].setValue(data.price_product);

  }
  SubmitBtn(){
    if(this.showSub != null){
      this.showSub== true ? this.SaveAdd():this.SaveEdit()
    }
    
  }


  SaveAdd(){
    if(this.FormProduct){
      this.ProductSV.create(this.FormProduct.value).subscribe({
        next:res=>{
          if(res){
            this.MessageSV.add({ severity: 'Success', summary: 'Success', detail: 'Thêm thành công' })
            this.FormProduct.reset();
    this.visible = false;

          }
        }
      })
    }
  }
  SaveEdit(){
    if(this.FormProduct){
      this.ProductSV.create(this.FormProduct).subscribe({
        next:res=>{
          if(res){
            this.MessageSV.add({ severity: 'Success', summary: 'Success', detail: 'Thêm thành công' })
    this.visible = false;

          }
        }
      })
    }
  }
}
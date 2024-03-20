import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CategoriesDto, ColorDto, Paging, Produces, ProductsDto, SizeDto } from 'src/app/model';
import { CategoriesService } from 'src/app/service';
import { ColorService } from 'src/app/service/color.service';
import { ProducesService } from 'src/app/service/produces.service';
import { ProductsService } from 'src/app/service/products.service';
import { SizeService } from 'src/app/service/size.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  loading: boolean = false;
  customers: any;
  keyword: string = '';
  visible: boolean = false;
  ListProducts!: ProductsDto[];
  paging: Paging = { keyword: "", pageIndex: 1, pageSize: 10 };
  Totalcount!: number;
  FormProduct!: FormGroup;
  Titile!: string;
  showSub!: boolean;
  SelectionCategory!: CategoriesDto[]
  SelectionProduces!: Produces[]
  SelectColor!: ColorDto[]
  Getid!: string;
  formUpload!: FormGroup
  formData: FormData = new FormData();
  selectedFile!: File | null;
  uploadedFiles: any[] = [];
  SelectSize!:SizeDto[]
  constructor(private ProductSV: ProductsService, private FB: FormBuilder, private MessageSV: MessageService,
    private CategorySV: CategoriesService, private changeDetector: ChangeDetectorRef, private confirmationService: ConfirmationService,
    private ProducesSv: ProducesService, private ColorSV: ColorService, private SizeSV: SizeService) { }
  ngOnInit() {
    this.GetCategory();
    this.Getproduces(); this.Getcolor();
    this. GetSize();
    this.FormProduct = this.FB.group({
      name: new FormControl("", Validators.required),
      idcategories: new FormControl("", Validators.required),
      idproduces: new FormControl("", Validators.required),
      describe: new FormControl("", Validators.required),
      image: new FormControl("",),
      idcolor: new FormControl("", Validators.required),
      idsize: new FormControl(""),
      price_product: new FormControl("", Validators.required),

    })

    this.formUpload = this.FB.group({
      id: [''],
      img: [''],
      image: [''],
    })
  }
  ngAfterContentChecked(): void {
    this.changeDetector.detectChanges();
  }
  GetCategory() {
    this.CategorySV.getAll().subscribe(data => {
      this.SelectionCategory = data;
    })
  }
  Getproduces() {
    this.ProducesSv.getAll().subscribe(data => {
      this.SelectionProduces = data;
    })
  }
  Getcolor() {
    this.ColorSV.getAll().subscribe(data => {
      this.SelectColor = data;
    })
  }
  GetSize() {
    this.SizeSV.getAll().subscribe(data => {
      this.SelectSize = data;
      console.log( this.SelectSize )
    })
  }

  onsubmit = () => {
    this.paging.keyword = this.keyword;
    this.ProductSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProducts = res.data;
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
    this.ProductSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProducts = res.data;
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
    this.FormProduct.reset();
    this.visible = true;
    this.Titile = "Thêm mới";
    this.showSub = true;

  }
  Edit(data: any) {
    this.visible = true;
    this.Titile = "Sửa";
    this.showSub = false;
    this.FormProduct.controls['name'].setValue(data.name);
    this.FormProduct.controls['idcategories'].setValue(data.idcategories);
    this.FormProduct.controls['idproduces'].setValue(data.idproduces);
    this.FormProduct.controls['describe'].setValue(data.describe);
    this.FormProduct.controls['price_product'].setValue(data.price_product);
    this.FormProduct.controls['idcolor'].setValue(data.idcolor);
    this.FormProduct.controls['idsize'].setValue(data.idsize);
    // this.FormProduct.controls['image'].setValue(data.image);
    this.Getid = data.id
  }
  SubmitBtn() {
    if (this.showSub != null) {
      this.showSub == true ? this.SaveAdd() : this.SaveEdit()
    }
  }

  close(){
    this.visible=false;
    this.uploadedFiles=[];
  }
  onUpload(event: any): void {
    const files = event.files;
    if (files.length > 0) {
      this.selectedFile = files[0]; // Gán giá trị của file vào biến file
      for(let file of files) {
        this.uploadedFiles.push(file);
    }
    }
  }
  
  SaveAdd() {
    if (this.FormProduct&& this.selectedFile !=undefined) {
      this.FormProduct.controls['image'].setValue("string");
      this.ProductSV.create(this.FormProduct.value).subscribe({
        next: res => {
          if (res) {
            const updatedData = this.formUpload.value;
            this.formData = new FormData();
            this.formData.append('id', res.id);
            this.formData.append('img',  this.selectedFile || '')
            this.formData.append('image', updatedData.image);
            this.ProductSV.Upload(this.formData).subscribe({
              next: res => {
                if (res) {
                  this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' })
                  this.FormProduct.reset();
                  this.uploadedFiles=[];
                  this.visible = false;
                  this.onsubmit()
                }
              }
            })

          }
        }
      })
    }
    else{
      this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Vui lòng điền đủ thông tin và Upload Ảnh' })
    }
  }
  SaveEdit() {
    if (this.FormProduct && this.selectedFile !=undefined) {
      this.FormProduct.value["id"] = this.Getid
      this.ProductSV.Update(this.FormProduct.value).subscribe({
        next: res => {
          if (res) {
            const updatedData = this.formUpload.value;
            this.formData = new FormData();
            this.formData.append('id',this.Getid);
            this.formData.append('img',  this.selectedFile || '')
            this.formData.append('image', updatedData.image);
            this.ProductSV.Upload(this.formData).subscribe({
              next: res => {
                if (res) {
                  this.MessageSV.add({ severity: 'warn', summary: 'Waning', detail: 'Sửa thành công' })
                  this.visible = false;
                  this.uploadedFiles=[];
                  this.onsubmit()
                }
              }
            })
          
          }
        }
      })
    }
    else{
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
          this.ProductSV.Delete(data).subscribe({
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
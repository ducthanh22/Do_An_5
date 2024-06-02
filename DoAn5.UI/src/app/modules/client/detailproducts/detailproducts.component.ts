import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ProductsDto } from 'src/app/model';
import { countProduct } from 'src/app/model/exportBill';
import { GetRatingDto } from 'src/app/model/rating';
import { exportBillService } from 'src/app/service/Exportbill.service';
import { AccountService } from 'src/app/service/account.service';
import { ProductsService } from 'src/app/service/products.service';
import { RatingService } from 'src/app/service/rating.service';
import { WarehousedetailService } from 'src/app/service/warehouse_detail.service';


@Component({
  selector: 'app-detailproducts',
  templateUrl: './detailproducts.component.html',
  styleUrls: ['./detailproducts.component.css']
})
export class DetailproductsComponent {
  id!: string;
  data!: any
  Carts!: any[];
  Size!: any;
  value: number = 5;
  first: number = 0;
  rows: number = 5;
  page!:number;
  datas: GetRatingDto[] = [];
  Totalcount!: number;
  informationToken: any;
  countProduct!:countProduct;
  countprohouse!:countProduct;
  constructor(private route: ActivatedRoute, private productService: ProductsService, private MessageSV: MessageService,
    private RatingService: RatingService, private AcountService: AccountService, private exportBillService :exportBillService,
  private warehouseService : WarehousedetailService) { }
  ngOnInit() {
    this.informationToken = this.AcountService.decodeToken();
    this.Carts = this.productService.GetCart();
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.id = params['id'];
      }
      this.getbyid(this.id);
      this.GetRating(this.id,1,10)
      this.CountProduct(this.id)
      this.CountProWarehouse(this.id)
    });
    
  }
  CountProduct(id:string){
    this.exportBillService.countProduct(id).subscribe({
      next:(res)=>{
        if(res){
          this.countProduct=res;
        }
      }
    })
  }
  CountProWarehouse(id:string){
    this.warehouseService.CountProduct(id).subscribe({
      next:(res)=>{
        if(res){
          this.countprohouse=res;
        }
      }
    })
  }
  getbyid(id: string) {
    this.productService.getbyid(this.id).subscribe({
      next: (res) => {
        if (res) {
          this.data = res.reduce((acc: any, x: any) => {
            const kt = acc.find((y: any) => y.id === x.id);
            if (!kt) {
              acc.push(x);
            } else {
              if (kt.activeSale - x.activeSale < 1) {
                const index = acc.indexOf(kt);
                if (index !== -1) {
                  acc.splice(index, 1); // Loại bỏ phần tử tại vị trí index
                  acc.push(x);
                }
              }
            }
            return acc;
          }, []);
        }
      }
    })
  }
  selectSize(data: any) {
    this.Size = []
    this.Size = data;
  }
  addtocart(data: any) {
    if(data.activeSale == 1){
      data.price_product=data.salePrice
    }
    if (this.Size) {
      let idx = this.Carts.findIndex((item: any) => {
        return item.data.id == data.id && item.size.id == this.Size.id
      });
      if (idx >= 0) {
        this.Carts[idx].quantity += 1;
      } else {
        let cartItem: any = {
          data,
          size: this.Size,
          quantity: 1,
        };
        this.Carts.push(cartItem)
      }
      this.productService.saveCart(this.Carts)
      this.MessageSV.add({ severity: 'success', summary: 'Thành công', detail: 'Thêm giỏ hàng thành công' })
    }
    else {
      this.MessageSV.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Vui lòng chọn kích thước' })

    }
  }

  GetRating(id: string,page:number,pageSize:number) {
    this.RatingService.GetByProduct(id, page, pageSize).subscribe({
      next: (value) => { 
        if (value) {
          this.datas = value.data;
          this.Totalcount = value.totalFilter;
        }
      },
    });
  }
  

  onPageChange(event: any) {
    this.first = event.first;
    this.rows = event.rows;
    this.page = event.page;
    this. GetRating(this.id, this.page+1,this.rows) 
  }
}

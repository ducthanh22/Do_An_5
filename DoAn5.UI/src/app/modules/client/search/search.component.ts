import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Paging } from 'src/app/model/Paging';
import { ShareService } from 'src/app/service/Common/share.service';
import { ProductsService } from 'src/app/service/products.service';
import { WarehouseComponent } from '../../admin/warehouse/warehouse.component';
import { WarehousedetailService } from 'src/app/service/warehouse_detail.service';
import { exportBillService } from 'src/app/service/Exportbill.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  loading: boolean = true;
  paging: Paging = { keyword: "", pageIndex: 1, pageSize: 10 };
  ListProducts: any[] = [];
  Totalcount!: number;
  receivedKeyword: string = '';
  layout: 'grid' | 'list' = 'grid';
  data:any;
  visible:boolean=false;
  Size!: any;
  Carts:any;
  countprohouse:any;
  countProduct:any;
  constructor(private ProductSV: ProductsService, private ShareService: ShareService, private MessageSV: MessageService,
    private warehouseService: WarehousedetailService,private exportBillService:exportBillService){ }

  ngOnInit() {
    this.Carts = this.ProductSV.GetCart();

    this.ShareService.keyword$.subscribe(keyword => {
      if (this.receivedKeyword !== keyword) {
        this.receivedKeyword = keyword;
        this.loadData(this.receivedKeyword);
      }
    });
  }

  loadData(data: any) {
    this.paging.keyword = data;
    this.ProductSV.Search(this.paging).subscribe({
      next: (res) => {
        this.ListProducts = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (err) => {
        console.error('Error fetching products', err);
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = Math.floor(event.first / pageSize) + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.receivedKeyword,
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
  getbyid(id: string) {
    this.visible=true;
    this.ProductSV.getbyid(id).subscribe({
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
      this.ProductSV.saveCart(this.Carts)
      this.MessageSV.add({ severity: 'success', summary: 'Thành công', detail: 'Thêm giỏ hàng thành công' })
      this.visible=false
    }
    else {
      this.visible=false;
      this.MessageSV.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Vui lòng chọn kích thước' })

    }
  }
  selectSize(id:string,data: any) {
    this.Size = []
    this.Size = data;
    this.CountProWarehouse(id,data.id);
    this.countProduct(id);
  }
  CountProWarehouse(id:string,idSize:string){
    this.warehouseService.CountProduct(id,idSize).subscribe({
      next:(res)=>{
        if(res){
          this.countprohouse=res;
        }
      }
    })
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
}

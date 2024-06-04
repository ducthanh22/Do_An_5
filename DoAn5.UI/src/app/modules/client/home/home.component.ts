import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Subscription, interval } from 'rxjs';
import { GetProductsDto, ProductsDto, bestSellingProducts } from 'src/app/model';
import { ShareService } from 'src/app/service/Common/share.service';
import { exportBillService } from 'src/app/service/Exportbill.service';
import { AccountService } from 'src/app/service/account.service';
import { ProducesService } from 'src/app/service/produces.service';
import { ProductsService } from 'src/app/service/products.service';
import { SaleService } from 'src/app/service/sale.service';
import { WarehousedetailService } from 'src/app/service/warehouse_detail.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  responsiveOptions: any[] | undefined;
  ListProduces: any[] = [];
  products: GetProductsDto[] = [];
  productsSale: GetProductsDto[] = [];
  Carts!: any[];

  layout: 'grid' | 'list' = 'grid'
  datasale!: number
  subscription!: Subscription;
  informationToken: any;
  bestSellingProducts!: bestSellingProducts[]
  newKeyword: string = "";
  notify: any;
  visible:boolean=false;
  data:any;
  Size!: any;
  countProduct:any;
  countprohouse:any;
  constructor(private ProducesService: ProducesService, private productService: ProductsService, private SaleService: SaleService, private AcountService: AccountService,
    private shareService: ShareService, private router: Router,private exportBillService:exportBillService,private warehouseService:WarehousedetailService,
    private MessageSV:MessageService
  ) { }

  ngOnInit() {
    this.informationToken = this.AcountService.decodeToken();
    this.Carts = this.productService.GetCart();
    this.resetAcount();
    this.responsiveOptions = [
      {
        breakpoint: '1199px',
        numVisible: 1,
        numScroll: 1
      },
      {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 1
      },
      {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1
      }
    ];
    this.GetallProduces();
    this.Getproductnew();
    this.startUpdateSalesPrices();
    this.GetproductSale();
    this.GetBestSellingProducts()
  }
  resetAcount() {
    if (this.informationToken && this.informationToken.status != 2) {
      localStorage.removeItem('Token');
      window.location.href = '/client/Home';
    }
  }

  startUpdateSalesPrices() {
    this.SaleService.UpdateSalesPrices().subscribe(data => { 
      if (data) {
        this.datasale = data;
      }
    });
  }
  showDialog(){
    this.visible=true;
  }
  handleEvent(e: any) {
    if (e.action == 'done') {
      this.startUpdateSalesPrices()
      this.GetproductSale();
      window.location.href = '/client/Home';
    }
  }

  search(data: string) {
    this.shareService.sendKeyword(data);
  }
  GetallProduces() {
    this.ProducesService.getAll().subscribe(data => {
      this.ListProduces = data;
    })
  }
  Getproductnew() {
    this.productService.Getproductnew().subscribe(data => {
      this.products = data.reduce((acc: any, x: any) => {
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
    });
  }
  GetproductSale() {
    this.productService.GetproductSale().subscribe(data => {
      this.productsSale = data;
    });
  }
  GetBestSellingProducts() {
    this.productService.GetBestSellingProducts().subscribe(data => {
      this.bestSellingProducts = data.reduce((acc: any, x: any) => {
        const kt = acc.find((y: any) => y.id === x.id);
        if (!kt) {
          acc.push(x);
        } else {
          if (kt.activeFlag - x.activeFlag < 1) {
            const index = acc.indexOf(kt);
            if (index !== -1) {
              acc.splice(index, 1); // Loại bỏ phần tử tại vị trí index
              acc.push(x);
            }
          }
        }
        return acc;
      }, []);
    });
  }

  getbyid(id: string) {
    this.visible=true;
    this.productService.getbyid(id).subscribe({
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
          console.log(this.data)
        }
      }
    })
  }
  selectSize(id:string,data: any) {
    this.Size = []
    this.Size = data;
    this.CountProWarehouse(id,data.id)
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
  CountProWarehouse(id:string,idSize:string){
    this.warehouseService.CountProduct(id,idSize).subscribe({
      next:(res)=>{
        if(res){
          this.countprohouse=res;
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
      this.productService.saveCart(this.Carts)
      this.MessageSV.add({ severity: 'success', summary: 'Thành công', detail: 'Thêm giỏ hàng thành công' })
    }
    else {
      this.visible=false;
      this.MessageSV.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Vui lòng chọn kích thước' })

    }
  }

}

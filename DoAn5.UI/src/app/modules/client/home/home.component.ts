import { Component } from '@angular/core';
import { Subscription, interval } from 'rxjs';
import { GetProductsDto, ProductsDto } from 'src/app/model';
import { AccountService } from 'src/app/service/account.service';
import { ProducesService } from 'src/app/service/produces.service';
import { ProductsService } from 'src/app/service/products.service';
import { SaleService } from 'src/app/service/sale.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  responsiveOptions: any[] | undefined;
  ListProduces:any[]=[];
  products:GetProductsDto[]=[];
  productsSale:GetProductsDto[]=[];

  layout: 'grid' | 'list' = 'grid'
  datasale!:any
  subscription!: Subscription;
  informationToken:any;
  constructor(private ProducesService:ProducesService, private productService:ProductsService,private SaleService : SaleService,private AcountService:AccountService) {}

  ngOnInit() {
    this.informationToken= this.AcountService.decodeToken();
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
  }
  resetAcount(){
    if(this.informationToken && this.informationToken.status !=1  ){
      localStorage.removeItem('Token');
      window.location.href = '/client/Home';

    }
  }
  startUpdateSalesPrices(): void {
    this.subscription = interval(1000) // Tạo một luồng mới gửi một sự kiện sau mỗi 1 phút (60 giây)
      .subscribe(() => { // Subscribe vào luồng
        this.SaleService.UpdateSalesPrices().subscribe(data => { // Gọi phương thức UpdateSalesPrices
          if (data !== "") {
            this.datasale = data;
          } else {
            this.subscription.unsubscribe(); // Dừng interval nếu data là null
          }
        });
      });
  }
  GetallProduces(){
    this.ProducesService.getAll().subscribe(data=>{
        this.ListProduces=data;
    })
  }
  Getproductnew(){
    this.productService.Getproductnew().subscribe(data=>{
      this.products= data.slice(0, 8);
    });
  }
  GetproductSale(){
    this.productService.GetproductSale().subscribe(data=>{
      this.productsSale= data;
    });
  }

  
}

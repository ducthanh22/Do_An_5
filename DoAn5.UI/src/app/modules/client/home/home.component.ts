import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription, interval } from 'rxjs';
import { GetProductsDto, ProductsDto, bestSellingProducts } from 'src/app/model';
import { ShareService } from 'src/app/service/Common/share.service';
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
  ListProduces: any[] = [];
  products: GetProductsDto[] = [];
  productsSale: GetProductsDto[] = [];

  layout: 'grid' | 'list' = 'grid'
  datasale!: number
  subscription!: Subscription;
  informationToken: any;
  bestSellingProducts!: bestSellingProducts[]
  newKeyword: string = "";
  notify: any
  constructor(private ProducesService: ProducesService, private productService: ProductsService, private SaleService: SaleService, private AcountService: AccountService,
    private shareService: ShareService, private router: Router
  ) { }

  ngOnInit() {
    this.informationToken = this.AcountService.decodeToken();
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
}

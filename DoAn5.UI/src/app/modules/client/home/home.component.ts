import { Component } from '@angular/core';
import { ProductsDto } from 'src/app/model';
import { ProducesService } from 'src/app/service/produces.service';
import { ProductsService } from 'src/app/service/products.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  responsiveOptions: any[] | undefined;
  ListProduces:any[]=[];
  products:ProductsDto[]=[];
  layout: 'grid' | 'list' = 'grid'
  constructor(private ProducesService:ProducesService, private productService:ProductsService) {}

  ngOnInit() {
      

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
      
  }

  GetallProduces(){
    this.ProducesService.getAll().subscribe(data=>{
        this.ListProduces=data;

    })
  }
  Getproductnew(){
    this.productService.Getproductnew().subscribe(data=>{
      this.products= data.slice(0, 8);
      console.log(this.products)
    });
  }

  
}

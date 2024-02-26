import { Component } from '@angular/core';
import { ProducesService } from 'src/app/service/produces.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  responsiveOptions: any[] | undefined;
  ListProduces:any[]=[]
  constructor(private ProducesService:ProducesService) {}

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
      this.GetallProduces()
  }

  GetallProduces(){
    this.ProducesService.getAll().subscribe(data=>{
        this.ListProduces=data;
    console.log(this.ListProduces)
    })
  }

  
}

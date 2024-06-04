import { Component } from '@angular/core';
import { format, setMonth } from 'date-fns';
import { StatisticalDto } from 'src/app/model/statistical';
import { StatisticalService } from 'src/app/service/statistical.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  salesChartData: any;
  ordersChartData: any;
  ratingChartData: any;
  revenueChartData: any;
  data: any;
  options: any;
  date1!: Date;
  date2!: Date;
  statistical!: StatisticalDto;
  maxdate!:Date;
  constructor(private statisticalservice: StatisticalService) { }

  ngOnInit() {
    this.maxdate= new Date();
    this.getDashboarsh();
  }

  getDashboarsh() {
    let start='';
    let end='';
    if (this.date1 == undefined && this.date2 == undefined) {
       start='';
       end='';
    }
    else{
      start  = format(this.date1, "yyyy-MM-dd'T'HH:mm:ss.SSS");
      end = format(this.date2, "yyyy-MM-dd'T'HH:mm:ss.SSS"); 
    }
    
    this.statisticalservice.Darhboarsh(start, end).subscribe({
      next: (value) => {
        if (value) {
          this.statistical = value;
          console.log(this.statistical);
          this.Charts(this.statistical)
        }
      },
    })
  }

  Charts(data: any) {
    this.options = {
      stacked: false,
      maintainAspectRatio: false,
      aspectRatio: 1.14,
    };

    let labelscategory = data.categoryRevenue.map((item: any) => item.nameCategory.toString());
    let datacategory = data.categoryRevenue.map((item: any) => item.revenue);
    this.salesChartData = {
      labels: labelscategory,
      datasets: [
        {
          data: datacategory,
          backgroundColor: ['#00CC00', '#33CCFF', '#CC0000','#FFFF00'],
          hoverBackgroundColor: ['#00FF00', '#33FFFF', '#FF0000','#FFFF66']
        }
      ]
    };
    let labelsRating = data.rating_rate.map((item: any) => item.name.toString());
    let dataRating = data.rating_rate.map((item: any) => item.quantity);
    this.ratingChartData = {
      labels: labelsRating,
      datasets: [
        {
          data: dataRating,
          backgroundColor: ['#00CC00', '#33CCFF', '#CC0000','#FFFF00'],
          hoverBackgroundColor: ['#00FF00', '#33FFFF', '#FF0000','#FFFF66']
        }
      ]
    };
    let labelsmonthlyrevenue = data.monthlyrevenue.map((item: any) =>  format(item.date, "MM").toString());
    let datamonthlyrevenue= data.monthlyrevenue.map((item: any) => item.revenue);
    this.revenueChartData = {
      labels: labelsmonthlyrevenue,
      datasets: [
        {
          label: 'Revenue',
          data: datamonthlyrevenue,
          fill: false,
          borderColor: '#4bc0c0'
        }
      ]
    };
  }

}



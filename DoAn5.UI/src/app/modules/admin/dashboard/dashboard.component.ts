import { Component } from '@angular/core';


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
  date1: Date | undefined;

  date2: Date | undefined;

  date3: Date | undefined;


  ngOnInit() {

  
        
        this.options = {
            stacked: false,
            maintainAspectRatio: false,
            aspectRatio: 1.15,
        };
    
    this.salesChartData = {
        labels: ['Clothing', 'Accessories', 'Electronics'],
        datasets: [
          {
            data: [300, 50, 100],
            backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'],
            hoverBackgroundColor: ['#FF6384', '#36A2EB', '#FFCE56']
          }
        ]
      };
  
      this.ordersChartData = {
        labels: ['January', 'February', 'March', 'April', 'May'],
        datasets: [
          {
            label: 'Orders',
            backgroundColor: '#42A5F5',
            borderColor: '#1E88E5',
            data: [65, 59, 80, 81, 56]
          }
        ]
      };
  
      this.ratingChartData = {
        labels: ['5', '4'],
        datasets: [
          {
            data: [200, 120],
            backgroundColor: ['#FF6384', '#36A2EB'],
            hoverBackgroundColor: ['#FF6384', '#36A2EB']
          }
        ]
      };
  
      this.revenueChartData = {
        labels: ['January', 'February', 'March', 'April', 'May'],
        datasets: [
          {
            label: 'Revenue',
            data: [15000, 20000, 18000, 22000, 24000],
            fill: false,
            borderColor: '#4bc0c0'
          }
        ]
      };
    }

    
      
  }
 


import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing.module';
import { CategoriesComponent } from './categories/categories.component';
import { CategoriesService } from 'src/app/service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { ChartModule } from 'primeng/chart';
import { DialogModule } from 'primeng/dialog';

@NgModule({
  declarations: [
    DashboardComponent,
    CategoriesComponent,
  
  
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    HttpClientModule,
    FormsModule,
    TableModule,
    ButtonModule,
    CalendarModule,
    ChartModule,
    DialogModule,ReactiveFormsModule
    
    

  ],
  providers: [
    CategoriesService,  
  ],
 
})
export class AdminModule { }

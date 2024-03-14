import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing.module';
import { CategoriesComponent } from './categories/categories.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { ChartModule } from 'primeng/chart';
import { DialogModule } from 'primeng/dialog';
import { ProductsComponent } from './products/products.component';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { ImageModule } from 'primeng/image';
import { DropdownModule } from 'primeng/dropdown';
@NgModule({
  declarations: [
    DashboardComponent,
    CategoriesComponent,
    ProductsComponent,
  
  
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
    DialogModule,ReactiveFormsModule,
    InputGroupModule,
    InputGroupAddonModule,
    ImageModule,
    DropdownModule
    
    

  ],
  providers: [ 
  ],
 
})
export class AdminModule { }

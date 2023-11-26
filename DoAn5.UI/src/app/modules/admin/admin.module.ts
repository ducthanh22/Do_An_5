import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing.module';
import { CategoriesComponent } from './categories/categories.component';
import { CategoriesService } from 'src/app/service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';

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
    ButtonModule

  ],
  providers: [
    CategoriesService,  
  ],
 
})
export class AdminModule { }

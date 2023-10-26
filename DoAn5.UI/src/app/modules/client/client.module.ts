import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { ServiceComponent } from './service/service.component';
import { CategoriesComponent } from './categories/categories.component';
import { HomeComponent } from './home/home.component';
import { ClientRoutingModule } from './client-routing.module';



@NgModule({
  declarations: [
    AboutComponent,
    ServiceComponent,
    CategoriesComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule
  ]
})
export class ClientModule { }

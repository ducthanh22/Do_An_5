import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { ServiceComponent } from './service/service.component';
import { CategoriesComponent } from './categories/categories.component';
import { HomeComponent } from './home/home.component';
import { ClientRoutingModule } from './client-routing.module';
import { BlogComponent } from './blog/blog.component';

import { ContactComponent } from './contact/contact.component';
import { CartComponent } from './cart/cart.component';
import { DetailproductsComponent } from './detailproducts/detailproducts.component';
import { PayproductsComponent } from './payproducts/payproducts.component';
import { CarouselModule } from 'primeng/carousel';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { DataViewModule } from 'primeng/dataview';
import { DropdownModule } from 'primeng/dropdown';
import { ImageModule } from 'primeng/image';
import { MessagesModule } from 'primeng/messages';
import { ToastModule } from 'primeng/toast';
import { OrderListModule } from 'primeng/orderlist';
import { InputNumberModule } from 'primeng/inputnumber';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { RatingModule } from 'primeng/rating';
import { PaginatorModule } from 'primeng/paginator';
import { MycartComponent } from './mycart/mycart.component';
import { StepsModule } from 'primeng/steps';
import { TreeTableModule } from 'primeng/treetable';
import { TableModule } from 'primeng/table';
@NgModule({
  declarations: [
    AboutComponent,
    ServiceComponent,
    CategoriesComponent,
    HomeComponent,
    BlogComponent,
    ContactComponent,
    CartComponent,
    DetailproductsComponent,
    PayproductsComponent,
    MycartComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
    CarouselModule,
    ButtonModule,
    TagModule,
    DataViewModule,
    DropdownModule,
    ImageModule,
    MessagesModule,
    ToastModule,
    OrderListModule,
    InputNumberModule,
    ReactiveFormsModule,
    FormsModule,
    InputTextModule,
    RatingModule,
    PaginatorModule,
    StepsModule,
    TreeTableModule,
    TableModule
  ]
})
export class ClientModule { }

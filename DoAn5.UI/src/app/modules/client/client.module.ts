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
    PayproductsComponent
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
    ToastModule
  ]
})
export class ClientModule { }

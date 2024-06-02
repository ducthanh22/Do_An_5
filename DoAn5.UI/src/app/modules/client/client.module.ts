import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesComponent } from './categories/categories.component';
import { HomeComponent } from './home/home.component';
import { ClientRoutingModule } from './client-routing.module';
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
import { DialogModule } from 'primeng/dialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { BadgeModule } from 'primeng/badge';
import { SearchComponent } from './search/search.component';
import { CountdownModule } from 'ngx-countdown';
import { ColorPickerModule } from 'primeng/colorpicker';
import { ConfirmOrderComponent } from './confirm-order/confirm-order.component';



@NgModule({
  declarations: [
    CategoriesComponent,
    HomeComponent,
    CartComponent,
    DetailproductsComponent,
    PayproductsComponent,
    MycartComponent,
    SearchComponent,
    ConfirmOrderComponent
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
    TableModule,
    DialogModule,
    InputTextareaModule,
    BadgeModule,
    CountdownModule,
    ColorPickerModule,
    TableModule,

  ]
})
export class ClientModule { }

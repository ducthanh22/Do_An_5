import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesComponent } from './categories/categories.component';
import { HomeComponent } from './home/home.component';
import { CartComponent } from './cart/cart.component';
import { DetailproductsComponent } from './detailproducts/detailproducts.component';
import { PayproductsComponent } from './payproducts/payproducts.component';
import { MycartComponent } from './mycart/mycart.component';
import { SearchComponent } from './search/search.component';
import { ConfirmOrderComponent } from './confirm-order/confirm-order.component';


const routes: Routes = [
  
  
      {
        path: 'Home',
        component:HomeComponent,
        title: 'Home',
      },
      {
        path: 'categories',
        component: CategoriesComponent,
        title: 'Categories',
       
      },
     {path:'cart',component:CartComponent,title:'Cart'},
     {path:'mycart',component:MycartComponent,title:'MyCart'},
     {path:'detail/:id',component:DetailproductsComponent,title:'Detail'},
     {path:'pay',component:PayproductsComponent,title:'Pay'},
     {path:'search',component:SearchComponent,title:'Search'},
     {path:'confirmOder/:id',component:ConfirmOrderComponent,title:'Confirm_Oder'}


    
  

   
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CategoriesComponent } from './categories/categories.component';
import { ProductsComponent } from './products/products.component';
import { ProduceComponent } from './produce/produce.component';
import { ProductTypeComponent } from './product-type/product-type.component';
import { OrderComponent } from './order/order.component';





const routes: Routes = [
    {path:'dashboard',component:DashboardComponent},
    {path:'categories_admin',component:CategoriesComponent},
    {path:'produces',component:ProduceComponent},
    {path:'products',component:ProductsComponent},
    {path:'productsType',component:ProductTypeComponent},
    {path:'order',component:OrderComponent}



   
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

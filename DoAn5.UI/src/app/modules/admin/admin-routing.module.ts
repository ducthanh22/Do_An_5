import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CategoriesComponent } from './categories/categories.component';
import { ProductsComponent } from './products/products.component';
import { ProduceComponent } from './produce/produce.component';
import { ProductTypeComponent } from './product-type/product-type.component';
import { OrderComponent } from './order/order.component';
import { ExportBillComponent } from './export-bill/export-bill.component';
import { SaleComponent } from './sale/sale.component';
import { ColorComponent } from './color/color.component';
import { ImportBillComponent } from './import-bill/import-bill.component';
import { RoleclaimComponent } from './roleclaim/roleclaim.component';
import { StaffComponent } from './staff/staff.component';
import { CustomerComponent } from './customer/customer.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { Rating } from 'primeng/rating';
import { RatingComponent } from './rating/rating.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';


const routes: Routes = [
    {path:'dashboard',component:DashboardComponent,title:'ADMIN'},
    {path:'categories_admin',component:CategoriesComponent,title:'ADMIN'},
    {path:'produces',component:ProduceComponent,title:'ADMIN'},
    {path:'products',component:ProductsComponent},
    {path:'productsType',component:ProductTypeComponent,title:'ADMIN'},
    {path:'order',component:OrderComponent,title:'ADMIN'},
    {path:'exportbill',component:ExportBillComponent,title:'ADMIN'},
    {path:'sale',component:SaleComponent,title:'ADMIN'},
    {path:'color',component:ColorComponent,title:'ADMIN'},
    {path:'importbill',component:ImportBillComponent},
    {path:'role',component:RoleclaimComponent,title:'ADMIN'},
    {path:'staff',component:StaffComponent,title:'ADMIN'},
    {path:'customer',component:CustomerComponent,title:'ADMIN'},
    {path:'warehouse',component:WarehouseComponent,title:'ADMIN'},
    {path:'rating',component:RatingComponent,title:'ADMIN'},
    {path:'unauthorized',component:UnauthorizedComponent,title:'Unauthorized'},







];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

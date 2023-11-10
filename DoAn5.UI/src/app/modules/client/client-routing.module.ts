import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { CategoriesComponent } from './categories/categories.component';
import { ServiceComponent } from './service/service.component';
import { HomeComponent } from './home/home.component';
import { BlogComponent } from './blog/blog.component';
import { ContactComponent } from './contact/contact.component';
import { CartComponent } from './cart/cart.component';
import { DetailproductsComponent } from './detailproducts/detailproducts.component';
import { PayproductsComponent } from './payproducts/payproducts.component';


const routes: Routes = [
  
  
      {
        path: 'Home',
        component:HomeComponent,
        title: 'Home',
      },
      {
        path: 'about',
        component: AboutComponent,
        title: 'About',
        
      },
      {
        path: 'categories',
        component: CategoriesComponent,
        title: 'Categories',
       
      },
      {
        path: 'service',
        component:ServiceComponent,
        title: 'Service',
      },
      {
        path: 'blog',
        component:BlogComponent,
        title: 'Blog',
      },
      {
        path: 'contact',
        component:ContactComponent,
        title: 'Contact',
      },
     {path:'cart',component:CartComponent,title:'Cart'},

     {path:'detail',component:DetailproductsComponent,title:'Detail'},
     {path:'pay',component:PayproductsComponent,title:'Pay'}
    
  

   
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }

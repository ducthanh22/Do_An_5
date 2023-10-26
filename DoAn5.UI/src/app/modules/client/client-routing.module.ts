import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { CategoriesComponent } from './categories/categories.component';
import { ServiceComponent } from './service/service.component';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  
  
      {
        path: 'Home',
        component:HomeComponent,
        title: 'Home',
      },
      {
        path: 'about',
        component: AboutComponent,
        title: 'about',
        data: {
          title: 'about',
        },
      },
      {
        path: 'categories',
        component: CategoriesComponent,
        title: 'categories',
        data: {
          title: 'categories',
        },
      },
      {
        path: 'service',
        component:ServiceComponent,
        title: 'service',
      },
    
  

   
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }

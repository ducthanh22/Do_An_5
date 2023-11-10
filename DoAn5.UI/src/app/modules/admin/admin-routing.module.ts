import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RecruitmentComponent } from './recruitment/recruitment.component';




const routes: Routes = [
    {path:'dashboard',component:DashboardComponent},
    {path:'tuyendung',component:RecruitmentComponent}
   
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

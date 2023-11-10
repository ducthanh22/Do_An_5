import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing.module';
import { RecruitmentComponent } from './recruitment/recruitment.component';




@NgModule({
  declarations: [
    DashboardComponent,
    RecruitmentComponent
  
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }

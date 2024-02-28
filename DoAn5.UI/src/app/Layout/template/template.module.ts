import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminAdmintemplateComponent } from './admin-admintemplate/admin-admintemplate.component';
import { ClientClienttemplateComponent } from './client-clienttemplate/client-clienttemplate.component';
import { PartialsModule } from '../partials/partials.module';
import { ClientModule } from 'src/app/modules/client/client.module';
import { ClientRoutingModule } from 'src/app/modules/client/client-routing.module';
import { LoginComponent } from './login/login.component';
import { AdminModule } from 'src/app/modules/admin/admin.module';
import { AdminRoutingModule } from 'src/app/modules/admin/admin-routing.module';
import { InputTextModule } from 'primeng/inputtext';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { ToastModule } from 'primeng/toast';




@NgModule({
  declarations: [
    AdminAdmintemplateComponent,
    ClientClienttemplateComponent,
    LoginComponent,
    ResetpasswordComponent,
    ForgotpasswordComponent,
  
  ],
  imports: [
    CommonModule,
    PartialsModule,
    ClientModule,
    ClientRoutingModule,
    AdminModule,
    AdminRoutingModule,
    InputTextModule,
    FormsModule,
    ButtonModule,
    InputGroupModule ,
    InputGroupAddonModule,
    ToastModule

  ]
})
export class TemplateModule { }

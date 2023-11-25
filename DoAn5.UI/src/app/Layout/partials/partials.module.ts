import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { HeaderadminComponent } from './headeradmin/headeradmin.component';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    HeaderadminComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[HeaderComponent,
    FooterComponent,
    SidebarComponent,HeaderadminComponent]
})
export class PartialsModule { }

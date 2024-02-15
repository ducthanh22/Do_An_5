import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { HeaderadminComponent } from './headeradmin/headeradmin.component';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
// import { BrowserModule } from '@angular/platform-browser';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    HeaderadminComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    DropdownModule,
    FormsModule,
    MenubarModule,
    InputTextModule
   
    


  ],
  exports:[HeaderComponent,
    FooterComponent,
    SidebarComponent,HeaderadminComponent]
})
export class PartialsModule { }

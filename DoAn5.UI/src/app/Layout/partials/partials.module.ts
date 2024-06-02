import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { BrowserModule } from '@angular/platform-browser';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import { SplitButtonModule } from 'primeng/splitbutton';
import { ToastModule } from 'primeng/toast';
import { MenuModule } from 'primeng/menu';
import { BadgeModule } from 'primeng/badge';
import { DialogModule } from 'primeng/dialog';





@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SidebarComponent,

  ],
  imports: [
    CommonModule,
    RouterModule,
    DropdownModule,
    FormsModule,
    MenubarModule,
    InputTextModule,
    SidebarModule,
    ButtonModule,
    ToolbarModule,
    SplitButtonModule,
    ToastModule,
    MenuModule,
    BadgeModule,
    DialogModule,
    ReactiveFormsModule,

    
  ],
  exports:[HeaderComponent,
    FooterComponent,
    SidebarComponent]
})
export class PartialsModule { }

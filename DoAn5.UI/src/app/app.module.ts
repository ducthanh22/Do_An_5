import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TemplateModule } from './Layout/template/template.module';
import { TemplateRoutingModule } from './Layout/template/template-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MessageService } from 'primeng/api';



@NgModule({
  declarations: [
    AppComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    TemplateModule,
    TemplateRoutingModule,
   
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing.module';
import { CategoriesComponent } from './categories/categories.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { ChartModule } from 'primeng/chart';
import { DialogModule } from 'primeng/dialog';
import { ProductsComponent } from './products/products.component';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { ImageModule } from 'primeng/image';
import { DropdownModule } from 'primeng/dropdown';
import { EditorModule } from 'primeng/editor';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { FileUploadModule } from 'primeng/fileupload';
import { ProduceComponent } from './produce/produce.component';
import { ToastModule } from 'primeng/toast';
import { ProductTypeComponent } from './product-type/product-type.component';
import { OrderComponent } from './order/order.component';
import { ExportBillComponent } from './export-bill/export-bill.component';
import { CardModule } from 'primeng/card';
import { ToolbarModule } from 'primeng/toolbar';
import { SaleComponent } from './sale/sale.component';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ColorComponent } from './color/color.component';
import { ImportBillComponent } from './import-bill/import-bill.component';
import { RoleclaimComponent } from './roleclaim/roleclaim.component';
import { TreeSelectModule } from 'primeng/treeselect';
import { TagModule } from 'primeng/tag';
import { StaffComponent } from './staff/staff.component';
import { CustomerComponent } from './customer/customer.component';
import { ColorPickerModule } from 'primeng/colorpicker';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ClipboardModule } from '@angular/cdk/clipboard'; 
import { WarehouseComponent } from './warehouse/warehouse.component';
import { RatingComponent } from './rating/rating.component';
import { RatingModule } from 'primeng/rating';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { InputNumberModule } from 'primeng/inputnumber';


@NgModule({
  declarations: [
    DashboardComponent,
    CategoriesComponent,
    ProductsComponent,
    ProduceComponent,
    ProductTypeComponent,
    OrderComponent,
    ExportBillComponent,
    SaleComponent,
    ColorComponent,
    ImportBillComponent,
    RoleclaimComponent,
    StaffComponent,
    CustomerComponent,
    WarehouseComponent,
    RatingComponent,
    UnauthorizedComponent
  
  
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    HttpClientModule,
    FormsModule,
    TableModule,
    ButtonModule,
    CalendarModule,
    ChartModule,
    DialogModule,ReactiveFormsModule,
    InputGroupModule,
    InputGroupAddonModule,
    ImageModule,
    DropdownModule,
    EditorModule,
    ConfirmDialogModule,
    FileUploadModule,
    ToastModule,
    CardModule,
    ToolbarModule,
    SelectButtonModule,
    TreeSelectModule,
    TagModule,
    ColorPickerModule,
    InputTextModule,
    CheckboxModule,
    RadioButtonModule,
    ClipboardModule,
    RatingModule,
    InputNumberModule

    
  ],
  providers: [ConfirmationService],
 
})
export class AdminModule { }

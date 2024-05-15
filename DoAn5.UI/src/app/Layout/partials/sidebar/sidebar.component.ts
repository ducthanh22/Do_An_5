import { Component, ViewChild } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { Sidebar } from 'primeng/sidebar';
import { AccountService } from 'src/app/service/account.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {


  sidebarVisible: boolean = false;
  items: MenuItem[];
  label!:string
  informationToken!:any;


  constructor(private messageService: MessageService, private AcountService:AccountService) {
      this.items = [
          {
              label: 'Thông tin',
              icon: 'pi pi-refresh',
              command: () => {
                  this.update();
              }
          },
          {
              label: 'Đăng xuất',
              icon: 'pi pi-times',
              command: () => {
                  this.delete();
              }
          },
      ];
  }
  ngOnInit(){
    this.informationToken= this.AcountService.decodeToken();
    this.showName();

  }

  save(severity: string) {
      this.messageService.add({ severity: severity, summary: 'Success', detail: 'Data Saved' });
  }

  update() {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Data Updated' });
  }

  delete() {
    const token = localStorage.getItem('Token');
    if(token !=null){
        localStorage.removeItem('Token');
        window.location.reload()
    }
}
showName(){
    if(this.informationToken){
        this.label=this.informationToken.Username  
        return this.label  
    }
    return  this.label=" Đăng Nhập"
  } 

}

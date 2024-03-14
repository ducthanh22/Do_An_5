import { Component, ViewChild } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { Sidebar } from 'primeng/sidebar';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  // @ViewChild('sidebarRef') sidebarRef!: Sidebar;

  // closeCallback(e: any): void {
  //     this.sidebarRef.close(e);
  // }

  sidebarVisible: boolean = false;
  items: MenuItem[];
  label:string="Phí Đức Thanh"

  constructor(private messageService: MessageService) {
      this.items = [
          {
              label: 'Update',
              icon: 'pi pi-refresh',
              command: () => {
                  this.update();
              }
          },
          {
              label: 'Delete',
              icon: 'pi pi-times',
              command: () => {
                  this.delete();
              }
          },
          { label: 'Angular.io', icon: 'pi pi-info', url: 'http://angular.io' },
          { separator: true },
          { label: 'Installation', icon: 'pi pi-cog', routerLink: ['/installation'] }
      ];
  }

  save(severity: string) {
      this.messageService.add({ severity: severity, summary: 'Success', detail: 'Data Saved' });
  }

  update() {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Data Updated' });
  }

  delete() {
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Data Deleted' });
  }
  

}

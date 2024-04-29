import { Component } from '@angular/core';
import { MenuItem, MessageService, TreeNode } from 'primeng/api';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mycart',
  templateUrl: './mycart.component.html',
  styleUrls: ['./mycart.component.css']
})
export class MycartComponent {
  items!: MenuItem[];

  subscription!: Subscription;
  files!: TreeNode[];
  activeIndex: number = 0;
  onActiveIndexChange(event: number) {
    this.activeIndex = event;
}
  constructor(public messageService: MessageService,) {}

  ngOnInit() {
    this.items = [
      {
          label: 'Chờ xác nhận',
          command: (event: any) => this.messageService.add({severity:'info', summary:'First Step', detail: event.item.label})
      },
      {
          label: 'Chờ lấy hàng',
          command: (event: any) => this.messageService.add({severity:'info', summary:'Second Step', detail: event.item.label})
      },
      {
          label: 'Chờ giao hàng',
          command: (event: any) => this.messageService.add({severity:'info', summary:'Third Step', detail: event.item.label})
      },
      {
          label: 'Đánh giá',
          command: (event: any) => this.messageService.add({severity:'info', summary:'Last Step', detail: event.item.label})
      }
  ];
  // this.nodeService.getFilesystem().then((files) => (this.files = files));
  this.files = [
    {
      label: 'Documents',
      children: [
        { label: 'Work', icon: 'pi pi-fw pi-file', children: [{ label: 'Expenses.doc', icon: 'pi pi-fw pi-file' }] },
        { label: 'Home', icon: 'pi pi-fw pi-home', children: [{ label: 'Invoices.txt', icon: 'pi pi-fw pi-file' }] }
      ]
    },
    {
      label: 'Pictures',
      children: [
        { label: 'barcelona.jpg', icon: 'pi pi-fw pi-image' },
        { label: 'primefaces.png', icon: 'pi pi-fw pi-image' },
        { label: 'optimus.jpg', icon: 'pi pi-fw pi-image' }
      ]
    }
  ];
 
  }

  ngOnDestroy() {
      if (this.subscription) {
          this.subscription.unsubscribe();
      }
  }
  update(){

  }
  
}

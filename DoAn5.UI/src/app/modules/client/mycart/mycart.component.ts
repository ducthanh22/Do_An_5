import { Component } from '@angular/core';
import { MenuItem, MessageService, TreeNode } from 'primeng/api';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/service/account.service';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-mycart',
  templateUrl: './mycart.component.html',
  styleUrls: ['./mycart.component.css']
})
export class MycartComponent {
  items!: MenuItem[];

  subscription!: Subscription;
  files!: any;
  activeIndex: number = 0;
  informationToken: any
  productColumns = [
    { field: 'id_product', header: 'ID Sản phẩm' },
    { field: 'image', header: 'Ảnh' },
    { field: 'quantity', header: 'Số lượng' },
    { field: 'price', header: 'Giá' }
  ];
  router: any;
  constructor(public messageService: MessageService, private OrderService: OrderService, private AcountService: AccountService) { }

  ngOnInit() {
    this.informationToken = this.AcountService.decodeToken();
    this.GetOrderProduct(1);
    this.items = [
      {
        label: 'Chờ xác nhận',
        command:() => {this.GetOrderProduct(1);}
      },
      {
        label: 'Chờ lấy hàng',
        command:() => {this.GetOrderProduct(2);}
      },
      {
        label: 'Chờ giao hàng',
        command:() => {this.GetOrderProduct(3);}
      },
      {
        label: 'Đánh giá',
        command:() => {this.GetOrderProduct(4);}
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
  onActiveIndexChange(event: number) {
    this.activeIndex = event;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  GetOrderProduct(status: number) {
    this.OrderService.GetOrderProduct(this.informationToken.Id, status).subscribe({
      next:(res)=>{
        if(res){
          this.files = res
          console.log(this.files)
        }

      }
    })

  }
  OpenPay(){
    if(this.informationToken){
      this.files && this.files.length>0?this.router.navigate(['/client/pay']):this.router.navigate(['/client/Home'])
    }
    else{
      this.messageService.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Vui lòng đăng nhập' })
    }
  }


}

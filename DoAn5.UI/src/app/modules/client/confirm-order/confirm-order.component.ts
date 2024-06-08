import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CreateOrderDto, OrderDto } from 'src/app/model';
import { AccountService } from 'src/app/service/account.service';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-confirm-order',
  templateUrl: './confirm-order.component.html',
  styleUrl: './confirm-order.component.css'
})
export class ConfirmOrderComponent {
  token!: string;
  id!: string;
  dataOrder!: any;
  informationToken:any;
  constructor(private route: Router, private activeRoute: ActivatedRoute, private orderService: OrderService,
     private messageService: MessageService,private AcountService:AccountService) { }
  ngOnInit() {
    this.confirmOrder();
  }
  confirmOrder() {
    var checkToken = localStorage.getItem('Token');
    if (checkToken != undefined) {
      this.informationToken = this.AcountService.decodeToken();
      this.activeRoute.params.subscribe(params => {
        this.id = params['id'];
        this.orderService.getbyid(this.id).subscribe({
          next: (res) => {
            if (res) {
              if(res[0].id_customer== this.informationToken.Id){
                this.dataOrder = res;
                this.updateOrder(this.dataOrder)
              }
              else{
              localStorage.removeItem('Token');
              this.route.navigate(['/Login'])
              }
              
            }
          }
        })
      });
    }
    else {
      this.route.navigate(['/Login'])
    }
  }
  updateOrder(data: any) {
    if (data[0].status == 1) {
      data[0].status = 2;
      this.orderService.Update(data[0]).subscribe({
        next: (res) => {
          if (res) {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Đặt hàng thành công' })
          }
        }
      })
    }
    else {
      this.messageService.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Đơn hàng đã được đặt' })

    }
  }

}

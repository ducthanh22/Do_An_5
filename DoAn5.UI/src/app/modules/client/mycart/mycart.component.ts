import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem, MessageService, TreeNode } from 'primeng/api';
import { Subscription } from 'rxjs';
import { OrderDto } from 'src/app/model';
import { CreateRatingDto } from 'src/app/model/rating';
import { AccountService } from 'src/app/service/account.service';
import { OrderService } from 'src/app/service/order.service';
import { RatingService } from 'src/app/service/rating.service';
import { format } from 'date-fns';
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
  visible: boolean = false
  evaluate: number = 0;
  data_evaluate: any
  createRating!: CreateRatingDto
  order:any

  constructor(public messageService: MessageService, private OrderService: OrderService, private AcountService: AccountService,
    private router: Router, private RatingService: RatingService) { }

  ngOnInit() {
    this.informationToken = this.AcountService.decodeToken();
    console.log(this.informationToken)
    this.GetOrderProduct(1);
    this.items = [
      {
        label: 'Chờ xác nhận',
        command: () => { this.GetOrderProduct(1); }
      },
      {
        label: 'Chờ lấy hàng',
        command: () => { this.GetOrderProduct(2); }
      },
      {
        label: 'Chờ giao hàng',
        command: () => { this.GetOrderProduct(3); }
      },
      {
        label: 'Đánh giá',
        command: () => { this.GetOrderProduct(4); }
      },
      {
        label: 'Đã Đánh giá',
        command: () => { this.GetOrderProduct(5); }
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
      next: (res) => {
        if (res) {
          this.files = res
        }

      }
    })
  }


  Rating(data: any) {
    this.data_evaluate = data;
    this.visible = true;
  }
  SaveRating() {
    this.createRating = {
      listRating: this.data_evaluate?.orderProductList.map((item: any) => ({
        id_Order: this.data_evaluate.id,
        id_product: item.id_product,
        id_customer: this.informationToken.Id,
        evaluate: item.evaluate,
        comment: item.comment,
        status: 1
      }))
    }
    this.RatingService.CreateS(this.createRating).subscribe({
      next: (value) => {
        if (value) {
          this.messageService.add({ severity: 'success', summary: 'Thành công', detail: 'Đánh giá thành công' })
         
          for (let x of value.listRating){
            this.OrderService.getbyid(x.id_Order).subscribe({
              next:(value)=>{
                this.order=value
              }
            })
            const order: OrderDto = {
              id: x.id_Order,
              id_customer: x.id_customer,
              status: 5,
              price: this.data_evaluate.price,
              address: this.data_evaluate.address,
              payment: this.data_evaluate.payment,
              activeFlag: null,
              createdBy: null,
              created: format(this.order?.created, "yyyy-MM-dd'T'HH:mm:ss.SSS"),
              modifiedBy: null,
              modified: format(new Date(), "yyyy-MM-dd'T'HH:mm:ss.SSS")
            }
            this.OrderService.Update(order).subscribe(data=>{
              const a =data
            })
          }
        }
        this.GetOrderProduct(4);
        this.visible=false
      },
    })
  }
  OpenPay() {
    if (this.informationToken) {
      this.files && this.files.length > 0 ? this.router.navigate(['/client/pay']) : this.router.navigate(['/client/Home'])
    }
    else {
      this.messageService.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Vui lòng đăng nhập' })
    }
  }


}

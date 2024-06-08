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
import { exportBillService } from 'src/app/service/Exportbill.service';
import { CreateExportbillDto } from 'src/app/model/exportBill';
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
  data_evaluate: any;
  createRating!: CreateRatingDto;
  order: any;
  active!: number;
  visibledetail:boolean=false;
  dataDetail:any;
  constructor(public messageService: MessageService, private OrderService: OrderService, private AcountService: AccountService,
    private router: Router, private RatingService: RatingService, private exportBillService: exportBillService) { }

  ngOnInit() {
    this.informationToken = this.AcountService.decodeToken();
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
        label: 'Đang giao hàng',
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
      next: (res) => {
        if (res) {
          this.OrderService.getbyid(res.listRating[0].id_Order).subscribe({
            next: (res) => {
              if (res) {
                this.order = res;
                const order: OrderDto = {
                  id: this.order[0].id,
                  id_customer: this.order[0].id_customer,
                  status: 5,
                  price: this.order[0].price,
                  address: this.order[0].address,
                  payment: this.order[0].payment,
                  activeFlag: 1,
                  createdBy: null,
                  created: format(this.order[0].created, "yyyy-MM-dd'T'HH:mm:ss.SSS"),
                  modifiedBy: null,
                  modified: format(new Date(), "yyyy-MM-dd'T'HH:mm:ss.SSS")
                }
                this.OrderService.Update(order).subscribe({
                  next: (res) => {
                    if (res) {
                      this.GetOrderProduct(4);
                      this.visible = false
                      this.messageService.add({ severity: 'success', summary: 'Thành công', detail: 'Đánh giá thành công' })
                    }
                  }
                })
              }
            }
          })
        }
      }
    })
  }

  Update(data: any, status: number) {
    status == 7 ? this.active = 0 : this.active = 1
    const order: OrderDto = {
      id: data.id,
      id_customer: data.id_customer,
      status: status,
      price: data.price,
      address: data.address,
      payment: data.payment,
      activeFlag: this.active,
      createdBy: null,
      created: format(data.created, "yyyy-MM-dd'T'HH:mm:ss.SSS"),
      modifiedBy: null,
      modified: format(new Date(), "yyyy-MM-dd'T'HH:mm:ss.SSS")
    }
    this.OrderService.Update(order).subscribe({
      next: (value) => {
        if (value) {
          this.OrderService.getbyid(data.id).subscribe({
            next: (res) => {
              if (res) {
                this.order = res;
                const exportBill: CreateExportbillDto = {

                  price: this.order[0].price,
                  status: 0,
                  idStaff: this.order[0].id_customer,
                  detail_exportbillDto: this.order[0].orderProductList.map((item: any) => ({
                    Idproduct: item.id_product,
                    idsize: item.id_size,
                    quantity: item.quantity,
                    price: item.price,
                  }))
                }
                this.exportBillService.create(exportBill).subscribe({
                  next: (res) => {
                    if (res) {
                      this.GetOrderProduct(3);
                      this.visible = false
                      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Đã nhận hàng thành công' })
                    }
                  }
                })
              }
            }
          })
        }
      }
    })
  }
 detailOrder(data:any){
  this.visibledetail=true;
  this.dataDetail=data;
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

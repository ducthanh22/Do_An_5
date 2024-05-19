import { ChangeDetectorRef, Component } from '@angular/core';
import { format } from 'date-fns';
import { ConfirmationService, MessageService } from 'primeng/api';
import { OrderDto, Paging } from 'src/app/model';
import { OrderService } from 'src/app/service/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  paging: Paging = { keyword: "", pageIndex: 1, pageSize: 10 };
  listOrder: any;
  totalCount!: number;
  keyword: string = "";
  loading!: boolean;
  visible: boolean = false;
  dataGetId: any[] = [];
  order: any;
  active!: number;
  constructor(private OrderService: OrderService, private messageService: MessageService, private changeDetector: ChangeDetectorRef,
    private confirmationService: ConfirmationService,
  ) { }
  ngOnInit() {

  }
  onsubmit() {
    this.paging.keyword = this.keyword;
    this.OrderService.Search(this.paging).subscribe({
      next: (res) => {
        this.listOrder = res.data;
        this.totalCount = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });

  }

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.keyword,
    };
    this.OrderService.Search(this.paging).subscribe({
      next: (res) => {
        this.listOrder = res.data;
        this.totalCount = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };

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
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Đổi trạng thái thành công' })
          this.onsubmit()
        }
      }
    })
  }
  confirm2(event: Event,data:any) {
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: 'Bạn có muốn hủy đơn hàng không?',
        header: 'Đơn hàng lỗi phát sinh',
        icon: 'pi pi-info-circle',
        acceptButtonStyleClass:"p-button-danger p-button-text",
        rejectButtonStyleClass:"p-button-text p-button-text",
        acceptIcon:"none",
        rejectIcon:"none",

        accept: () => {
          this.Update(data,7)
        },
        reject: () => {
            this.messageService.add({ severity: 'error', summary: 'Hủy', detail: 'Bạn đã hủy' });
        }
    });
}



  getOrderDetail(data: string) {
    this.visible = true;
    this.OrderService.getbyid(data).subscribe({
      next: (res) => {
        if (res) {
          this.dataGetId = res;
        }
      }
    })
  }
}

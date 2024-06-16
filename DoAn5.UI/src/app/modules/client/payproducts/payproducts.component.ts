import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CreateOrderDto, OrderDto, Order_detailDto } from 'src/app/model';
import { PaymentDto } from 'src/app/model/payment';
import { PaymentService } from 'src/app/service/Payment.service';
import { AccountService } from 'src/app/service/account.service';
import { OrderService } from 'src/app/service/order.service';
import { ProductsService } from 'src/app/service/products.service';
import { SendEmailService } from 'src/app/service/sendEmail.service';
import { format } from 'date-fns';

@Component({
  selector: 'app-payproducts',
  templateUrl: './payproducts.component.html',
  styleUrls: ['./payproducts.component.css']
})
export class PayproductsComponent {
  Carts!: any[];
  Pay!: any[] | undefined
  FormPay!: FormGroup
  informationAccount: any
  formData: FormData = new FormData();
  datapayment: PaymentDto = { orderId: '', money: 0, transactionStatus: 0 };
  GetId_order: string = ''
  order: any;
  loading: boolean = false;

  constructor(private productService: ProductsService, private fb: FormBuilder, private AccountService: AccountService,
    private OrderService: OrderService, private MessageSV: MessageService, private EmailService: SendEmailService, private PaymentService: PaymentService,
    private route: ActivatedRoute, private router:Router
  ) { }
  ngOnInit() {
    this.Carts = this.productService.GetCart();
    this.informationAccount = this.AccountService.decodeToken()
    this.ReturnUrl()
    this.Pay = [
      { name: 'Thanh toán khi nhận hàng', code: 'Cod' },
      { name: 'Thanh toán online VNPAY', code: 'VNPAY' },
    ];
    this.FormPay = this.fb.group({
      name: new FormControl(this.informationAccount.Username, Validators.required),
      email: new FormControl(this.informationAccount.Email, Validators.required),
      phone: new FormControl(this.informationAccount.Phone, Validators.required),
      Address: new FormControl('', Validators.required),
      selectPay: new FormControl("", Validators.required)
    })
  }
  totalPrice(index: number) {
    let total = this.Carts[index].data.price_product * this.Carts[index].quantity
    return total
  }
  getTotalPrice(): number {
    let total: number = 0;
    this.Carts.forEach((item: any) => {
      total += item.data.price_product * item.quantity;
    });
    return total;
  }

  SaveAdd() {
    debugger
    if (this.Carts.length > 0) {
      this.loading = true;
      const order: CreateOrderDto = {
        id_customer: this.informationAccount.Id,
        status: 1,
        price: this.getTotalPrice(),
        address: this.FormPay.value.Address,
        payment: this.FormPay.value.selectPay.name,
        orderList: this.Carts.map((item: any) => ({
          // id_Order: undefined,
          id_product: item.data.id,
          idsize: item.size.id,
          quantity: item.quantity,
          price: item.data.price_product,
        }))
      };
      if (this.FormPay.value.selectPay.code == 'VNPAY') {
        order.status=0;
        this.OrderService.create(order).subscribe({
          next: (res) => {
            if (res != null) {
              this.datapayment.orderId = res.id,
                this.GetId_order = res.id,
                this.datapayment.money = this.getTotalPrice(),
                this.datapayment.transactionStatus = 0,
                this.PaymentService.CreatURL(this.datapayment).subscribe({
                  next: (url) => {
                    // window.open(url, '_blank');
                    window.open(url, '_self')
                  }
                });
            }
          }
        })
      }
      else{
        this.OrderService.create(order).subscribe({
          next: (res) => {
            if (res != null) {
              this.FormPay.reset();
              this.Carts = []
              this.productService.saveCart(this.Carts);
              this.loading = false;
              this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Đặt hàng thành công vui lòng kiểm tra email' })
            }
          }
        })

      }
    }
  }

    ReturnUrl() {
      const params = this.route.snapshot.queryParams;
      this.PaymentService.Callback(params).subscribe({
        next: (res) => {
          if (res.vnp_TransactionStatus == '00') {
            this.OrderService.getbyid(res.vnp_TxnRef).subscribe({
              next: (value) => {
                if (value) {
                  debugger
                  this.order = value;
                  const order: OrderDto = {
                    id: res.vnp_TxnRef,
                    id_customer: this.informationAccount.Id,
                    status: 2,
                    price: this.order[0]?.price,
                    address: this.order[0]?.address,
                    payment: this.order[0]?.payment,
                    activeFlag: null,
                    createdBy: null,
                    created: format(this.order[0]?.created, "yyyy-MM-dd'T'HH:mm:ss.SSS"),
                    modifiedBy: null,
                    modified: format(new Date(), "yyyy-MM-dd'T'HH:mm:ss.SSS")
                  }
                  this.OrderService.Update(order).subscribe({
                    next: (value) => {
                      if (value) {
                        this.FormPay.reset();
                        this.Carts = []
                        this.productService.saveCart(this.Carts);
                       
                        this.formData = new FormData();
                        this.formData.append('email', this.informationAccount.Email);
                        this.formData.append('donhang', value.id)
                        this.EmailService.SendEmail(this.formData).subscribe({
                          next: (response) => {
                            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Đặt hàng thành công' });
                              this.router.navigate(['/client/cart'])
                          },
                        })
                      }
                    }
                  })
                 

                }
              }
            })

          }
        },
        error: (error) => {
          console.error('Error occurred:', error);
          // Xử lý lỗi nếu cần
        }
      });
    }

  }

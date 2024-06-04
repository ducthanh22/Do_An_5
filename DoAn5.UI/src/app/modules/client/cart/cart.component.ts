import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AccountService } from 'src/app/service/account.service';
import { OrderService } from 'src/app/service/order.service';
import { ProductsService } from 'src/app/service/products.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  Carts!: any[];
  quantity!: number
  informationToken:any
  constructor(private productService: ProductsService, private orderService:OrderService,
    private router:Router,private AcountService:AccountService,private MessageSV:MessageService
  ) { }
  ngOnInit() {
    this.Carts = this.productService.GetCart();
    this.informationToken= this.AcountService.decodeToken();
  }


  updateQuantity(newQuantity: number, product: any, index: number) {
    product.quantity = newQuantity;
    this.Carts[index].quantity = product.quantity;
    this.productService.saveCart(this.Carts);
  }
  totalPrice(index: number){
    let total=  this.Carts[index].data.price_product * this.Carts[index].quantity
    return total
  }
  deleteCart(index: number){
    if (index >= 0 && index < this.Carts.length) {
      this.Carts.splice(index,1);
      this.productService.saveCart(this.Carts);
    }
  }
  OpenPay(){
    if(this.informationToken){
      this.Carts && this.Carts.length>0?this.router.navigate(['/client/pay']):this.router.navigate(['/client/Home'])
    }
    else{
      this.MessageSV.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Vui lòng đăng nhập' })
    }
  }
  getTotalPrice(): number {
    let total: number = 0;
    this.Carts.forEach((item: any) => {
      total += item.data.price_product * item.quantity;
    });
    return total;
  }
}

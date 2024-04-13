import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(private productService: ProductsService, private orderService:OrderService,
    private router:Router
  ) { }
  ngOnInit() {
    this.Carts = this.productService.GetCart();
  }

  saveCart(){
    let jsonCart = JSON.stringify(this.Carts);
    sessionStorage.setItem('cart', jsonCart)
    
  }
  updateQuantity(newQuantity: number, product: any, index: number) {
    product.quantity = newQuantity;
    this.Carts[index].quantity = product.quantity;
    this.saveCart();
  }
  totalPrice(index: number){
    let total=  this.Carts[index].data[0].price_product * this.Carts[index].quantity
    return total
  }
  deleteCart(index: number){
    if (index >= 0 && index < this.Carts.length) {
      this.Carts.splice(index);
      this.saveCart();
    }
  }
  OpenPay(){
    if(this.Carts && this.Carts.length>0){
      this.router.navigate(['/client/pay']);
    }
    else{
      this.router.navigate(['/client/Home']);

    }
    
  }
  create(){

  }
}

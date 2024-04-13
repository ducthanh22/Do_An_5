import { Component } from '@angular/core';
import { ProductsService } from 'src/app/service/products.service';

@Component({
  selector: 'app-payproducts',
  templateUrl: './payproducts.component.html',
  styleUrls: ['./payproducts.component.css']
})
export class PayproductsComponent {
  Carts!: any[];
constructor(private productService:ProductsService){}
ngOnInit(){
  this.Carts = this.productService.GetCart();
  console.log(this.Carts)
}
totalPrice(index: number){
  let total=  this.Carts[index].data[0].price_product * this.Carts[index].quantity
  return total
}
getTotalPrice(): number {
  let total: number = 0;
  this.Carts.forEach((item: any) => {
    total += item.data[0].price_product  * item.quantity;
  });
  return total;
}
}

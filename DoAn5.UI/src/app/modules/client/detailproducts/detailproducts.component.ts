import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ProductsDto } from 'src/app/model';
import { ProductsService } from 'src/app/service/products.service';


@Component({
  selector: 'app-detailproducts',
  templateUrl: './detailproducts.component.html',
  styleUrls: ['./detailproducts.component.css']
})
export class DetailproductsComponent {
  id!: string;
  data!: any
  Carts!: any[];
  Size!: any;
  value:number=5;

  first: number = 0;

    rows: number = 10;
  constructor(private route: ActivatedRoute, private productService: ProductsService, private MessageSV:MessageService) { }
  ngOnInit() {
    this.Carts = this.productService.GetCart();
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.id = params['id'];
      }
      this.getbyid(this.id);
    });
  }
  getbyid(id: string) {
    this.productService.getbyid(this.id).subscribe({
      next: (res) => {
        if (res) {
          this.data = res;
        }
      }
    })
  }
  selectSize(data: any) {
    this.Size =[]
    this.Size = data;
  }
  addtocart(data: any) {
    if (this.Size) {
      let idx = this.Carts.findIndex((item: any) => {
        return item.data[0].id == data[0].id && item.size.id == this.Size.id
      });
      if (idx >= 0) {
        this.Carts[idx].quantity += 1;
      } else {
        let cartItem: any = {
          data ,
          size:this.Size,
          quantity: 1,
        };
        this.Carts.push(cartItem)
      }
   this.productService.saveCart(this.Carts)
      this.MessageSV.add({ severity: 'success', summary: 'Thành công', detail: 'Thêm giỏ hàng thành công' })
    }
    else{
      this.MessageSV.add({ severity: 'warn', summary: 'Cảnh báo', detail: 'Vui lòng chọn kích thước' })

    }
  }
  onPageChange(event: any) {
    this.first = event.first;
    this.rows = event.rows;
}
}

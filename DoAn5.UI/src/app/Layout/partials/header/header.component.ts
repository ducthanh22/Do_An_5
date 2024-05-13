import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MenuItem, MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/service/account.service';
import { ProducesService } from 'src/app/service/produces.service';
import { ProductsService } from 'src/app/service/products.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  items: MenuItem[] | undefined;
  itemAccount:MenuItem[] | undefined;
  informationToken!:any;
  isSubMenuOpen: boolean = false;
  menuItems: MenuItem[] = [];
  showMenu: boolean = false;
  Carts:any
  cartUpdateSubscription!: Subscription;
  constructor(private AcountService:AccountService,private messageService:MessageService,private router:Router,private productService:ProductsService) { }

  ngOnInit() {
    this.informationToken= this.AcountService.decodeToken();
    this.Carts = this.productService.GetCart();
    this.cartUpdateSubscription = this.productService.cartUpdated.subscribe(() => {
        this.Carts = this.productService.GetCart();
      });
    this.itemAccount = [
        {
            label: 'Options',
            items: [
                {
                    label: 'Thông Tin',
                    icon: 'pi pi-user',
                    command: () => {
                        this.update();
                    }
                },
                {
                    label: 'Đơn Hàng',
                    icon: 'pi pi-shopping-bag',
                    routerLink: 'client/mycart'
                },
                {
                    label: 'Đăng Xuất',
                    icon: 'pi pi-times',
                    command: () => {
                        this.delete();
                    }
                }
            ]
        } 
    ];

    this.items = [
      {
          label: 'Trang chủ',
      },
    
      {
          label: 'Áo Nam',
          items: [
              {
                  label: 'New',
                  icon: 'pi pi-fw pi-user-plus'
              },
              {
                  label: 'Delete',
                  icon: 'pi pi-fw pi-user-minus'
              },
              
          ]
      },
      {
          label: 'Quần Nam',
          items: [
              {
                  label: 'Edit',
                  icon: 'pi pi-fw pi-pencil',
                 
              },
             
          ]
      },
      {
          label: 'Phụ Kiện',
          items: [
            {
                label: 'Remove',
                icon: 'pi pi-fw pi-calendar-minus'
            }
        ]
      },
      {
        label: 'Giày dép',
        items: [
          {
              label: 'Remove',
              icon: 'pi pi-fw pi-calendar-minus'
          }
      ]
    }
  ];

  }
  ngOnDestroy() {
    if (this.cartUpdateSubscription) {
      this.cartUpdateSubscription.unsubscribe();
    }
  }

  showName(){
    if(this.informationToken){
        return this.informationToken.Username  
    }
    return " Đăng Nhập"
  } 

  update() {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Data Updated' });
}

delete() {
    const token = localStorage.getItem('Token');
    if(token !=null){
        localStorage.removeItem('Token');
        // window.location.reload();
        window.location.href = '/client/Home';
    }
}

}
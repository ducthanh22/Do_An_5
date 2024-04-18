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
                    label: 'Update',
                    icon: 'pi pi-refresh',
                    command: () => {
                        this.update();
                    }
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
          label: 'File',
          icon: 'pi pi-fw pi-file',
          items: [
              {
                  label: 'New',
                  icon: 'pi pi-fw pi-plus',
                  items: [
                      {
                          label: 'Bookmark',
                          icon: 'pi pi-fw pi-bookmark'
                      },
                      {
                          label: 'Video',
                          icon: 'pi pi-fw pi-video'
                      }
                  ]
              },
              {
                  label: 'Delete',
                  icon: 'pi pi-fw pi-trash'
              },
              {
                  separator: true
              },
              {
                  label: 'Export',
                  icon: 'pi pi-fw pi-external-link'
              }
          ]
      },
      {
          label: 'Edit',
          icon: 'pi pi-fw pi-pencil',
          items: [
              {
                  label: 'Left',
                  icon: 'pi pi-fw pi-align-left'
              },
              {
                  label: 'Right',
                  icon: 'pi pi-fw pi-align-right'
              },
              {
                  label: 'Center',
                  icon: 'pi pi-fw pi-align-center'
              },
              {
                  label: 'Justify',
                  icon: 'pi pi-fw pi-align-justify'
              }
          ]
      },
      {
          label: 'Users',
          icon: 'pi pi-fw pi-user',
          items: [
              {
                  label: 'New',
                  icon: 'pi pi-fw pi-user-plus'
              },
              {
                  label: 'Delete',
                  icon: 'pi pi-fw pi-user-minus'
              },
              {
                  label: 'Search',
                  icon: 'pi pi-fw pi-users',
                  items: [
                      {
                          label: 'Filter',
                          icon: 'pi pi-fw pi-filter',
                          items: [
                              {
                                  label: 'Print',
                                  icon: 'pi pi-fw pi-print'
                              }
                          ]
                      },
                      {
                          icon: 'pi pi-fw pi-bars',
                          label: 'List'
                      }
                  ]
              }
          ]
      },
      {
          label: 'Events',
          icon: 'pi pi-fw pi-calendar',
          items: [
              {
                  label: 'Edit',
                  icon: 'pi pi-fw pi-pencil',
                  items: [
                      {
                          label: 'Save',
                          icon: 'pi pi-fw pi-calendar-plus'
                      },
                      {
                          label: 'Delete',
                          icon: 'pi pi-fw pi-calendar-minus'
                      }
                  ]
              },
              {
                  label: 'Archieve',
                  icon: 'pi pi-fw pi-calendar-times',
                  items: [
                      {
                          label: 'Remove',
                          icon: 'pi pi-fw pi-calendar-minus'
                      }
                  ]
              }
          ]
      },
      {
          label: 'Quit',
          icon: 'pi pi-fw pi-power-off'
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
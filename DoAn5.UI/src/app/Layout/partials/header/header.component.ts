import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MenuItem, MessageService } from 'primeng/api';
import { Subscription, forkJoin, map } from 'rxjs';
import { CategoriesDto } from 'src/app/model';
import { Product_typeDto } from 'src/app/model/Product_type';
import { CategoriesService } from 'src/app/service';
import { Product_TypeService } from 'src/app/service/Product_type.service';
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
    itemAccount: MenuItem[] | undefined;
    informationToken!: any;
    isSubMenuOpen: boolean = false;
    menuItems: MenuItem[] = [];
    showMenu: boolean = false;
    Carts: any
    cartUpdateSubscription!: Subscription;
    listCategory: CategoriesDto[] = []
    listProduct_type: Product_typeDto[] = []
    submenu: any[] = []

    constructor(private AcountService: AccountService, private messageService: MessageService, private router: Router, private productService: ProductsService,
        private categoryService: CategoriesService, private product_typeService: Product_TypeService
    ) { }

    ngOnInit() {
        this.informationToken = this.AcountService.decodeToken();
        this.Carts = this.productService.GetCart();
        this.selectCategory();
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

    }
    ngOnDestroy() {
        if (this.cartUpdateSubscription) {
            this.cartUpdateSubscription.unsubscribe();
        }
    }
    selectCategory() {
        this.categoryService.getAll().subscribe(data => {
            this.listCategory = data;
            this.mapCategoriesToItems(this.listCategory);
        });
    }

    mapCategoriesToItems(categories: any[]) {
        const observables = categories.map(category => {
            return this.mapPro_typeToItems(category.id).pipe(
                map(items => ({
                    label: category.name,
                    items: items,
                }))
            );
        });
        forkJoin(observables).subscribe(mappedItems => {
            this.items = mappedItems; // Gán items sau khi đã xử lý tất cả các danh mục
        });
    }

    mapPro_typeToItems(id: string) {
        return this.product_typeService.GetByCategory(id).pipe(
            map(data => {
                return data.map((x: any) => ({
                    label: x.name,
                    command: () => {
                        this.update();
                    }
                }));
            })
        );
    }

    showName() {
        if (this.informationToken) {
            return this.informationToken.Username
        }
        return " Đăng nhập"
    }

    update() {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Data Updated' });
    }

    delete() {
        const token = localStorage.getItem('Token');
        if (token != null) {
            localStorage.removeItem('Token');
            window.location.href = '/client/Home';
        }
    }

}
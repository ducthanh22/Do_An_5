import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MenuItem, MessageService } from 'primeng/api';
import { Subscription, forkJoin, map } from 'rxjs';
import { CategoriesDto } from 'src/app/model';
import { Product_typeDto } from 'src/app/model/Product_type';
import { CategoriesService } from 'src/app/service/categories.service';
import { ShareService } from 'src/app/service/Common/share.service';
import { Product_TypeService } from 'src/app/service/Product_type.service';
import { AccountService } from 'src/app/service/account.service';
import { ProducesService } from 'src/app/service/produces.service';
import { ProductsService } from 'src/app/service/products.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
    Carts: any;
    cartUpdateSubscription!: Subscription;
    listCategory: CategoriesDto[] = [];
    listProduct_type: Product_typeDto[] = [];
    submenu: any[] = [];
    newKeyword: string = '';
    visible: boolean = false;
    formACC!: FormGroup;

    constructor(private AcountService: AccountService, private messageService: MessageService, private router: Router, private productService: ProductsService,
        private categoryService: CategoriesService, private product_typeService: Product_TypeService, private shareService: ShareService, private fb: FormBuilder) { }

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
                            this.infoACC();
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
        this.formACC=this.fb.group({
            id:new FormControl(this.informationToken.Id,Validators.required),
            address:new FormControl(this.informationToken.Address,Validators.required),
            cccd:new FormControl(''),
            phoneNumber:new FormControl(this.informationToken.Phone,Validators.required),
            userName:new FormControl(this.informationToken.Username,Validators.required),
        })
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
                        this.shareService.sendKeyword(x.id);
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

    infoACC() {
        this.visible = true;
        
    }

    delete() {
        const token = localStorage.getItem('Token');
        if (token != null) {
            localStorage.removeItem('Token');
            window.location.href = '/client/Home';
        }
    }
    search() {
        this.shareService.sendKeyword(this.newKeyword);
    }
    updateUser(){
        if(this.formACC){
            this.AcountService.updateUser(this.formACC.value).subscribe({
                next:(res:any)=>{
                    if(res){
                    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Sửa thông tin thành công' });
                    this.visible=false;             
                    }
                }
            })
        }
    }
    close(){
        this.visible=false;
        this.formACC.controls['id'].setValue(this.informationToken.Id);
        this.formACC.controls['address'].setValue(this.informationToken.Address);
        this.formACC.controls['cccd'].setValue('');
        this.formACC.controls['phoneNumber'].setValue(this.informationToken.Phone);
        this.formACC.controls['userName'].setValue(this.informationToken.Username);
    }

}
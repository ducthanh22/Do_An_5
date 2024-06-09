import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Paging, User } from 'src/app/model';
import { AccountService } from 'src/app/service/account.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent {
  loading: boolean = false;
  paging: Paging = { keyword: '', pageIndex: 1, pageSize: 10 };
  keyword: string = "";
  datas: User[] = [];
  Totalcount!: number;
  formACC!: FormGroup;
  Titile2: string = '';
  visible1: boolean = false;
  constructor(private accountService: AccountService, private FB: FormBuilder, private MessageSV: MessageService,
    private route :Router
  ) { }
  ngOnInit() {
    this.onsubmit();
    this.formACC = this.FB.group({
      id: new FormControl("", Validators.required),
      address: new FormControl("", Validators.required),
      phoneNumber: new FormControl("", [Validators.required, Validators.pattern('^[0-9]{10}$')]),
      userName: new FormControl("", Validators.required),
      status: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
    })
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
    this.accountService.GetUser("2", this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.Totalcount = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };
  onsubmit() {
    this.paging.keyword = this.keyword;
    this.accountService.GetUser("2", this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.Totalcount = res.totalFilter;
      },
      error:(error: HttpErrorResponse) => {
        if (error.status === 401) {
         this.route.navigate(['/admin/unauthorized'])
        } else if (error.status === 403) {
         this.route.navigate(['/admin/unauthorized'])
        } else {
         this.route.navigate(['/admin/unauthorized'])
        }
      },
      complete: () => {
        this.loading = false;
      },
    });
  };
  edit(user: any) {
    console.log(user)
    this.visible1 = true;
    this.Titile2 = "Sửa tài khoản";
    this.formACC.controls['userName'].setValue(user.userName);
    this.formACC.controls['address'].setValue(user.address);
    this.formACC.controls['status'].setValue(user.status);
    this.formACC.controls['email'].setValue(user.email);
    this.formACC.controls['phoneNumber'].setValue(user.phoneNumber);
    this.formACC.controls['id'].setValue(user.id);
    this.formACC.controls['roleName'].setValue('');
  }
  updateUser() {
    if (this.formACC) {
      this.accountService.updateUser(this.formACC.value).subscribe({
        next: (res: any) => {
          if (res) {
            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Sửa thông tin thành công' });
            this.close();
            this.onsubmit();
          }
        },
        error:(error: HttpErrorResponse) => {
          debugger
          if (error.status === 401) {
           this.route.navigate(['/admin/unauthorized'])
          } else if (error.status === 403) {
           this.route.navigate(['/admin/unauthorized'])
          } else {
           this.route.navigate(['/admin/unauthorized'])
          }
        }
      })
    }
  }
  close() {
    this.visible1 = false;
    this.formACC.reset();
  }
}

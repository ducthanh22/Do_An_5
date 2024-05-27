import { Component } from '@angular/core';
import { Paging, User } from 'src/app/model';
import { AccountService } from 'src/app/service/account.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent {
  loading:boolean=false;
  paging:Paging={keyword:'',pageIndex:1,pageSize:10};
  keyword:string="";
  datas:User[]=[];
  Totalcount!:number;
constructor(private accountService :AccountService ){}
ngOnInit(){
  this.onsubmit();
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
  this.accountService.GetUser("2",this.paging).subscribe({
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
onsubmit () {
  this.paging.keyword = this.keyword;
  this.accountService.GetUser("2",this.paging).subscribe({
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
}

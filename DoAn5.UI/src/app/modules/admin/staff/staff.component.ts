import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Paging } from 'src/app/model';
import { RoleDto } from 'src/app/model/role';
import { AccountService } from 'src/app/service/account.service';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css']
})
export class StaffComponent implements OnInit {

  constructor(private accountService :AccountService, private FB:FormBuilder, private MessageSV :MessageService){}
  loading:boolean=false;
  paging:Paging={keyword:'',pageIndex:1,pageSize:10};
  keyword:string='';
  datas:any;
  Totalcount!:number;
  formSigIn!:FormGroup;
  status:any[]=[
    {id:'1', name:'Nhân viên'},
    {id:'2', name:'Khách hàng'},
  ];
  visible:boolean=false;
  Titile:string='';
  checkbtn!:boolean;
  listRole:RoleDto[]=[];
  ngOnInit() {
    this.onsubmit();
    this.formSigIn=this.FB.group({
      userName: new FormControl('',Validators.required),
      address: new FormControl('',Validators.required),
      status:new FormControl('',Validators.required),
      email:new FormControl('',Validators.required),
      passwordHash:new FormControl('',Validators.required),
      phoneNumber: new FormControl('',[Validators.required, Validators.pattern('^[0-9]{10}$')]),
      cccd: new FormControl(''),
      roleName: new FormControl('',Validators.required),
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
    this.accountService.GetUser("1",this.paging).subscribe({
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
    this.accountService.GetUser("1",this.paging).subscribe({
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
  Add(){
    this.visible=true;
    this.checkbtn=true;
    this.Titile="Thêm tài khoản";
    this.getRole();
   
  }
  save(){
    if(this.formSigIn){
      this.accountService.Register(this.formSigIn.value).subscribe({
        next:(res)=>{
          if(res){
            this.formSigIn.reset();
            this.onsubmit();
            this.formSigIn.reset();
            this.visible=false;
            this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' })
          }
          else{
            this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Tên người dùng viết liền không dấu' })
          }
        }
      })
    }
    else{
      this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Chưa khớp mật khẩu' })
  
    }
  }
  edit(){
    // this.visible=true;
    this.checkbtn=false;
    this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Chưa khớp mật khẩu' })

  }
  close(){
    this.visible=false;
    this.formSigIn.reset();

  }
  SubmitBtn(){
    this.checkbtn== true? this.save():this.edit;
  }
  getRole(){
    this.accountService.GetAllRoles().subscribe(data=>{
      this.listRole=data;
    })
  }
}

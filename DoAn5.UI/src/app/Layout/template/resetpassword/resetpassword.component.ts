import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ResetPasswordModel } from 'src/app/model';
import { AccountService } from 'src/app/service/account.service';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent {
  value!:string;
  confirmValue!:string;
  password!: string;
  passwordFieldType: string = 'password';
  showPassword: boolean = false;
  email!:string;
  token!:string;
  dataReset:ResetPasswordModel={email:'',token:'',newPassword:''};
constructor(private accountService:AccountService,private activeRoute:ActivatedRoute, private route:Router,private messageService:MessageService){}

  ngOnInit(){
    this.activeRoute.params.subscribe(params => {
      this.token = params['token'];
      this.email = params['email'];
      console.log(this.token)
      console.log(this.email)
    });
  }
  resetPassWord(){
    if(this.value== this.confirmValue){
      this.dataReset.email=this.email;
      this.dataReset.token=this.token;
      this.dataReset.newPassword=this.value;
      this.accountService.ResetPassword(this.dataReset).subscribe({
        next:(data)=>{
          if(data){
            alert('Cập nhật mật khẩu thành công')
            this.route.navigate(['/Login']);
            // this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Cập nhật mật khẩu thành công' });

    
          }
        } 
       
      })
    }else{
      this.messageService.add({ severity: 'error', summary: 'Lỗi', detail: 'Vui lòng nhập trùng mật khẩu ' });

    }
    
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
    this.passwordFieldType = this.showPassword ? 'text' : 'password';
  }
}

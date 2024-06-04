import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AccountService } from 'src/app/service/account.service';
import { drawPoint } from 'src/assets/admin/vendor/chart.js/helpers';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  value: string='' ;
  password: string='' ;
  FormLogin!:FormGroup
  passwordFieldType: string = 'password';
  showPassword: boolean = false;

  constructor(private AccountService:AccountService, private fb:FormBuilder,
    private router: Router, private messageService:MessageService){}
  ngOnInit(){
    this.FormLogin= this.fb.group({
      email:new FormControl('',Validators.required),
      passwordHash:new FormControl('',Validators.required),
    })
  }

login() {
  if (this.FormLogin.valid) {
    const Login = this.FormLogin.value;
    this.AccountService.Login(Login).subscribe({
      next: (res) => {
        if (res != null) {
          const token = res.token;
          localStorage.setItem('Token', token);  
          // // Giải mã phần base64 của token
          const tokenPayload = this.AccountService.decodeToken();
          // // Lấy thông tin từ payload
          const status = tokenPayload.status;      
          this.FormLogin.reset();
          if(status==2){
            this.router.navigate(['/client/Home']);
          }else{
              this.router.navigate(['/admin/dashboard']);
          }
        }
      },
      error: (e) => {
        // console.error(e.errorMessage);
        this.messageService.add({ severity: 'error', summary: 'Lỗi', detail: 'Vui lòng kiểm tra lại tài khoản và mật khẩu' });

      },
    });
  }
}
togglePasswordVisibility(): void {
  this.showPassword = !this.showPassword;
  this.passwordFieldType = this.showPassword ? 'text' : 'password';
}
}

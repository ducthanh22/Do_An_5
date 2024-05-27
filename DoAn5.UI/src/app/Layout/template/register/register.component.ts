import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { AccountService } from 'src/app/service/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  formRegister!:FormGroup
  passwordFieldType: string = 'password';
  showPassword: boolean = false;

constructor(private AccountSV:AccountService,private FB: FormBuilder, private MessageSV:MessageService){}
ngOnInit(){
 this.formRegister=this.FB.group({
  userName: new FormControl('',[
    Validators.required, 
    Validators.pattern('^[a-zA-Z0-9]*$')]),
  address: new FormControl('',Validators.required),
  status:new FormControl('2',Validators.required),
  email:new FormControl('',Validators.required),
  passwordHash:new FormControl('',Validators.required),
  confirmPass:new FormControl('', [Validators.required, this.passwordMatchValidator()]),
  phoneNumber: new FormControl('',[Validators.required, Validators.pattern('^[0-9]{10}$')]),
  roleName: new FormControl('Khách hàng'),
 })
}
passwordMatchValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const password = control.parent?.get('passwordHash');
    const confirmPassword = control.parent?.get('confirmPass');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { 'passwordMismatch': true };
    }
    return null;
  };
}
togglePasswordVisibility(): void {
  this.showPassword = !this.showPassword;
  this.passwordFieldType = this.showPassword ? 'text' : 'password';
}
Register(){
  if(this.formRegister.value.passwordHash==this.formRegister.value.confirmPass){
    this.AccountSV.Register(this.formRegister.value).subscribe({
      next:(res)=>{
        if(res){
          this.formRegister.reset();
          this.MessageSV.add({ severity: 'success', summary: 'Success', detail: 'Thêm thành công' })
        }
        else{
          this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Mật khẩu phải có ít nhất 6 kí tự,có kí tự in hoa và kí tự đặc biệt hoặc thay tên khác' })
        }
      }
    })
  }
  else{
    this.MessageSV.add({ severity: 'error', summary: 'Lỗi', detail: 'Chưa khớp mật khẩu' })
  }
  
}
}

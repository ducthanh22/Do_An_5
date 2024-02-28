import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ForgotPasswordModel } from 'src/app/model';
import { AccountService } from 'src/app/service/account.service';


@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css'],
  providers: [MessageService]
})
export class ForgotpasswordComponent {
  Fogot: ForgotPasswordModel = { email: '' };
  loading: boolean = false;
  constructor(private Account: AccountService,private messageService:MessageService){}
  ngOnInit(){    
  }
  
  FogotPass(){
    this.loading = true;
    if(this.Fogot.email!=null){
      this.Account.FogotPassWord(this.Fogot).subscribe({
        next:(value)=> {
          if(value!=null){
            this.loading = false
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Vui lòng kiểm tra email' });
          }
        },
        error:(e)=>{
          console.error('Error',e)
        }
  
      })
    }
    
  }
}

import { Component } from '@angular/core';
import { drawPoint } from 'src/assets/admin/vendor/chart.js/helpers';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  value: string='' ;
  password: string='' ;
  constructor(){}
  ngOnInit(){}

  ForgotPassword(){
    debugger
    alert("xin vui lòng kiểm tra email")
  }

}

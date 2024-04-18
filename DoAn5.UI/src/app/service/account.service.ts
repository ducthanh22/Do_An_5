
import { environment } from 'src/environment/environment';

import { Injectable } from '@angular/core';
import { Observable, first } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { User, UserDto } from '../model';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(protected _http: HttpClient) { }

  FogotPassWord<ForgotPasswordModel>(data: ForgotPasswordModel): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/ForgotPassword`, data);
  }

  Register(data: User): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/Register`, data).pipe(first());
  }
  Login(data: UserDto): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/Login`, data).pipe(first());
  }
  
  decodeToken() {
    const token = localStorage.getItem('Token');
    if (!token) {
      // throw new Error('Token is not present in localStorage');
      return null;
    }
    const tokenParts = token.split('.');
    if (tokenParts.length !== 3) {
      throw new Error('Invalid token format');
    }
    const payloadBase64 = tokenParts[1];
    const payload = JSON.parse(decodeURIComponent(escape(atob(payloadBase64))));
    return payload;
  }


}

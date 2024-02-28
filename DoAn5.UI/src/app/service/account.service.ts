
import { environment } from 'src/environment/environment';

import { Injectable } from '@angular/core';
import { Observable, first } from 'rxjs';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(protected _http: HttpClient) { }

  
  FogotPassWord<ForgotPasswordModel>(data: ForgotPasswordModel): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/ForgotPassword`, data);
}


}

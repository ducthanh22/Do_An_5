import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { Injectable } from '@angular/core';
import { BaseCommandResponse } from '../model/Common/BaseCommandResponse';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SendEmailService  {
  constructor(private http: HttpClient) {}
  SendEmail(data: FormData): Observable<string> {
    return this.http
      .post(`${environment.apiUrl}/SendEmail/send-email`, data,{ responseType: 'text' })
      .pipe(first());
  }
}

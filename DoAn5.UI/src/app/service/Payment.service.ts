import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { Injectable } from '@angular/core';
import { BaseCommandResponse } from '../model/Common/BaseCommandResponse';
import { Observable, first } from 'rxjs';
import { PaymentDto } from '../model/payment';

@Injectable({
    providedIn: 'root',
})
export class PaymentService {
    constructor(private http: HttpClient) { }
    CreatURL(data: PaymentDto): Observable<string> {
        return this.http
            .post(`${environment.apiUrl}/Payment/createURL`, data, { responseType: 'text' })
            .pipe(first());
    }
    Callback(data: any): Observable<any> {
        return this.http
            .get<any>(`${environment.apiUrl}/Payment/Callback`, { params: data })
            .pipe(first());
    }

}

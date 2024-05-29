import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { CreateOrderDto } from '../model/index';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService extends BaseService<CreateOrderDto> {
  constructor (private http: HttpClient) {
    super(http, `${environment.apiUrl}/Order`);
  }
  GetOrderProduct(id:string,status:number):Observable<any>{
    return this.http
    .get<any>(`${environment.apiUrl}/Order/GetOrderProduct`,  { params: { id, status } })
    .pipe(first());
  }
  destroyOrder(id:string):Observable<any>{
    return this.http
    .get<any>(`${environment.apiUrl}/Order/destroyOrder/${id}`)
    .pipe(first());
  }
}

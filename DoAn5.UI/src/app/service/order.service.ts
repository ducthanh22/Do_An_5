import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { CreateOrderDto, Paging } from '../model/index';
import { Observable, first } from 'rxjs';
import { BaseQuerieResponse } from '../model/Common/BaseQuerieResponse';

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
  Searchbystatus(paging: Paging, status:number): Observable<BaseQuerieResponse<CreateOrderDto>> {
    const params = new HttpParams()
      .set('pageIndex', paging.pageIndex.toString())
      .set('pageSize', paging.pageSize.toString())
      .set('keyword', paging.keyword || '')  
    return this._http
      .get<BaseQuerieResponse<CreateOrderDto>>(`${this.actionUrl}/Search/${status}`, { params })
      .pipe(first());
  }
}

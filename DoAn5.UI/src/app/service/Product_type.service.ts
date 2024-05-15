import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';

import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { Product_typeDto } from '../model/Product_type';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Product_TypeService extends BaseService<Product_typeDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Product_type`);
  }
  GetByCategory(id:string):Observable<any>{
    return this._http.get(`${environment.apiUrl}/Product_type/GetByCategory/${id}`)
  }
}


import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { ColorDto, SizeDto } from '../model/index';
import { Observable } from 'rxjs';
import { countProduct } from '../model/exportBill';

@Injectable({
  providedIn: 'root',
})
export class WarehousedetailService extends BaseService<any> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Detail_warehouse`);
  }
  CountProduct(id:string):Observable<countProduct>{
    return this._http.get<countProduct>(`${environment.apiUrl}/Detail_warehouse/CountProduct/${id}`)
  }
}

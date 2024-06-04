
import { HttpClient, HttpParams } from '@angular/common/http';
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
  CountProduct(id:string,idSize:string):Observable<countProduct>{
    const params = new HttpParams()
    .set('id', id.toString())
    .set('idSize', idSize.toString())
    return this._http.get<countProduct>(`${environment.apiUrl}/Detail_warehouse/CountProduct`,{params})
  }
}

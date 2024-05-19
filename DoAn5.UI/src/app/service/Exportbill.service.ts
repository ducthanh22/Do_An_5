import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';

import { CreateExportbillDto, GetDetail_exportbillDto, countProduct } from '../model/exportBill';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class exportBillService extends BaseService<CreateExportbillDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Exportbill`);
  }
  countProduct(id : string): Observable<countProduct> {
    return this._http.get<countProduct>(`${environment.apiUrl}/Detail_exportbill/CountProduct/${id}`).pipe(first());
  }
  GetDetail(id : string): Observable<GetDetail_exportbillDto[]> {
    return this._http.get<GetDetail_exportbillDto[]>(`${environment.apiUrl}/Detail_exportbill/GetByid/${id}`).pipe(first());
  }
}

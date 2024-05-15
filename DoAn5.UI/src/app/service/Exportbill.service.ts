import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';

import { CreateExportbillDto, countProduct } from '../model/exportBill';
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
}

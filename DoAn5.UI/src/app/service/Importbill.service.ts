import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';

import { CreateImportbillDto, GetDetail_importbillDto } from '../model/importBill';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class importBillService extends BaseService<CreateImportbillDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Importbill`);
  }
  GetDetail(id : string): Observable<GetDetail_importbillDto[]> {
    return this._http.get<GetDetail_importbillDto[]>(`${environment.apiUrl}/Detail_importbill/GetByid/${id}`).pipe(first());
  }
}

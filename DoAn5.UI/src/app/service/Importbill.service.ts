import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';

import { CreateImportbillDto } from '../model/importBill';

@Injectable({
  providedIn: 'root',
})
export class importBillService extends BaseService<CreateImportbillDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Importbill`);
  }
}

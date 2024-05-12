import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { Observable, first } from 'rxjs';
import { SaleDto } from '../model/sale';

@Injectable({
  providedIn: 'root',
})
export class SaleService extends BaseService<SaleDto> {
  constructor (private http: HttpClient) {
    super(http, `${environment.apiUrl}/Sale`);
  }
  UpdateSalesPrices():Observable<string>{
    return this.http
    .get(`${environment.apiUrl}/Sale/UpdateSalesPrices`,{ responseType: 'text' })
    .pipe(first());
  }
}

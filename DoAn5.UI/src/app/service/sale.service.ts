import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { Observable, first } from 'rxjs';
import { GetSaleDto, SaleDto } from '../model/sale';

@Injectable({
  providedIn: 'root',
})
export class SaleService extends BaseService<SaleDto> {
  constructor (private http: HttpClient) {
    super(http, `${environment.apiUrl}/Sale`);
  }
  UpdateSalesPrices():Observable<number>{
    return this.http
    .get<number>(`${environment.apiUrl}/Sale/UpdateSalesPrices`)
    .pipe(first());
  }
  getSale(keyword: string, active: number): Observable<GetSaleDto[]> {
    const params = { keyword, active: active.toString() };
    return this.http
      .get<GetSaleDto[]>(`${environment.apiUrl}/Sale/GetSale`, { params })
      .pipe(first());
  }
}

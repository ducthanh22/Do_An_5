import { Injectable } from '@angular/core';
import { BaseService } from './Common/base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { ProductsDto, UpLoadFile } from '../model';
import { Observable, first } from 'rxjs';
import { BaseCommandResponse } from '../model/Common/BaseCommandResponse';

@Injectable({
  providedIn: 'root'
})
export class ProductsService extends BaseService<ProductsDto>{
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Product`);
  }
  Upload(data: FormData): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${environment.apiUrl}/Product/UploadFile`, data)
      .pipe(first());
  }
}

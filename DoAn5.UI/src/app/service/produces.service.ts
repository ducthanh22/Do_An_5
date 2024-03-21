import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { Produces } from '../model/index';
import { BaseCommandResponse } from '../model/Common/BaseCommandResponse';
import { Observable, first } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProducesService extends BaseService<Produces> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Produces`);
  }
  Upload(data: FormData): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${environment.apiUrl}/Produces/UploadFile`, data)
      .pipe(first());
  }
}

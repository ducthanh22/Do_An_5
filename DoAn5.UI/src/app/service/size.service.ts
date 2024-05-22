import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { ColorDto, SizeDto } from '../model/index';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SizeService extends BaseService<SizeDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Size`);
  }
  Getbyidproduct(id:string):Observable<SizeDto[]>{
    return this._http.get<SizeDto[]>(`${environment.apiUrl}/Size/Getbyidproduct/${id}`)
  }
}

import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { ColorDto, SizeDto } from '../model/index';
import { Observable } from 'rxjs';
import { StatisticalDto } from '../model/statistical';

@Injectable({
  providedIn: 'root',
})
export class StatisticalService extends BaseService<SizeDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Statistical`);
  }
  Darhboarsh(start:string, end:string):Observable<StatisticalDto>{
    const params = new HttpParams()
    .set('start', start.toString())
    .set('end', end.toString())
    return this._http.get<StatisticalDto>(`${environment.apiUrl}/Statistical/Darhboarsh`,{params})
  }
}

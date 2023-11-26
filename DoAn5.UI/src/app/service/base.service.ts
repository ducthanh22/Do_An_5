
import { Observable, first } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Paging } from '../model/Paging';
import { BaseQuerieResponse } from '../model/Common/BaseQuerieResponse';


export class BaseService<T> {

  constructor(protected _http: HttpClient, protected actionUrl: string) { }
  
  getAll(): Observable<T[]> {
    return this._http.get<T[]>(`${this.actionUrl}/GetAll`).pipe(first());
  }
  Search(paging: Paging): Observable<BaseQuerieResponse<T>> {
    const params = new HttpParams()
      .set('pageIndex', paging.pageIndex.toString())
      .set('pageSize', paging.pageSize.toString())
      .set('keyword', paging.keyword || '')  // Đảm bảo rằng keyword không bị undefined
      .set('orderBy', paging.orderBy || ''); // Đảm bảo rằng orderBy không bị undefined
    return this._http
      .get<BaseQuerieResponse<T>>(`${this.actionUrl}/Search`, { params })
      .pipe(first());
  }
}

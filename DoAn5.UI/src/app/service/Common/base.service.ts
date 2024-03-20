
import { Observable, first } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Paging } from '../../model/Paging';
import { BaseQuerieResponse } from '../../model/Common/BaseQuerieResponse';
import { BaseCommandResponse } from '../../model/Common/BaseCommandResponse';


export class BaseService<T> {

  constructor(protected _http: HttpClient, protected actionUrl: string) { }
  
  getAll(): Observable<T[]> {
    return this._http.get<T[]>(`${this.actionUrl}/GetAll`).pipe(first());
  }
  getbyid(id : string): Observable<T[]> {
    return this._http.get<T[]>(`${this.actionUrl}/GetByid/${id}`).pipe(first());
  }
  Search(paging: Paging): Observable<BaseQuerieResponse<T>> {
    const params = new HttpParams()
      .set('pageIndex', paging.pageIndex.toString())
      .set('pageSize', paging.pageSize.toString())
      .set('keyword', paging.keyword || '')  // Đảm bảo rằng keyword không bị undefined
      // .set('orderBy', paging.orderBy || ''); // Đảm bảo rằng orderBy không bị undefined
    return this._http
      .get<BaseQuerieResponse<T>>(`${this.actionUrl}/Search`, { params })
      .pipe(first());
  }
  create<T>(data: T): Observable<BaseCommandResponse> {
    return this._http
      .post<BaseCommandResponse>(`${this.actionUrl}/create`, data)
      .pipe(first());
  }
  Update<T>(data: T): Observable<BaseCommandResponse> {
    return this._http
      .put<BaseCommandResponse>(`${this.actionUrl}/update`, data)
      .pipe(first());
  }
  Delete(id : string): Observable<T[]> {
    return this._http.delete<T[]>(`${this.actionUrl}/Delete/${id}`).pipe(first());
  }
}

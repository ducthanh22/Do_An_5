import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { Observable, first } from 'rxjs';
import { GetRatingDto, RatingDto } from '../model/rating';
import { BaseQuerieResponse } from '../model/Common/BaseQuerieResponse';

@Injectable({
  providedIn: 'root',
})
export class RatingService extends BaseService<RatingDto> {
  constructor (private http: HttpClient) {
    super(http, `${environment.apiUrl}/Rating`);
  }
  GetByProduct(id:string,page:number,pageSize:number):Observable<BaseQuerieResponse<GetRatingDto>>{
    return this.http
    .get<BaseQuerieResponse<GetRatingDto>>(`${environment.apiUrl}/Rating/GetByProduct`,  { params: { id, page,pageSize } })
    .pipe(first());
  }
}

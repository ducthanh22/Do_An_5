import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { Produces } from '../model/index';

@Injectable({
  providedIn: 'root',
})
export class ProducesService extends BaseService<Produces> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Produces`);
  }
}

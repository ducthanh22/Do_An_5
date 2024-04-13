import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { CreateOrderDto } from '../model/index';

@Injectable({
  providedIn: 'root',
})
export class OrderService extends BaseService<CreateOrderDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Order`);
  }
}

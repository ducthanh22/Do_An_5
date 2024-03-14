import { Injectable } from '@angular/core';
import { BaseService } from './Common/base.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { ProductsDto } from '../model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService extends BaseService<ProductsDto>{
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Product`);
  }
}

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { ColorDto, SizeDto } from '../model/index';

@Injectable({
  providedIn: 'root',
})
export class SizeService extends BaseService<SizeDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Size`);
  }
}

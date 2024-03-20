import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';
import { BaseService } from './Common/base.service';
import { Injectable } from '@angular/core';
import { ColorDto } from '../model/index';

@Injectable({
  providedIn: 'root',
})
export class ColorService extends BaseService<ColorDto> {
  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Color`);
  }
}

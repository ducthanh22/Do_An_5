
import { environment } from 'src/environment/environment';

import { Injectable } from '@angular/core';
import { Observable, first } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Paging, ResetPasswordModel, User, UserDto, updateUserDto } from '../model';
import { ClaimDto, CreateRoleDto, RoleDto } from '../model/role';
import { BaseQuerieResponse } from '../model/Common/BaseQuerieResponse';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(protected _http: HttpClient) { }

  FogotPassWord<ForgotPasswordModel>(data: ForgotPasswordModel): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/ForgotPassword`, data);
  }

  Register(data: User): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/Register`, data).pipe(first());
  }
  ResetPassword(data: ResetPasswordModel): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/ResetPassword`, data).pipe(first());
  }
  Login(data: UserDto): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/Login`, data).pipe(first());
  }
  CreateRole(data: CreateRoleDto): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/CreateRole`, data).pipe(first());
  }
  UpdateRole(data: CreateRoleDto): Observable<any> {
    return this._http.post<any>(`${environment.apiUrl}/Account/UpdateRole`, data).pipe(first());
  }

  GetAllRoles(): Observable<RoleDto[]> {
    return this._http.get<RoleDto[]>(`${environment.apiUrl}/Account/GetAllRoles`).pipe(first());
  }
  getClaimByIdRole(id: string): Observable<CreateRoleDto> {
    return this._http.get<CreateRoleDto>(`${environment.apiUrl}/Account/getClaimByIdRole/${id}`).pipe(first());
  }
  DeleteRole(id: string): Observable<boolean> {
    return this._http.delete<boolean>(`${environment.apiUrl}/Account/DeleteRole/${id}`).pipe(first());
  }
  GetUser(status: string, paging: Paging): Observable<BaseQuerieResponse<any>> {
    const params = new HttpParams()
      .set('pageIndex', paging.pageIndex.toString())
      .set('pageSize', paging.pageSize.toString())
      .set('keyword', paging.keyword || '')
    return this._http.get<BaseQuerieResponse<any>>(`${environment.apiUrl}/Account/GetUser/${status}`, { params }).pipe(first());
  }

  updateUser(data: updateUserDto): Observable<updateUserDto> {
    return this._http.patch<updateUserDto>(`${environment.apiUrl}/Account/update`, data).pipe(first());
  }

  decodeToken() {
    const token = localStorage.getItem('Token');
    if (!token) {
      return null;
    }
    const tokenParts = token.split('.');
    if (tokenParts.length !== 3) {
      throw new Error('Invalid token format');
    }
    const payloadBase64 = tokenParts[1];
    // Chuyển đổi từ Base64URL sang Base64 tiêu chuẩn
    const base64 = payloadBase64.replace(/-/g, '+').replace(/_/g, '/');
    const payload = JSON.parse(decodeURIComponent(escape(atob(base64))));
    return payload;
  }


}

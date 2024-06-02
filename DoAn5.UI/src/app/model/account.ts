import { BasedbDto } from "./Common/BaseDto";

export interface UserDto {
    email: string;
    passwordHash: string;
}

export interface GennToken {
    userName: string;
    token: string;
}

export interface ResetPasswordModel {
    email: string;
    token: string;
    newPassword: string;
}

export interface ForgotPasswordModel {
    email: string;
}



export interface User  {
    address: string;
    status: string | null;
    activeFlag: number | null;
    email: string;
    passwordHash: string;
    phoneNumber: string;
    cccd: string;
}
export interface updateUserDto {
    id: string;
    address: string;
    cCCD: string | null;
    phoneNumber: string;
    userName: string;
}
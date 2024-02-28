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
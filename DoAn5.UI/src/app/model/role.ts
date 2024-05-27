import { BasedbDto } from "./Common/BaseDto";

export interface ClaimDto {
    type: string;
    value: string;
}

export interface RoleDto extends BasedbDto {
    name: string;
}

export interface CreateRoleDto {
    role: RoleDto;
    roleClaims: ClaimDto[];
}
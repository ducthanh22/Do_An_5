import { BasedbDto } from "./Common/BaseDto";

export interface ColorDto extends BasedbDto {
    nameColor: string;
    colorformat: string;
}

export interface SizeDto extends BasedbDto {
    idproduct:string;
    nameSize: string;
}


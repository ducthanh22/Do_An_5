import { BasedbDto } from "./Common/BaseDto";


export interface Product_typeDto extends BasedbDto {
    name: string | null;
    idcategories: string;
}
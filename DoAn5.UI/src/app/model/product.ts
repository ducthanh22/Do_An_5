import { BasedbDto } from "./Common/BaseDto";

export interface ProductsDto extends BasedbDto {
    name: string;
    idcategories: string;
    idproduces: string;
    describe: string;
    nameColor: string;
    price_product: number;
    nameSize: string;
    image: string;
}
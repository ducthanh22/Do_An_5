import { BasedbDto } from "./Common/BaseDto";
import { SizeDto } from "./color";



export interface ProductsDto extends BasedbDto {
    name: string;
    idcategories: string;
    idproduces: string;
    describe: string;
    image: string;
    idcolor: string;
    price_product: number | null;
    listsize: SizeDto[]
}
export interface GetProductsDto extends BasedbDto {
    name: string;
    idcategories: string;
    namecategory: string;
    nameProduces: string;
    idproduces: string;
    describe: string;
    image: string;
    namecolor: string;
    price_product: number | null;
    idcolor: string;
    listSize: SizeDto[];
}

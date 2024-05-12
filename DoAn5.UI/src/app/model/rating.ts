import { BasedbDto } from "./Common/BaseDto";

export interface RatingDto  {
    id_Order: string;
    id_product: string;
    id_customer: string;
    evaluate: number;
    comment: string;
    status: number;
}

export interface GetRatingDto extends BasedbDto {
    id_Order: string;
    id_product: string;
    id_customer: string;
    evaluate: number;
    comment: string;
    status: number;
    username: string
}
export interface CreateRatingDto {
    listRating: RatingDto[];
}

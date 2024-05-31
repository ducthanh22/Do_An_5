export interface StatisticalDto {
    totalOrder: number;
    totalCustomer: number;
    totalProduct: number;
    expected_Revenue: number;
    totalRevenue: number;
    totalMonthlyrevenue: number;
    totalRating: number;
    totalRevenueCategory: number;
    monthlyrevenue: Monthly_revenue[];
    categoryRevenue: Category_revenue[];
    rating_rate: rating_rate[];
}

export interface Monthly_revenue {
    date: string;
    revenue: number;
}

export interface Category_revenue {
    nameCategory: string;
    revenue: number;
}

export interface rating_rate {
    name: number;
    quantity: number;
}
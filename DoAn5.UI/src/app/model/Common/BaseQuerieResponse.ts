export interface BaseQuerieResponse<T> {
    pageIndex: number;
    pageSize: number;
    keyword: string | null;
    keynumber: number | null;
    totalFilter: number;
    data: T[];
}
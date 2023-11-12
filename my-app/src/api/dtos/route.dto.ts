export interface RouteDTO{
    routeId: number;
    distance: number;
    duration: number;
    polyline: string;
    segments: RouteOrderDTO[];
}

export interface RouteOrderDTO{
    order: number;
    address : string;
    userId: number;
}

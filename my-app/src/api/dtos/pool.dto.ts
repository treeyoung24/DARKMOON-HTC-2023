export interface PoolDTO {
  hostId: number;
  poolSize: number;
  arrivalTime: string;
  destination: string;
}

export interface PoolDTOReturn {
  routeId: number;
  poolId: number;
  hostId: number;
  poolSize: number;
  arrivalTime: string;
  destination: string;
}

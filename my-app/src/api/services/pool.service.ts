import { PassengerDTOReturn } from "../dtos/passenger.dto";
import { PoolDriverMyPoolDTO, PoolDriverMyPoolDTOReturn } from "../dtos/pool-driver-mypool.dto";
import { PoolPassengerMyViewDTO } from "../dtos/pool-passenger-myview.dto";
import { PoolDTO } from "../dtos/pool.dto";

const rootUrl = 'https://localhost:7113/api/';

export const ENDPOINTS = {
    user: 'Users/',
    route: 'Routes/',
    requestJoin: 'RequestJoins/',
    pool: 'Pools/',
    passenger:'Passengers/',
    driver: 'Drivers/'
}

interface ReturnDTO<T>{
    data: T;
}

const createApiRequestFunction = (method: string, endpointSuffix?: string) => {
    return async ( data?: any): Promise<any> => {
        try {
            const requestOptions: RequestInit = {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                },
                body: method === 'POST' && data ? JSON.stringify(data) : undefined,
            };

            if(method == 'GET') endpointSuffix = endpointSuffix + data;
            const response = await fetch(rootUrl + endpointSuffix, requestOptions);

            if (!response.ok) {
                throw new Error(`Request failed. Status: ${response.status}`);
            }

            const res = await response.json();
            return res;
        } catch (error) {
            console.error(`Error making request to ${endpointSuffix}:`, error);
            throw error; // Rethrow the error to be caught by the caller
        }
    };
};


// Generate dynamic API request
// export const postUser = createApiRequestFunction('POST')(ENDPOINTS.user);
// export const getUser = createApiRequestFunction('GET')(ENDPOINTS.user);
// export const postRoute = createApiRequestFunction('POST')(ENDPOINTS.route);
// Create Pool
export const postPool: (pool: PoolDTO) => Promise<any> = createApiRequestFunction('POST', ENDPOINTS.pool);

// Get My Pool - Driver
export const getMyPool: (id: number) => Promise<ReturnDTO<PoolDriverMyPoolDTOReturn[]>> = createApiRequestFunction('POST', ENDPOINTS.pool + 'GetDriverPools');

export const getPassengerPool: (passId: number) => Promise<ReturnDTO<PassengerDTOReturn[]>> = createApiRequestFunction('GET', ENDPOINTS.pool + 'GetPassengerPools');
export const getPoolDetail: (poolId: number) => Promise<any> = createApiRequestFunction('GET', ENDPOINTS.pool);

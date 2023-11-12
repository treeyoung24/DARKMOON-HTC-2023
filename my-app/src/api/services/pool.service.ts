import { PassengerDTOReturn } from "../dtos/passenger.dto";
import { PoolDriverMyPoolDTO, PoolDriverMyPoolDTOReturn } from "../dtos/pool-driver-mypool.dto";
import { PoolPassengerMyViewDTO } from "../dtos/pool-passenger-myview.dto";
import { PoolDTO } from "../dtos/pool.dto";
import { PoolViewDTOReturn } from "../dtos/poolview.dto";

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
    return async (data?: any): Promise<any> => {
        try {
            let url = rootUrl + endpointSuffix;

            if (method === 'GET' && data) {
                url = url + '?id=' + data;  // Modify this based on your endpoint structure
                console.log(url);
            }

            const requestOptions: RequestInit = {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                },
                body: method === 'POST' && data ? JSON.stringify(data) : undefined,
            };

            const response = await fetch(url, requestOptions);

            if (!response.ok) {
                throw new Error(`Request failed. Status: ${response.status}`);
            }

            const res = await response.json();
            return res;
        } catch (error) {
            console.error(`Error making request to ${rootUrl + endpointSuffix}:`, error);
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
export const getMyPool: (id: number) => Promise<ReturnDTO<PoolDriverMyPoolDTOReturn[]>> = createApiRequestFunction('GET', ENDPOINTS.pool + 'GetDriverPools');

export const getPassengerPool: (passId: number) => Promise<ReturnDTO<PassengerDTOReturn[]>> = createApiRequestFunction('GET', ENDPOINTS.pool + 'GetPassengerPools');
export const getPoolDetail: (poolId: number) => Promise<ReturnDTO<PoolViewDTOReturn>> = createApiRequestFunction('GET', ENDPOINTS.pool);

// Get all join request of a pool
export const getRequestPool: (passId: number) => Promise<any> = createApiRequestFunction('GET', ENDPOINTS.requestJoin + 'GetPassengerRequest');

// Get all pending requests of a pool
export const getPendingRequests: (poolId: number) => Promise<any> = createApiRequestFunction('GET', ENDPOINTS.requestJoin + 'GetAllPendingRequest');
export const getRouteUsingPoolId: (poolId: number) => Promise<any> = createApiRequestFunction('GET', ENDPOINTS.route);

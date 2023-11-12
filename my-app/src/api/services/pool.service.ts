import { PoolDriverMyPoolDTO } from "../dtos/pool-driver-mypool.dto";
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

const createApiRequestFunction = (method: string, endpointSuffix?: string) => {
    return async ( data?: any): Promise<any> => {
        try {
            const requestOptions: RequestInit = {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                },
                body: data ? JSON.stringify(data) : undefined,
            };

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
export const postPool: (pool: PoolDTO) => Promise<any> = createApiRequestFunction('POST');

// Get My Pool - Driver
export const getMyPool: (pool: PoolDriverMyPoolDTO) => Promise<any> = createApiRequestFunction('POST', ENDPOINTS.pool + 'GetDriverPools');


export const getRequestPool: (pool: PoolPassengerMyViewDTO) => Promise<any> = createApiRequestFunction('POST', ENDPOINTS.pool + 'GetPassengerPools');

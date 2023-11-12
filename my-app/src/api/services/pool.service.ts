import { PoolDriverMyPoolDTO } from "../dtos/pool-driver-mypool.dto";
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
    return async (endpoint: string, data?: any): Promise<any> => {
        try {
            const requestOptions: RequestInit = {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                },
                body: data ? JSON.stringify(data) : undefined,
            };

            const response = await fetch(rootUrl + endpoint + endpointSuffix, requestOptions);

            if (!response.ok) {
                throw new Error(`Request failed. Status: ${response.status}`);
            }

            const res = await response.json();
            return res;
        } catch (error) {
            console.error(`Error making request to ${endpoint}:`, error);
            throw error; // Rethrow the error to be caught by the caller
        }
    };
};


// Generate dynamic API request
// export const postUser = createApiRequestFunction('POST')(ENDPOINTS.user);
// export const getUser = createApiRequestFunction('GET')(ENDPOINTS.user);
// export const postRoute = createApiRequestFunction('POST')(ENDPOINTS.route);
export const postPool: (endpoint: string, pool: PoolDTO) => Promise<any> = createApiRequestFunction('POST');
export const getMyPool: (endpoint: string, pool: PoolDriverMyPoolDTO) => Promise<any> = createApiRequestFunction('POST', 'GetDriverPools');

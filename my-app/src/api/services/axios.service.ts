import axios from 'axios'

export const BASE_URL = 'https://localhost:7113/';

export const ENDPOINTS = {
    user: 'Users',
    route: 'Routes',
    requestJoin: 'RequestJoins',
    pool: 'Pools',
    passenger:'Passengers',
    driver: 'Drivers'
}

export const createAPIEndpoint = (endpoint: string) => {

    let url = BASE_URL + 'api/' + endpoint + '/';
    return {
        fetch: () => axios.get(url),
        fetchById: (id: number) => axios.get(url + id),
        // post: newRecord => axios.post(url, newRecord),
        // customizePost: (newRecord, path) => axios.post(url + path, newRecord),
        // log: info => axios.post(url + 'Login', info),
        // put: (id, updatedRecord) => axios.put(url + id, updatedRecord),
        // delete: id => axios.delete(url + id),
    }
}
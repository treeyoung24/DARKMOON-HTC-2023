import { PoolDTO } from "../dtos/pool.dto";


const rootUrl = 'https://localhost:7113/api';

export async function postPool(pool: PoolDTO): Promise<PoolDTO>{

    const response = await fetch(rootUrl + '/Pools', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(pool),
    });
    const res = await response.json();
    return res;
}

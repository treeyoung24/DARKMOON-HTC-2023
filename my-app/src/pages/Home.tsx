import React from 'react';
import { Button } from '../components/Button';
import "../styles/component.sass";
import Logo from "../OIG-removebg-preview.png"
import { PoolCard } from '../components/PoolCard';
import { Datepicker } from 'flowbite-react';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { createAPIEndpoint, ENDPOINTS } from "../api/services/axios.service";
import { PoolPassengerMyViewDTOReturn } from "../api/dtos/pool-passenger-myview.dto";

export function Home() {
    const [searches, setSearches] = React.useState<PoolPassengerMyViewDTOReturn[]>([]);

    React.useEffect(() => {
        
    }, []);

    return (
        <div className="home">
            <div className="form">
                <p className="mx-10">Search Pool</p>
                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Pick-up Location</label>
                    <input
                        type="text"
                        className="input"
                        id="wifi-name"
                        value="856 Campus Pl NW, Calgary, AB T2N 4V8"
                    ></input>
                </div>

                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Drop-off Location</label>
                    <input
                        type="text"
                        className="input"
                        id="text-input"
                        value="Aldred Centre, CA416, 1301 Trans-Canada Hwy, Calgary, AB T2M 4W7"
                    ></input>
                </div>
                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Time Arrival</label>
                    <Datepicker />
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <TimePicker label="Basic time picker" />
                    </LocalizationProvider>
                </div>
                <Button type="contained" className="button-form" onClick={() => createAPIEndpoint(ENDPOINTS.pool)
        .getPoolByDestination
        ('https://localhost:7113/api/Pools/GetPoolByDestination?address=588%20Aero%20Dr%20NE%20%23106%2C%20Calgary%2C%20AB%20T2E%207Y4')
        .then((res: PoolPassengerMyViewDTOReturn[]) => {
            console.log(res);
            setSearches(searches => ([...searches, ...res.data]));
        })}>Search</Button>
            </div>

                    {
                        searches.map((search: PoolPassengerMyViewDTOReturn) => {

                            return <PoolCard pickupTime={search.arrivalTime} pickupLocation="" arrivalTime={search.arrivalTime} co2Emmission={search.co2} numStop={search.stops} fees={search.fee}/>
                        
                        })
                    } 

        </div>
    )
}


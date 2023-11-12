import React from 'react';
import { Button } from '../components/Button';
import "../styles/component.sass";
import Logo from "../OIG-removebg-preview.png"
import { PoolCard } from '../components/PoolCard';
import { Datepicker } from 'flowbite-react';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';


export function Home() {

    return (
        <div className="home">
            <div className="form">
                <p>Search Pool</p>
                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Pick-up Location</label>
                    <input
                        type="text"
                        className="input"
                        id="wifi-name"
                    ></input>
                </div>

                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Drop-off Location</label>
                    <input
                        type="text"
                        className="input"
                        id="text-input"
                    ></input>
                </div>
                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Time Arrival</label>
                    <Datepicker />
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <TimePicker label="Basic time picker" />
                    </LocalizationProvider>
                </div>
                <Button type="contained" className="button-form" onClick={() => console.log('Create Pool clicked')}>Search</Button>
            </div>
            <PoolCard pickupTime="10:00" pickupLocation="1234" arrivalTime="11:00" co2Emmission={100} numStop={2} fees={10} />
        </div>
    )
}


import { Datepicker } from "flowbite-react"
import { Button } from "../components/Button"
import "../styles/component.sass";
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { RequestTable } from "../components/RequestTable";
import { DriverMap } from "./DriverMap";
import { useParams } from "react-router-dom";
import { getPoolDetail } from "../api/services/pool.service";
import { useEffect } from "react";
import React from "react";

export function ManageDetailedPool() {
    const { poolId } = useParams();
    const [destination, setDestination] = React.useState<string>("");
    const [poolSize, setPoolSize] = React.useState<number>(0);
    const [arrivalTime, setArrivalTime] = React.useState<string>("");
    const [arrivalDate, setArrivalDate] = React.useState<string>("");
    const [startingPoint, setStartingPoint] = React.useState<string>("");




    useEffect(() => {
        getPoolDetail(Number(poolId)).then((res) => {
            console.log(res.data);
            setDestination(res.destination);
            setPoolSize(res.poolSize);
            setArrivalTime(res.arrivalTime);
            setStartingPoint(res.startingPoint);
        })
    }
        , [poolId]);

    return (
        <div>
            <div className="manage-home">
                <div className="form">
                    <p className="mx-10">Create Pool</p>
                    <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                        <label className="text-black">Starting Location</label>

                        <input
                            type="text"
                            className="input"
                            id="wifi-name"
                            value={startingPoint}
                        ></input>
                    </div>

                    <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                        <label className="text-black">Destination</label>
                        <input
                            type="text"
                            className="input"
                            id="text-input"
                            value={destination}
                        ></input>
                    </div>
                    <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                        <label className="text-black">Pool Size</label>
                        <input
                            type="text"
                            className="input"
                            id="text-input"
                            value={poolSize}
                        ></input>
                    </div>
                    <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                        <label className="text-black">Time Arrival</label>
                        <Datepicker />
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <TimePicker label="Basic time picker" />
                        </LocalizationProvider>
                    </div>
                    <Button type="contained" className="button-form" onClick={() => console.log('Create Pool clicked')}>Create</Button>
                </div>
                <RequestTable />
            </div>
            <div className="flex flex-col items-center mt-20">
                <p className="text-lg">Navigation Map</p>
                <DriverMap />
            </div>
        </div>

    )
}

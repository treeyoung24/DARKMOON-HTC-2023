import { Datepicker } from "flowbite-react"
import { Button } from "../components/Button"
import "../styles/component.sass";
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { useEffect } from "react";
import { PoolDTO } from "../api/dtos/pool.dto";
import { postPool } from "../api/services/pool.service";

export function CreatePool() {
    useEffect(() => {
            const dto:PoolDTO = {
                hostId: 1,
                poolSize: 1,
                destination: "588 Aero Dr NE #106, Calgary, AB T2E 7Y4",
                arrivalTime: "2023-11-15T15:01:23.045123456Z",
            }

            postPool(dto).then((res) => {
                console.log(res);
            })}, [])
    return (
        <div className="home">
            <div className="form">
                <p>Create Pool</p>
                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Starting Location</label>

                    <input
                        type="text"
                        className="input"
                        id="wifi-name"
                    ></input>
                </div>

                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Destination</label>
                    <input
                        type="text"
                        className="input"
                        id="text-input"
                    ></input>
                </div>
                <div className="my-4 mx-4 flex flex-col items-start md:mx-10">
                    <label className="text-black">Pool Size</label>
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
                <Button type="contained" className="button-form" onClick={() => console.log('Create Pool clicked')}>Create</Button>
            </div>
        </div>
    )
}

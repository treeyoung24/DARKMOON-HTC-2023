import { Button } from "./Button";
import "../styles/component.sass";

interface PoolCardProps {
    pickupTime: string;
    pickupLocation: string;
    arrivalTime: string;
    co2Emmission: number;
    numStop: number;
    fees: number;
}


export function PoolCard(props: PoolCardProps) {
    return (
        <div className="pool-card">
            <div className="time">
                <p className="header">Pickup Time - Arrival Time</p>
                <p>{props.pickupTime}-{props.arrivalTime}</p>
            </div>
            <div className="emission">
                <p className="header">CO2 Emission</p>
                <p>{props.co2Emmission}g CO2</p>
            </div>
            <div className="stop">
                <p className="header">Stops</p>
                <p>{props.numStop}</p>
            </div>
            <div className="fees">
                <p className="header">Fees</p>
                <p>${props.fees}</p>
            </div>
            <Button type="contained" className="button-form" onClick={() => console.log('Create Pool clicked')}>Request</Button>
        </div>
    );
}

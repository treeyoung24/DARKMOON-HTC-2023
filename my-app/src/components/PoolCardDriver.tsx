import { Button } from "./Button";
import "../styles/component.sass";
import { useNavigate } from "react-router-dom";

interface PoolCardProps {
    id: string;
    startingTime: string;
    pickupLocation: string;
    arrivalTime: string;
    poolSize: number;
    availableSlot: number;
    totalEarn: number;
}

export function PoolCardDriver(props: PoolCardProps) {
     const navigate = useNavigate();
    return (
        <div className="pool-card">
            <div className="time">
                <p className="header">Starting - Arrival Time</p>
                <p>{props.startingTime}-{props.arrivalTime}</p>
            </div>
            <div className="poolSize">
                <p className="header">Available Slots</p>
                <p>{props.poolSize}</p>
            </div>
            <div className="availableSlot">
                <p className="header">Stops</p>
                <p>{props.availableSlot}</p>
            </div>
            <div className="totalEarn">
                <p className="header">Total Earn</p>
                <p>${props.totalEarn}</p>
            </div>
            <div className="button-container">
                <Button type="contained" className="button-form" onClick={() => navigate(`/manage-pool/${props.id}`)}>Edit Pool</Button>
                <Button type="contained" className="button-form" onClick={() => console.log('Create Pool clicked')}>Lock Pool</Button>
            </div>
        </div>
    );
}

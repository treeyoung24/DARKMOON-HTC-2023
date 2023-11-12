import { PoolCardPassenger, PoolCardPassengerProps } from "../components/PoolCardPassenger";
import { PoolCardDriver, PoolCardDriverProps } from "../components/PoolCardDriver";
import React from "react";
import { paste } from "@testing-library/user-event/dist/paste";


export function ManagePool() {
    const [drivers, setDrivers] = React.useState<PoolCardDriverProps[]>([]);
    const [passengers, setPassengers] = React.useState<PoolCardPassengerProps[]>([]);

    return (
        <div className="manage-pool">
            <div className="flex flex-col justify-center">
                <p className="text-md mb-2">My Pool</p>
                <div className="container">
                    {
                        drivers?.map((driver: PoolCardDriverProps) => {
                            return <PoolCardDriver id={driver.id} startingTime={driver.startingTime} pickupLocation={driver.pickupLocation} arrivalTime={driver.arrivalTime} poolSize={driver.poolSize} availableSlot={driver.availableSlot} totalEarn={driver.totalEarn} />
                        })}
                </div>
            </div>
            <div className="flex flex-col justify-center">
                <p className="text-md mb-2">Request Pool</p>
                <div className="container">
                    {
                        passengers?.map((passenger: PoolCardPassengerProps) => {
                            return <PoolCardPassenger pickupTime={passenger.pickupTime} pickupLocation={passenger.pickupLocation} arrivalTime={passenger.arrivalTime} co2Emmission={passenger.co2Emmission} numStop={passenger.numStop} fees={passenger.fees} />
                        })}
                </div>
            </div>

        </div>
    )
}

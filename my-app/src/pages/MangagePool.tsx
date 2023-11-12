import { PoolCardPassenger, PoolCardPassengerProps } from "../components/PoolCardPassenger";
import { PoolCardDriver, PoolCardDriverProps } from "../components/PoolCardDriver";
import React from "react";
import { paste } from "@testing-library/user-event/dist/paste";
import { getMyPool } from "../api/services/pool.service";
import { PoolDriverMyPoolDTOReturn } from "../api/dtos/pool-driver-mypool.dto";


export function ManagePool() {
    const [drivers, setDrivers] = React.useState<PoolDriverMyPoolDTOReturn[]>([]);
    const [passengers, setPassengers] = React.useState<PoolCardPassengerProps[]>([]);

    React.useEffect(() => {
        getMyPool(1).then((res) => {
            console.log(res);
            setDrivers(drivers => ([ ...drivers, ...res]));
            console.log(drivers);

        }).catch((error) => {
            console.error(error);
        }
        )
    }, []);

    return (
        <div className="manage-pool">
            <div className="flex flex-col justify-center">
                <p className="text-md mb-2">My Pool</p>
                <div className="container">
                    {
                        drivers.map((driver: PoolDriverMyPoolDTOReturn) => {
                            return <PoolCardDriver id={`${driver.poolId}`} startingTime={driver.startTime} pickupLocation={driver.destination} arrivalTime={driver.arrivalTime} poolSize={driver.poolSize} availableSlot={driver.availableSlots} totalEarn={driver.totalEarnings} />
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

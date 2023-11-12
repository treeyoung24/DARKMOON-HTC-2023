import { PoolCardPassenger, PoolCardPassengerProps } from "../components/PoolCardPassenger";
import { PoolCardDriver, PoolCardDriverProps } from "../components/PoolCardDriver";
import React from "react";
import { paste } from "@testing-library/user-event/dist/paste";
import { getMyPool, getPassengerPool, getRequestPool } from "../api/services/pool.service";
import { PoolDriverMyPoolDTOReturn } from "../api/dtos/pool-driver-mypool.dto";
import { PassengerDTOReturn } from "../api/dtos/passenger.dto";
import { PoolPassengerMyViewDTOReturn } from "../api/dtos/pool-passenger-myview.dto";


export function ManagePool() {
    const [drivers, setDrivers] = React.useState<PoolDriverMyPoolDTOReturn[]>([]);
    const [pendingPassengers, setPendingPassengers] = React.useState<PoolPassengerMyViewDTOReturn[]>([]);
    const [approvedPassengers, setApprovedPassengers] = React.useState<PoolPassengerMyViewDTOReturn[]>([]);

    React.useEffect(() => {
        const promises = [getMyPool(1), getPassengerPool(2), getRequestPool(1)];

        Promise.all(promises).then(values => {
            console.log(values);
            setDrivers(drivers => ([ ...drivers, ...values[0]]));
            setPendingPassengers(pendingPassengers => ([...pendingPassengers, ...values[1]]));
            setApprovedPassengers(approvedPassengers => ([...approvedPassengers, ...values[2]]));
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
                        pendingPassengers.map((passenger: PoolPassengerMyViewDTOReturn) => {
                            return <PoolCardPassenger key={passenger.poolId} numStop={passenger.stops} pickupTime={passenger.pickupTime} arrivalTime={passenger.arrivalTime} co2Emmission={passenger.co2} fees={passenger.fee} isPending={true}/>
                        })}
                    {
                        approvedPassengers.map((passenger: PoolPassengerMyViewDTOReturn) => {
                            return <PoolCardPassenger key={passenger.poolId} numStop={passenger.stops} pickupTime={passenger.pickupTime} arrivalTime={passenger.arrivalTime} co2Emmission={passenger.co2} fees={passenger.fee} isPending={false}/>
                        })}
                </div>
            </div>

        </div>
    )
}

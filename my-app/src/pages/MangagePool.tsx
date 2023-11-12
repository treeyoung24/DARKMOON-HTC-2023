import { PoolCardPassenger } from "../components/PoolCardPassenger";
import { PoolCardDriver } from "../components/PoolCardDriver";

export function ManagePool() {
    return (
        <div className="manage-pool">
            <div className="flex flex-col justify-center">
                <p className="text-md mb-2">My Pool</p>
                <div className="container">
                    <PoolCardDriver startingTime="10:00" pickupLocation="1234" arrivalTime="11:00" poolSize={4} availableSlot={2} totalEarn={10} />
                    <PoolCardDriver startingTime="10:00" pickupLocation="1234" arrivalTime="11:00" poolSize={4} availableSlot={2} totalEarn={10} />
                    <PoolCardDriver startingTime="10:00" pickupLocation="1234" arrivalTime="11:00" poolSize={4} availableSlot={2} totalEarn={10} />
                </div>
            </div>
            <div className="flex flex-col justify-center">
                <p className="text-md mb-2">Request Pool</p>
                <div className="container">
                    <PoolCardPassenger pickupTime="10:00" pickupLocation="1234" arrivalTime="11:00" co2Emmission={100} numStop={2} fees={10} />
                </div>
            </div>

        </div>
    )
}

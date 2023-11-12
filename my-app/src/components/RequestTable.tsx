import { Button } from "./Button";

export function RequestTable() {
    return (
        <div className="request-table">
            <p>Pending Requests</p>
            <div className="pending">
                <div className="pending-item">
                    <p>Edward</p>
                    <p>10:00AM</p>
                    <Button type="contained" className="button-form" onClick={() => console.log('Create Pool clicked')}>Accept</Button>
                </div>
            </div>
            <p>Accepted Requests</p>
            <div className="accepted">
                <div className="accepted-item">
                    <div className="pending-item">
                        <p>Edward</p>
                        <p>10:00AM</p>
                        <Button type="contained" className="button-form" onClick={() => console.log('Create Pool clicked')}>Remove</Button>
                    </div>

                </div>
            </div>
        </div>
    )
}

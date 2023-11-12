import { Home } from "../pages/Home";
import { SideNavBar } from "./SideNavBar";
import "../styles/layout.sass";
import { CreatePool } from "../pages/CreatePool";
import { ManagePool } from "../pages/MangagePool";
import { ManageDetailedPool } from "../pages/ManageDetailedPool";

export const Layout: React.FC = () => {
    return (
        <div className="layout">
            {/* Side Navigation Bar */}
            <SideNavBar />

            {/* Main Content Area */}
            <div className="main">
                {/* Placeholder for your components */}
                {/*< Home/>*/}
                {/*<CreatePool />*/}
                {/*<ManagePool />*/}
                <ManageDetailedPool />

            </div>
        </div>
    );
};

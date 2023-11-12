import { Home } from "../pages/Home";
import { SideNavBar } from "./SideNavBar";
import "../styles/layout.sass";
import { CreatePool } from "../pages/CreatePool";
import { ManagePool } from "../pages/MangagePool";
import { ManageDetailedPool } from "../pages/ManageDetailedPool";
import { Route, Routes } from "react-router-dom";

export const Layout: React.FC = () => {
    return (
        <div className="layout">
            {/* Side Navigation Bar */}
            <SideNavBar />

            {/* Main Content Area */}
            <div className="main">
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/home" element={<Home />} />
                    <Route path="/create-pool" element={<CreatePool />} />
                    <Route path="/manage-pool" element={<ManagePool />} />
                </Routes>
            </div>
        </div>
    );
};

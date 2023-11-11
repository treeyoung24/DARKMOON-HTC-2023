import { Home } from "../pages/Home";
import { SideNavBar } from "./SideNavBar";
import "../styles/layout.sass";

export const Layout: React.FC = () => {
  return (
    <div className="layout">
      {/* Side Navigation Bar */}
      <SideNavBar />

      {/* Main Content Area */}
      <div className="main">
        {/* Placeholder for your components */}
        <Home/>
      </div>
    </div>
  );
};

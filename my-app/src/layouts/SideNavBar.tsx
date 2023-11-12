import React from 'react';
import { Button } from '../components/Button';
import "../styles/layout.sass";
import { ProfilePicture } from './ProfilePicture';
import Logo from "../OIG-removebg-preview.png"
import { useNavigate } from 'react-router-dom';

// Define SideNavBar component
export const SideNavBar: React.FC = () => {
    const navigate = useNavigate();
    return (
        <div className="sideNavBar">
            {/* Profile Picture Holder */}
            <img
                src={Logo}
                onClick={() => navigate('/home')}
            />

            {/* Navigation Buttons */}
            <Button type="contained" className="button-nav" onClick={() => navigate('create-pool')}>Create Pool</Button>
            <Button type="contained" className="button-nav" onClick={() => navigate('manage-pool')}>Manage Pool</Button>
        </div>
    );
};

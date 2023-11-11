import React from 'react';
import { Button } from '../components/Button';
import "../styles/layout.sass";
import { ProfilePicture } from './ProfilePicture';

// Define SideNavBar component
export const SideNavBar: React.FC = () => {
  return (
    <div className="sideNavBar">
      {/* Profile Picture Holder */}
      <ProfilePicture />

      {/* Navigation Buttons */}
      <Button type="contained" className="button-nav" onClick={() => console.log('Create Pool clicked')}>Create Pool</Button>
      <Button type="contained" className="button-nav" onClick={() => console.log('View Pool clicked')}>View Pool</Button>
      <Button type="contained" className="button-nav" onClick={() => console.log('Manage Pool clicked')}>Manage Pool</Button>
    </div>
  );
};

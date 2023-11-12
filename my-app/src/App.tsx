import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Layout } from './layouts/Layout';
import { useEffect } from 'react';
import { useJsApiLoader } from '@react-google-maps/api';
import { GoogleMap } from '@react-google-maps/api';
const containerStyle = {
    width: '400px',
    height: '400px'
};

const center = {
    lat: -3.745,
    lng: -38.523
};
const googleMapsAPIKey = "AIzaSyCOSGEkapfeB2E0aP3j9RgEdkwVx77hUaY";
function App() {
    // const { isLoaded } = useJsApiLoader({
    //     id: 'google-map-script',
    //     googleMapsApiKey: googleMapsAPIKey
    // })
    //
    // const [map, setMap] = React.useState(null)
    //
    // const onLoad = React.useCallback(function callback(map:any) {
    //     // This is just an example of getting and using the map instance!!! don't just blindly copy!
    //     const bounds = new window.google.maps.LatLngBounds(center);
    //     map.fitBounds(bounds);
    //
    //     setMap(map)
    // }, [])
    // const onUnmount = React.useCallback(function callback(map:any) {
    //     setMap(null)
    // }, [])
    //   return isLoaded ? (
    //     <GoogleMap
    //       mapContainerStyle={containerStyle}
    //       center={center}
    //       zoom={10}
    //       onLoad={onLoad}
    //       onUnmount={onUnmount}
    //     >
    //     </GoogleMap>
    // ) : <></>}
    return (
        <Layout />
    )
}
export default App;

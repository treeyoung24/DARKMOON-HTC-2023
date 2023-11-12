import { GoogleMap, useJsApiLoader } from "@react-google-maps/api";
import React from "react";
const containerStyle = {
    width: '800px',
    height: '600px'
};

const center = {
    lat: -3.745,
    lng: -38.523
};

interface DriverMapProps { polyline: string }

export const DriverMap = ({ polyline }: DriverMapProps) => {

    const { isLoaded } = useJsApiLoader({
        id: 'google-map-script',
        googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY || ''
    })

    const [map, setMap] = React.useState(null)
    const decodedPath = window.google.maps.geometry.encoding.decodePath(polyline);
    console.log(decodedPath);

    const onLoad = React.useCallback(function callback(map: any) {
        // This is just an example of getting and using the map instance!!! don't just blindly copy!
        const bounds = new window.google.maps.LatLngBounds(center);
        map.fitBounds(bounds);

        setMap(map)
    }, [])
    const onUnmount = React.useCallback(function callback(map: any) {
        setMap(null)
    }, [])
    return isLoaded ? (
        <GoogleMap
            mapContainerStyle={containerStyle}
            center={center}
            zoom={10}
            onLoad={onLoad}
            onUnmount={onUnmount}
        >
        </GoogleMap>
    ) : <></>
}

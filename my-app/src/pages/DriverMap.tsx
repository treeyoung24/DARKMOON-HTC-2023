import { GoogleMap, useJsApiLoader } from "@react-google-maps/api";
import React from "react";
import Map from "../map.png"
const containerStyle = {
    width: '800px',
    height: '600px'
};

const center = {
    lat: -3.745,
    lng: -38.523
};

const defaultLibraries = ["geometry"]

export const DriverMap = () => {

    const { isLoaded } = useJsApiLoader({
        id: 'google-map-script',
        googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY || '',
        libraries: defaultLibraries 
    })

    const [map, setMap] = React.useState(null)

    const onLoad = React.useCallback(function callback(map: any) {
        // This is just an example of getting and using the map instance!!! don't just blindly copy!
        const bounds = new window.google.maps.LatLngBounds(center);
         //const ff = new google.maps.geometry.encoding.decodePath("awpvHtlhwTJeFNsS{D[gEGsDSk@jn@]~b@FrAL|@ZjAnBlDTNtAnCbBnENv@Bv@E|@G\SZSLs@VcP@aMKuCBeBAqEFQD}SFqACsO@[MuBIcVHSQmBc@y@WiAi@a@Mg@u@WsAAU@?aA??bYGdS@hp@IjM?a@G~J?CLbFAnLEnBOpBIx@s@jAy@p@aBhAa@h@WAKz@AnCOfAQp@yAtAwVrSmDtCm@@[MSUc@mAwAqJKuAGeDAoBaAEe@Ny@r@qAnA[cA@i@gAcA[Q{@Gq@Dm@Ri@\aEjDi@RoBPg@TcAlAcB~BQf@I`AHdCR~ENhBvB?");
        // var bounds = new google.maps.LatLngBounds
        map = new google.maps.Map(
            document.getElementById("map_canvas"), {
              center: new google.maps.LatLng(51.049, -114.06),
              zoom: 13,
              mapTypeId: google.maps.MapTypeId.ROADMAP
            });
        var path = google.maps.geometry.encoding.decodePath("awpvHtlhwTJeFNsS{D[gEGsDSk@jn@]~b@FrAL|@ZjAnBlDTNtAnCbBnENv@Bv@E|@G\SZSLs@VcP@aMKuCBeBAqEFQD}SFqACsO@[MuBIcVHSQmBc@y@WiAi@a@Mg@u@WsAAU@?aA??bYGdS@hp@IjM?a@G~J?CLbFAnLEnBOpBIx@s@jAy@p@aBhAa@h@WAKz@AnCOfAQp@yAtAwVrSmDtCm@@[MSUc@mAwAqJKuAGeDAoBaAEe@Ny@r@qAnA[cA@i@gAcA[Q{@Gq@Dm@Ri@\aEjDi@RoBPg@TcAlAcB~BQf@I`AHdCR~ENhBvB?s@@");
        //Initialize the Direction Service
    
        console.log(path);
        for (var i = 0; i < path.length; i++) {
            var myLatlng = new google.maps.LatLng(path[i].lat, path[i].lng);
          bounds.extend(myLatlng);
        }
        var polyline = new google.maps.Polyline({
            path: path,
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#FF0000',
            fillOpacity: 0.35,
            map: map
              // strokeColor: "#0000FF",
              // strokeOpacity: 1.0,
              // strokeWeight: 2
          });
           polyline.setMap(map);
           //map.fitBounds(bounds);
        //setMap(map)
    }, [isLoaded])
    const onUnmount = React.useCallback(function callback(map: any) {
        setMap(null)
    }, [])
    return isLoaded ? (
        //<img src={Map} alt="Map" />
        <GoogleMap id="map_canvas"
            mapContainerStyle={containerStyle}
            center={center}
            zoom={10}
            onLoad={onLoad}
            onUnmount={onUnmount}
        >
        </GoogleMap>
    ) : <></>
}

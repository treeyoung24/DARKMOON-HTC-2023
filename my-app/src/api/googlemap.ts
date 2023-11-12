import { GoogleMap } from '@googlemaps/map-loader';

const googleMapsAPIKey = "AIzaSyCOSGEkapfeB2E0aP3j9RgEdkwVx77hUaY";

/* Options for how the map should initially render. */
const mapOptions = {
  center: {
    lat: 47.649196,
    lng: -122.350384
  },
  zoom: 12
}

/* Options for loading the Maps JS API. */
const apiOptions = {
  version: 'weekly',
  libraries: ['places']
}

/*
 * Set ID of the div where the map will be loaded,
 * and whether to append to that div.
 */
const mapLoaderOptions:any = {
  apiKey: googleMapsAPIKey,
  divId: 'google_map',
  append: true, // Appends to divId. Set to false to init in divId.
  mapOptions: mapOptions,
  apiOptions: apiOptions
};

// Instantiate map loader
export const mapLoader = new GoogleMap();

// Load the map
mapLoader
  .initMap(mapLoaderOptions)
  .then(googleMap => {
      return googleMap
    // returns instance of google.maps.Map
  });

import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Layout } from './layouts/Layout';
import { useEffect } from 'react';
import { useJsApiLoader } from '@react-google-maps/api';
import { GoogleMap } from '@react-google-maps/api';
function App() {
        return (
        <Layout />
    )
}
export default App;

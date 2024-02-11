import { useEffect, useState } from 'react';
import './App.css';

interface CityWeatherDetails {
    country: string;
    city: string;
    temperature: number;
    updatedDate: Date;
}

function App() {
    const [forecasts, setForecasts] = useState<CityWeatherDetails[]>();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>

                    <th>UpdatedDate</th>
                    <th>Country</th>
                    <th>City</th>
                    <th>Temp. (C)</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr>
                        <td>{forecast.updatedDate.toString()}</td>
                        <td>{forecast.country}</td>
                        <td>{forecast.city}</td> 
                        <td>{forecast.temperature}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;
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
  //  const [cities, setCities] = useState<string[]>();
   // const [selectedValue, setSelectedValue] = useState(null);

    useEffect(() => {
        populateWeatherData();
    }, []);
  

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. </em></p>
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
        </table>
        ;

    //const cityDropdown = cities === undefined ? "" :
    //    <select onChange={handleChange}>
    //        {cities.map((city) =>
    //            <option key={city}>{city}</option>
    //        )};
    //    </select>

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
         
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
       // if (selectedValue != null) {
            const response = await fetch('weatherforecast?city=Berlin&country=DE');
            const data = await response.json();
            setForecasts(data);
       // }
    }

    //async function populatecities() {
    //    const response = await fetch('weatherforecast/getcities');
    //    const data = await response.json();
    //    setCities(data);
    //}
}

export default App;
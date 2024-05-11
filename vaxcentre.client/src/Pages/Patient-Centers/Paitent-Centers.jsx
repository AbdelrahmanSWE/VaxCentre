import React, { useState,useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import { reserveVaccineAtCenter,reserveSecondDoseAtCenter } from '../../Services/PaitentServices';
import {  fetchCenters} from "../../Services/CenterServices.jsx";
import '../../App.css';

function Patient() {
    const [centers, setCenters] = useState([]);
    useEffect(() => {
        // Fetch patients when the component mounts
        fetchCenters()
            .then((data) => setCenters(data))
            .catch((error) => console.error('Error fetching centers:', error));
    }, []);
        

    /*const handleCenterClick = (centerId) => {
        // Handle center click
    };*/

    const handleReserve = async (centerId, vaccineId) => {
        try {
            const response = await reserveVaccineAtCenter(centerId, vaccineId);
            console.log(response);
        } catch (error) {
            console.error('Error reserving vaccine:', error);
        }
    };

    const handleSecondReserve = async (centerId, vaccineId) => {
        try {
            const response = await reserveSecondDoseAtCenter(centerId, vaccineId);
            console.log(response);
        } catch (error) {
            console.error('Error reserving second dose:', error);
        }
    };

    return (
        <div>
            <h2 className="card title">Centers</h2>
            {centers.map((center) => (
                <div key={center.id} className="card">
                    <h3>{center.displayName}</h3>
                    <ul>
                    </ul>
                </div>
            ))}
        </div>
    );
}

export default Patient;



/*import React, { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import '../../App.css';

const Patient = () => {
    const [centers, setCenters] = useState([]);
    const [selectedCenter, setSelectedCenter] = useState(null);
    const [reservedVaccine, setReservedVaccine] = useState(null);
    const [secondDoseReserveId, setSecondDoseReserveId] = useState(null); // Track the vaccine ID for second dose reserve

    useEffect(() => {
        // Dummy data for testing
        const dummyData = [
            {
                id: 1,
                name: 'Center 1',
                vaccines: [
                    { id: 101, name: 'Vaccine A', reserved: false }, // Add reserved property to track reservation status
                    { id: 102, name: 'Vaccine B', reserved: false }
                ]
            },
            {
                id: 2,
                name: 'Center 2',
                vaccines: [
                    { id: 103, name: 'Vaccine C', reserved: false },
                    { id: 104, name: 'Vaccine D', reserved: false }
                ]
            },
            {
                id: 3,
                name: 'Center 3',
                vaccines: [
                    { id: 105, name: 'Vaccine E', reserved: false },
                    { id: 106, name: 'Vaccine F', reserved: false } // Add a reserved vaccine for demonstration
                ]
            }
        ];

        setCenters(dummyData);
    }, []);

    const handleCenterClick = (centerId) => {
        setSelectedCenter(centerId);
        setReservedVaccine(null); // Reset reserved vaccine when selecting a new center
        setSecondDoseReserveId(null); // Reset second dose reserve ID
    };

    *//*const handleReserve = async (centerId, vaccineId) => {
        try {
            // Simulate reservation by updating dummy data
            const updatedCenters = centers.map(center => ({
                ...center,
                vaccines: center.vaccines.map(vaccine => ({
                    ...vaccine,
                    reserved: vaccine.id === vaccineId ? true : vaccine.reserved
                }))
            }));
            setCenters(updatedCenters);
            setReservedVaccine(vaccineId); // Set reserved vaccine after successful reservation
            setSecondDoseReserveId(vaccineId); // Set the vaccine ID for second dose reserve
            // Send POST request to server to reserve vaccine
            const response = await fetch('/api/reserve', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    centerId: centerId,
                    vaccineId: vaccineId
                })
            });
            // Handle response as needed
        } catch (error) {
            console.error('Error reserving vaccine:', error);
        }
    };

    const handleSecondReserve = async (centerId, vaccineId) => {
        try {
            // Send POST request to server to reserve the second dose
            const response = await fetch('/api/reserve', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    centerId: centerId,
                    vaccineId: vaccineId
                })
            });
            // Handle response as needed
            console.log('Second dose reserved successfully!');
        } catch (error) {
            console.error('Error reserving second dose:', error);
        }
    };*//*

    return (
        <div>
            <div style={{ display: 'flex', flexDirection: 'row' }}>
                <div style={{ flex: 1 }}>
                    <h2 className="card title">Centers</h2>
                    {centers.map((center) => (
                        <div key={center.id} style={{ marginBottom: '10px' }}>
                            <Button variant="success" onClick={() => handleCenterClick(center.id)}>
                                {center.name}
                            </Button>

                        </div>
                    ))}
                </div>
                {selectedCenter && (
                    <div style={{ flex: 1 }}>
                        <h2 className="card title">{centers.find((center) => center.id === selectedCenter).name}</h2>
                        <ul>
                            {centers.find((center) => center.id === selectedCenter).vaccines.map((vaccine, index) => (
                                <li key={index}>
                                    {vaccine.name}
                                    <button className="addBtn" onClick={() => handleReserve(selectedCenter, vaccine.id)} disabled={vaccine.reserved}>
                                        {vaccine.reserved ? 'Reserved' : 'Reserve'}
                                    </button>
                                    {*//* Show second dose reserve button next to reserved vaccine *//*}
                                    {vaccine.reserved && vaccine.id === secondDoseReserveId && (
                                        <button className="addBtn" onClick={() => handleSecondReserve(selectedCenter, vaccine.id)}>Reserve Second Dose</button>
                                    )}
                                </li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>

        </div>
    );

};

export default Patient;*/
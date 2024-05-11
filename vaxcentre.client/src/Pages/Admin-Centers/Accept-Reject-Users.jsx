// ListUsers.jsx

import React, { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import { fetchUnacceptedUsers, acceptPatient } from '../../Services/AdminServices.jsx'; // Import the API service function

function ListUsers() {
    const [patients, setPatients] = useState([]);

    useEffect(() => {
        // Fetch patients when the component mounts
        fetchUnacceptedUsers()
            .then((data) => setPatients(data))
            .catch((error) => console.error('Error fetching patients:', error));
    }, []);

    const handleAccept = async (id) => {
        try {
            const patient = await acceptPatient(id);
            console.log('Patient accepted:', patient);
            // Update the patients state to remove the accepted patient
            setPatients(prevPatients => prevPatients.filter(p => p.id !== id));
        } catch (error) {
            console.error('Error accepting patient:', error);
        }
    };



    return (
        <div>
            <h2 className='card title'>Requests</h2>
            <div>
                {patients.map((patient) => (
                    <div className='card' key={patient.id}>
                        <h2>{patient.firstName} {patient.lastName}</h2>
                        <Button variant="success" onClick={() => handleAccept(patient.id)}>Accept</Button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default ListUsers;

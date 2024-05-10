// ListUsers.jsx

import React, { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import { fetchUnacceptedUsers } from '../../Services/AdminServices.jsx'; // Import the API service function

function ListUsers() {
    const [patients, setPatients] = useState([]);

    useEffect(() => {
        // Fetch patients when the component mounts
        fetchUnacceptedUsers()
            .then((data) => setPatients(data))
            .catch((error) => console.error('Error fetching patients:', error));
    }, []);

    const handleAccept = (id) => {
        // Update the state of the patient with the given id to 1 (accepted)
        // Implement your logic here
    };

    const handleDelete = (id) => {
        // Remove the patient with the given id from the list (Database)
        // Implement your logic here
    };

    return (
        <div>
            <h2 className='card title'>Requests</h2>
            <div>
                {patients.map((patient) => (
                    <div className='card' key={patient.id}>
                        <h2>{patient.name}</h2>
                        <Button variant="success" onClick={() => handleAccept(patient.id)}>Accept</Button>
                        <Button variant="danger" onClick={() => handleDelete(patient.id)}>Delete</Button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default ListUsers;

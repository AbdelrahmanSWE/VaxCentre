import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import './App.css';


function ListUsers() {
    const [patients, setPatients] = useState([
        { id: 1, name: 'Patient 1', state: 0 },
        { id: 2, name: 'Patient 2', state: 0 },
        { id: 3, name: 'Patient 3', state: 0 },
        { id: 4, name: 'Patient 4', state: 0 }
    ]);

    const handleAccept = (id) => {
        // Update the state of the patient with the given id to 1 (accepted)

    };


    const handleDelete = (id) => {
        // Remove the patient with the given id from the list(Database)

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
import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import '../App.css';

function VaccinationCenter() {
    const [patients, setPatients] = useState([
        { id: 1, name: 'Patient 1', firstDoseAccepted: false, secondDoseAccepted: false },
        { id: 2, name: 'Patient 2', firstDoseAccepted: false, secondDoseAccepted: false },
        { id: 3, name: 'Patient 3', firstDoseAccepted: false, secondDoseAccepted: false }
    ]);

    const handleAcceptFirstDose = (patientId) => {
        // Find the index of the patient with the given ID
        const index = patients.findIndex(patient => patient.id === patientId);
    
        // If the patient is found, update the state to set firstDoseAccepted to true
        if (index !== -1) {
            const updatedPatients = [...patients];
            updatedPatients[index] = { ...updatedPatients[index], firstDoseAccepted: true };
            setPatients(updatedPatients);
    
            // Fetch request to update the server
            fetch('/api/vaccines', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    id: patientId,
                    patients: updatedPatients // Include the updated list of patients
                }),
            })
            .then(response => {
                // Handle the response accordingly
            })
            .catch(error => {
                // Handle errors
            });
        }
    };
    
    const handleRefuseFirstDose = (patientId) => {
        // Filter out the patient with the given ID
        const updatedPatients = patients.filter(patient => patient.id !== patientId);
    
        // Update the state with the filtered array
        setPatients(updatedPatients);
    
        // Fetch request to update the server
        fetch('/api/vaccines', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                id: patientId,
                patients: updatedPatients // Include the updated list of patients
            }),
        })
        .then(response => {
            // Handle the response accordingly
        })
        .catch(error => {
            // Handle errors
        });
    };
    
    const handleAcceptSecondDose = (patientId) => {
        // Find the index of the patient with the given ID
        const index = patients.findIndex(patient => patient.id === patientId);
    
        // If the patient is found, update the state to set secondDoseAccepted to true
        if (index !== -1) {
            const updatedPatients = [...patients];
            updatedPatients[index] = { ...updatedPatients[index], secondDoseAccepted: true };
            setPatients(updatedPatients);
    
            // Fetch request to update the server
            fetch('/api/vaccines', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    id: patientId,
                    patients: updatedPatients // Include the updated list of patients
                }),
            })
            .then(response => {
                // Handle the response accordingly
            })
            .catch(error => {
                // Handle errors
            });
        }
    };
    
    const handleRefuseSecondDose = (patientId) => {
        // Filter out the patient with the given ID
        const updatedPatients = patients.filter(patient => patient.id !== patientId);
    
        // Update the state with the filtered array
        setPatients(updatedPatients);
    
        // Fetch request to update the server
        fetch('/api/vaccines', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                id: patientId,
                patients: updatedPatients // Include the updated list of patients
            }),
        })
        .then(response => {
            // Handle the response accordingly
        })
        .catch(error => {
            // Handle errors
        });
    };
    
    
    const handleUploadCertificate = (patientId, file) => {
        // Handle uploading the certificate file for the patient with the given ID
    
        // Update the state with the uploaded file information
        // You might need additional logic here based on how you handle file uploads
    
        // Fetch request to update the server
        fetch('/api/uploadCertificate', {
            method: 'POST',
            headers: {
                // Add any necessary headers for file uploads
                'Content-Type': 'multipart/form-data',//edit it boys json or form-data fukit 
            },
            body: JSON.stringify({
                id: patientId,
                file: file, // Assuming file is already processed appropriately for uploading
                patients: patients // Include the updated list of patients
            }),
        })
        .then(response => {
        })
        .catch(error => {
            // Handle errors -> no isa 
        });
    };
    
    

    return (
        <div>
            <h2 className='card title'>Vaccination Center</h2>
            {patients.map((patient) => (
                <div className='card' key={patient.id}>
                    <h3>{patient.name}</h3>
                    {patient.firstDoseAccepted ? (
                        <>
                            <Button variant="success" className="addBtn" onClick={() => handleAcceptSecondDose(patient.id)}>Accept Second Dose</Button>
                            <Button variant="danger" className="addBtn" onClick={() => handleRefuseSecondDose(patient.id)}>Refuse Second Dose</Button>
                            {patient.secondDoseAccepted && (
                                <div>
                                    <input type="file" onChange={(e) => handleUploadCertificate(patient.id, e.target.files[0])} />
                                </div>
                            )}
                        </>
                    ) : (
                        <>
                            <Button variant="success" className="addBtn" onClick={() => handleAcceptFirstDose(patient.id)}>Accept First Dose</Button>
                            <Button variant="danger" className="addBtn" onClick={() => handleRefuseFirstDose(patient.id)}>Refuse First Dose</Button>
                        </>
                    )}
                </div>
            ))}
        </div>
    );

}

export default VaccinationCenter;
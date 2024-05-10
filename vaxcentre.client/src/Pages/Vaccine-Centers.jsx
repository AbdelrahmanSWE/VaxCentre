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
        // Handle accepting the first dose reservation for the patient with the given ID
        // Update the patient state accordingly
    };

    const handleRefuseFirstDose = (patientId) => {
        // Handle refusing the first dose reservation for the patient with the given ID
        // Update the patient state accordingly
    };

    const handleAcceptSecondDose = (patientId) => {
        // Handle accepting the second dose reservation for the patient with the given ID
        // Update the patient state accordingly
    };

    const handleRefuseSecondDose = (patientId) => {
        // Handle refusing the second dose reservation for the patient with the given ID
        // Update the patient state accordingly
    };

    const handleUploadCertificate = (patientId, file) => {
        // Handle uploading the certificate file for the patient with the given ID
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

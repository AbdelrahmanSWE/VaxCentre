import React from 'react';

const PatientsVaccination = () => {
    const patients = [
        {
            firstName: 'Obama',
            lastName: 'Deez',
            SSID: '123456789',
            vaccinationName: 'X',
        },
        {
            firstName: 'Jane',
            lastName: 'Smith',
            SSID: '987654321',
            vaccinationName: 'N+',
        },
    ];

    return (
        <div>
            {patients.map((patient, index) => (
                <div key={index} className="card">
                    <h2>{`${patient.firstName} ${patient.lastName}`}</h2>
                    <p>SSID: {patient.SSID}</p>
                    <p>Vaccination Name: {patient.vaccinationName}</p>
                </div>
            ))}
        </div>
    );
};

export default PatientsVaccination;

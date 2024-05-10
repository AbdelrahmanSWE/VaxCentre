import React, { useState, useEffect } from 'react';

const Patient = () => {
    const [centers, setCenters] = useState([]);
    const [selectedCenter, setSelectedCenter] = useState(null);

    useEffect(() => {
        const dummyData = [
            {
                id: 1,
                name: 'Center 1',
                vaccines: [
                    { id: 101, name: 'Vaccine A' },
                    { id: 102, name: 'Vaccine B' }
                ]
            },
            {
                id: 2,
                name: 'Center 2',
                vaccines: [
                    { id: 103, name: 'Vaccine C' },
                    { id: 104, name: 'Vaccine D' }
                ]
            },
            {
                id: 3,
                name: 'Center 3',
                vaccines: [
                    { id: 105, name: 'Vaccine E' },
                    { id: 106, name: 'Vaccine F' }
                ]
            }
        ];

        setCenters(dummyData); // dummy data for testing

        const fetchCenters = async () => {
            try {
                const response = await fetch('/api/centers'); // edit the end point as needed boy 
                const data = await response.json();
                setCenters(data);
            } catch (error) {
                console.error('Error fetching centers:', error);
            }
        };

        fetchCenters();
    }, []);

    const handleCenterClick = (centerId) => {
        setSelectedCenter(centerId);
    };
    // Handling the reservation 
    const handleReserve = async (centerId, vaccineId) => {
        try {
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
            const data = await response.json();
            console.log('Reservation successful:', data);
        } catch (error) {
            console.error('Error reserving vaccine:', error);
        }
    };

    return (
        <div>
            <div style={{ display: 'flex', flexDirection: 'row' }}>
                <div style={{ flex: 1 }}>
                    {centers.map((center) => (
                        <div key={center.id} style={{ marginBottom: '10px' }}>
                            <button onClick={() => handleCenterClick(center.id)}>{/** use the handleCenterClick to add the center choosen*/}
                                {center.name}
                            </button>
                        </div>
                    ))}
                </div>
                {selectedCenter && (
                    <div style={{ flex: 1 }}>
                        <h2>{centers.find((center) => center.id === selectedCenter).name}</h2>
                        <ul>
                            {centers.find((center) => center.id === selectedCenter).vaccines.map((vaccine, index) => ({/**mapping on the specified center */}
                                <li key={index}>
                                    {vaccine.name}
                                    <button onClick={() => handleReserve(selectedCenter, vaccine.id)}>Reserve</button>{/**The selected center represents center's id*/}
                                </li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Patient;

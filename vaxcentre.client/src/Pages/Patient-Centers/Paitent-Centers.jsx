import React, { useState } from 'react';

function Patient() {
    // const [selectedCenter, setSelectedCenter] = useState(null);
    // const [showAddOverlay, setShowAddOverlay] = useState(false);
    // const [showEditOverlay, setShowEditOverlay] = useState(false);
    // const [newCenter, setNewCenter] = useState({ name: '', vaccines: [] });

    const centers = [
        { id: 1, name: 'Center 1', vaccines: ['Vaccine 1', 'Vaccine 2'] },
        { id: 2, name: 'Center 2', vaccines: ['Vaccine 3', 'Vaccine 4'] },
        { id: 3, name: 'Center 3', vaccines: ['Vaccine 8', 'Vaccine 6'] },
        { id: 4, name: 'Center 4', vaccines: ['Vaccine 7', 'Vaccine 8'] }
    ];

    return (
        <div>
            {centers.map((center) => (
                <div key={center.id}>
                    <h2>{center.name}</h2>
                    <ul>
                        {center.vaccines.map((vaccine, index) => (
                            <li key={index}>{vaccine}</li>
                        ))}
                    </ul>
                </div>
            ))}
        </div>
    );
}

export default Patient;
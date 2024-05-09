import React, { useState } from 'react';

function Admin() {
    const [selectedCenter, setSelectedCenter] = useState(null);
    const [showAddOverlay, setShowAddOverlay] = useState(false);
    const [showEditOverlay, setShowEditOverlay] = useState(false);
    const [newCenter, setNewCenter] = useState({ name: '', vaccines: [] });

    const centers = [
        { id: 1, name: 'Center 1', vaccines: ['Vaccine 1', 'Vaccine 2'] },
        { id: 2, name: 'Center 2', vaccines: ['Vaccine 3', 'Vaccine 4'] },
        { id: 3, name: 'Center 3', vaccines: ['Vaccine 8', 'Vaccine 6'] },
        { id: 4, name: 'Center 4', vaccines: ['Vaccine 7', 'Vaccine 8'] }
    ];

    const handleAdd = () => {
        setShowAddOverlay(true);
    };

    const handleEdit = (center) => {
        setSelectedCenter(center);
        setShowEditOverlay(true);
    };

    const handleDelete = (centerId) => {
        // Implement delete
    };

    const handleSave = () => {
        centers.push(newCenter);
        setShowAddOverlay(false);
        setNewCenter({ name: '', vaccines: [] });
    };

    return (
        <div>
            <button onClick={handleAdd}>Add Center</button>
            {centers.map((center) => (
                <div key={center.id}>
                    <h2>{center.name}</h2>
                    <ul>
                        {center.vaccines.map((vaccine, index) => (
                            <li key={index}>{vaccine}</li>
                        ))}
                    </ul>
                    <button onClick={() => handleEdit(center)}>Edit</button>
                    <button onClick={() => handleDelete(center.id)}>Delete</button>
                </div>
            ))}
            {showAddOverlay && (
                <div className="overlay">
                    <div className="overlay-content">
                        <h2>Add New Center</h2>
                        <label>Name:</label>
                        <input
                            type="text"
                            value={newCenter.name}
                            onChange={(e) => setNewCenter({ ...newCenter, name: e.target.value })}
                        />
                        <label>Vaccines:</label>
                        <input
                            type="text"
                            value={newCenter.vaccines.join(', ')}
                            onChange={(e) => setNewCenter({ ...newCenter, vaccines: e.target.value.split(', ') })}
                        />
                        <button onClick={handleSave}>Save</button>
                        <button onClick={() => setShowAddOverlay(false)}>Cancel</button>
                    </div>
                </div>
            )}
            {showEditOverlay && (
                <div className="overlay">
                    <form className="modal" onSubmit={handleEdit}>
                        <h2>Edit Center</h2>
                        <label>
                            Name:
                            <input type="text" value={selectedCenter.name} onChange={(e) => setSelectedCenter({ ...selectedCenter, name: e.target.value })} />
                        </label>
                        <label>
                            Vaccines:
                            <input type="text" value={selectedCenter.vaccines.join(', ')} onChange={(e) => setSelectedCenter({ ...selectedCenter, vaccines: e.target.value.split(', ') })} />
                        </label>
                        <button type="submit">Save</button>
                        <button type="button" onClick={() => setShowEditOverlay(false)}>Cancel</button>
                    </form>
                </div>
            )}
        </div>
    );
}

export default Admin;
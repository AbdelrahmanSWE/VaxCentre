
import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import './App.css';


function Admin() {
    const [selectedCenter, setSelectedCenter] = useState(null);
    const [showAddOverlay, setShowAddOverlay] = useState(false);
    const [showEditOverlay, setShowEditOverlay] = useState(false);
    const [newCenter, setNewCenter] = useState({ name: '', vaccines: [] });

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

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
        <>
            <Button className='addBtn' variant="warning" onClick={handleShow}>
                +
            </Button>
            <Modal className='' show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add Center</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <label htmlFor="centerName">Name:</label>
                    <input
                        id="centerName"
                        name='centerName'
                        type="text"
                        value={newCenter.name}
                        onChange={(e) => setNewCenter({ ...newCenter, name: e.target.value })}
                    />
                    <label htmlFor="vaccines">Vaccines:</label>
                    <input
                        id='vaccines'
                        name='vaccines'
                        type="text"
                        value={newCenter.vaccines.join(', ')}
                        onChange={(e) => setNewCenter({ ...newCenter, vaccines: e.target.value.split(', ') })}
                    />
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleSave}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
            <div>
                {centers.map((center) => (
                    <div className='card' key={center.id}>
                        <h2>{center.name}</h2>
                        <ul>
                            {center.vaccines.map((vaccine, index) => (
                                <li key={index}>{vaccine}</li>
                            ))}
                        </ul>
                        <Button variant="info" onClick={() => handleEdit(center)}>Edit</Button>
                        <Button variant="danger" onClick={() => handleDelete(center.id)}>Delete</Button>                </div>
                ))}
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
        </>
    );
}

export default Admin;
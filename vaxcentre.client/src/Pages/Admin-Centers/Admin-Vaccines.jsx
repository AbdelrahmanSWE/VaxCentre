import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import '../../App.css';
import { fetchVaccines, CreateVaccine ,deleteVaccine} from '../../Services/VaccineServices.jsx';

function Vaccine() {
    const [vaccines, setVaccines] = useState([]);
    useEffect(() => {
        // Fetch patients when the component mounts
        fetchVaccines()
            .then((data) => setVaccines(data))
            .catch((error) => console.error('Error fetching vaccines:', error));
    }, []);

    const [showAddModal, setShowAddModal] = useState(false);
    const handleCloseAddModal = () => setShowAddModal(false);
    const handleShowAddModal = () => setShowAddModal(true);

    const handleSave = async (e) => {
        e.preventDefault();

        const data = {
            Name: e.target.Name.value,
            Description: e.target.Description.value,
            Precaution: e.target.Precaution.value,
            GapTime: e.target.GapTime.value
        }
        console.log(data);
        try {
            const result = await CreateVaccine(data);
            console.log('registered successful', result);

            fetchVaccines()
                .then((data) => setVaccines(data))
                .catch((error) => console.error('Error fetching centers:', error));

            handleCloseAddModal();

        } catch (error) {
            console.error('Signup failed', error);
        }

    }

    const handleEdit = (center) => {
    //    setSelectedVaccine(center);
    //    setShowEditModal(true);
    };

    const handleDelete = async (vaccine) => {
        try {
            await deleteVaccine(vaccine.id);
            console.log('Vaccine deleted successfully');

            fetchVaccines()
                .then((data) => setVaccines(data))
                .catch((error) => console.error('Error fetching vaccines:', error));
        } catch (error) {
            console.error('Error deleting vaccine:', error);
        }
    };

    return (
        <>
            <Button className='addBtn' variant="warning" onClick={handleShowAddModal}>
                +
            </Button>
            <Modal className='' show={showAddModal} onHide={handleCloseAddModal}>
                <Modal.Header closeButton>
                    <Modal.Title>Add Vaccine</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form onSubmit={handleSave}>
                        <div className='form-group'>
                            <label htmlFor='Name'>Name:</label>
                            <input type='text' id='Name' name='Name' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='Description'>Description:</label>
                            <input type='text' id='Description' name='Description' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='Precaution'>Precaution:</label>
                            <input type='text' id='Precaution' name='Precaution' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='GapTime'>Gap Time:</label>
                            <input type='number' id='GapTime' name='GapTime' />
                        </div>
                        <Button variant="primary" type="submit">
                            Save Changes
                        </Button>
                    </form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleCloseAddModal}>
                        Close
                    </Button>

                </Modal.Footer>
            </Modal>




           




            <div>
                {vaccines.map((vaccine) => (
                    <div className='card' key={vaccine.id}>
                        <h2>{vaccine.name}</h2>
                        <h5>Description:</h5>
                        <p>{vaccine.description}</p>
                        <p>Precaution: {vaccine.precaution}</p>
                        <span>Gap Days: {vaccine.gapTime}</span>
                        <Button className='EditBtn' variant="primary" onClick={() => handleEdit(vaccine.id)}>Edit Center</Button>
                        <Button variant="danger" onClick={() => handleDelete(vaccine.id)}>Delete</Button>
                    </div>
                ))}
            </div>
        </>
    );

}
export default Vaccine;
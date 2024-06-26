
import { useState ,useEffect} from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import '../../App.css';
import { fetchCenters, RegisterVaccineCentre,deleteCenter,addVaccineToCenter} from '../../Services/CenterServices.jsx';
function Admin() {
    const [centers, setCenters] = useState([]);
    useEffect(() => {
        // Fetch patients when the component mounts
        fetchCenters()
            .then((data) => setCenters(data))
            .catch((error) => console.error('Error fetching centers:', error));
    }, []);

    const [selectedCenter, setSelectedCenter] = useState(null);
    //const [showAddOverlay, setShowAddOverlay] = useState(false);
    //const [showEditOverlay, setShowEditOverlay] = useState(false);
    //const [newCenter, setNewCenter] = useState({ name: '', vaccines: [] });

    const [showAddModal, setShowAddModal] = useState(false);
    const handleCloseAddModal = () => setShowAddModal(false);
    const handleShowAddModal = () => setShowAddModal(true);

    const [showEditModal, setShowEditModal] = useState(false);
    const handleCloseEditModal = () => setShowEditModal(false);
    const handleShowEditModal = () => setShowEditModal(true);

    // const handleAdd = () => {
    //     setShowAddOverlay(true);
    // };

    // const handleEdit = (center) => {
    //     setSelectedCenter(center);
    //     setShowEditModal(true);
    // };

    // const handleUpdateCenter = (e) => {
    //     e.preventDefault();
    //     // Update the center in your database here
    //     //...
    //     //Close the modal
    //     setShowEditModal(false);
    // };

    const handleDelete = async (centre) => {
        try {
            await deleteCenter(centre);
            console.log('centre deleted successfully');

            fetchCenters()
                .then((data) => setCenters(data))
                .catch((error) => console.error('Error fetching centres:', error));
        } catch (error) {
            console.error('Error deleting centre:', error);
        }
    };

    const handleSave = async (e) => {
        e.preventDefault();

        const data = {
            UserName: e.target.UserName.value,
            Email: e.target.Email.value,
            DisplayName: e.target.DisplayName.value,
            PhoneNumber: e.target.PhoneNumber.value,
            Address: e.target.Address.value,
            Password: e.target.Password.value
        }
        console.log(data);
        try {
            const result = await RegisterVaccineCentre(data);
            console.log('registered successful', result);
            
            fetchCenters()
                .then((data) => setCenters(data))
                .catch((error) => console.error('Error fetching centers:', error));

            handleCloseAddModal();

        } catch (error) {
            console.error('Signup failed', error);
        }

    }

    const addVaccine = (event, centerId) => {
        event.preventDefault();
        const vaccineId = document.getElementById(`vaxId-${centerId}`).value;
        const centreId = document.getElementById(`centreId-${centerId}`).value;
        addVaccineToCenter(centreId, vaccineId);

        document.getElementById(`vaxId-${centerId}`).value = '';
    };

    return (
        <>
        
            <Button className='addBtn' variant="warning" onClick={handleShowAddModal}>
                +
            </Button>
            <Modal className='' show={showAddModal} onHide={handleCloseAddModal}>
                <Modal.Header closeButton>
                    <Modal.Title>Add Center</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form onSubmit={handleSave}>
                        <div className='form-group'>
                            <label htmlFor='UserName'>Username</label>
                            <input type='text' id='UserName' name='UserName' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='Email'>Email</label>
                            <input type='Email' id='Email' name='Email' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='DisplayName'>Display Name</label>
                            <input type='text' id='DisplayName' name='DisplayName' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='PhoneNumber'>Phone Number</label>
                            <input type='text' id='PhoneNumber' name='PhoneNumber' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='Address'>Address</label>
                            <input type='text' id='Address' name='Address' />
                        </div>
                        <div className='form-group'>
                            <label htmlFor='Password'>Password</label>
                            <input type='Password' id='Password' name='Password' />
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
                {centers.map((center) => (
                    <div className='card' key={center.id}>
                        <h2>{center.displayName}</h2>
                        <ul>

                        </ul>
                        <form onSubmit={(event) => addVaccine(event, center.id)}>
                            <input id={`centreId-${center.id}`} type="hidden" placeholder="Add Vaccine" value={center.id}></input>
                            <input id={`vaxId-${center.id}`} type="text" placeholder="Add Vaccine"></input>
                            <Button variant="primary" type="submit">Add Vaccine</Button>
                        </form>
                        <Button variant="danger" onClick={() => handleDelete(center.id)}>Delete</Button>
                    </div>
                ))}
            </div>
        </>
    );
}

export default Admin;
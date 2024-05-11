import React from 'react';
import ListUsers from './Accept-Reject-Users';
import Admin from './Admin-Centers';
import Vaccine from './Admin-Vaccines';

import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Navbar from 'react-bootstrap/Navbar';
import 'bootstrap/dist/css/bootstrap.min.css';

const AdminPanel = () => {
    return (
        <div>
            <Navbar bg="dark" data-bs-theme="dark">
                <Container>
                    <Navbar.Brand href="">
                        <img
                            alt=""
                            src=""
                            width="30"
                            height="30"
                            className="d-inline-block align-top"
                        />{' '}
                        VaxCenter
                    </Navbar.Brand>
                </Container>
            </Navbar>
            <Row>

                <Col>   <ListUsers /> </Col>
                <Col>   <Admin /> </Col>
                <Col>   <Vaccine /> </Col>

            </Row>
        </div>
    );
};

export default AdminPanel;
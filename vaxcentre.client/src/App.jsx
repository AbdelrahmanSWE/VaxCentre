import { useEffect, useState } from 'react';
/*import SignupForm from './Signup-form'*/
import SigninForm from './Pages/Signin-form/Signin-form';
import Admin from './Pages/Admin-Centers/Admin-Centers';
import ListUsers from './Pages/Admin-Centers/Accept-Reject-Users';
//import Patient from './Patient-Centers'
import React from 'react';
//import PatientsVaccination from './Paitents-Vaccination';
import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

function App() {
    return (
        <>
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
            <Row><SigninForm/></Row>
            <Row>
                <Col><ListUsers /></Col>
                <Col xs={5}><Admin /></Col>
                <Col></Col>
            </Row>


        </>
    );
}

export default App;

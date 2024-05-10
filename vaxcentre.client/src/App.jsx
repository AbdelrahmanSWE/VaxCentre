import React from 'react';
import { Navbar, Container, Row, Col } from 'react-bootstrap';
import './App.css';
import Patient from './Pages/Patient-Centers/Paitent-Centers';

const App = () => {
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
                <Col><Patient /></Col> 
            </Row>
        </div>
    );
};

export default App;

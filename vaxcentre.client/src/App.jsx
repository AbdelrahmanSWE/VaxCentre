import { useEffect, useState } from 'react';
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
            <Row>
                <Col><ListUsers /></Col>
                <Col xs={5}><Admin /></Col>
                <Col><PatientsVaccination /></Col>
            </Row>


        </>
    );
}

export default App;

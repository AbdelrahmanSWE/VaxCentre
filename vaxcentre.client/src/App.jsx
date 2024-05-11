import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Vaccine from './Pages/Admin-Centers/Admin-Vaccines';
import Patient from './Pages/Patient-Centers/Paitent-Centers';
import AdminPanel from './Pages/Admin-Centers/Admin-Panal';
import SignIn from './Pages/Signin-form/Signin-form';
import SignUp from './Pages/Signup-form/Signup-form';

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<SignIn />} />
                <Route path="/signup" element={<SignUp />} />
                {localStorage.role === 'Admin' && <Route path="/admin" element={<AdminPanel />} />}
                {localStorage.role === 'Patient' && <Route path="/patient" element={<Patient />} />}
                {localStorage.role === 'VaccineCentre' && <Route path="/VaccineCentre" element={<Vaccine />} />}
            </Routes>
        </Router>
    );
};

export default App;
import React, { createContext, useState } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Vaccine from './Pages/Admin-Centers/Admin-Vaccines';
import Patient from './Pages/Patient-Centers/Paitent-Centers';
import AdminPanel from './Pages/Admin-Centers/Admin-Panal';
import SignIn from './Pages/Signin-form/Signin-form';
import SignUp from './Pages/Signup-form/Signup-form';

export const AuthContext = createContext();

const App = () => {
    const [authState, setAuthState] = useState({
        isAuthenticated: false,
        role: null
    });

    return (
        <AuthContext.Provider value={{ authState, setAuthState }}>
            <Router>
                <Navbar />
                <Switch>
                    <Route path="/signin" component={SignIn} />
                    <Route path="/signup" component={SignUp} />
                    {authState.isAuthenticated && authState.role === 'admin' && <Route path="/admin" component={AdminPanel} />}
                    {authState.isAuthenticated && authState.role === 'patient' && <Route path="/patient" component={Patient} />}
                </Switch>
            </Router>
        </AuthContext.Provider>
    );
};

export default App;
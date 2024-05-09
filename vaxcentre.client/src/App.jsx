import { useEffect, useState } from 'react';
import './App.css';
import SignupForm from './Pages/Signup-form/Signup-form';
import SigninForm from './Pages/Signin-form/Signin-form';
import ListUsers from './Pages/Admin-Centers/Accept-Reject-Users';
function App() {
    return (
        <>
         
            <ListUsers />
        </>
    );
}
export default App;
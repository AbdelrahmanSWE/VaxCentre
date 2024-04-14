import { useEffect, useState } from 'react';
import './App.css';
import SignupForm from './Pages/Signup-form/Signup-form';
import SigninForm from './Pages/Signin-form/Signin-form';

function App() {
    return (
        <>
        <SignupForm />
        <SigninForm />
        </>
    );
}
export default App;
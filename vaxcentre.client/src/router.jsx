import App from "./App";
import {SignupForm} from './Pages/Signup-form/Signup-form';
import {SigninForm} from './Pages/Signin-form/Signin-form';

import { createBrowserRouter } from "react-router-dom";

export const router = createBrowserRouter([
    {
        path: "", // localhost:3000
        element: <App />,
        children: [
            {
                path: "/register", // localhost:3000/register
                element: <SignupForm />
            },
            {
                path: "/login", // localhost:3000/login
                element: <SigninForm />
            },
        ]
    }
])
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './globals.css';
import './index.css';
import App from './App.tsx';
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router";
import { SignUpForm } from './components/SignupPage.tsx';
import { LoginForm } from './components/LoginPage.tsx';
import DemoPage from './components/ProjectDashboard.tsx';

let router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
    },
    {
        path: "/login",
        element: <LoginForm />,
    },
    {
        path: "/register",
        element: <SignUpForm />
    },
    {
        path: "/test",
        element: <DemoPage />
    }
]);

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router} />
  </StrictMode>,
)

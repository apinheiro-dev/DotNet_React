import React, { useState } from 'react';
import ReactDOM from "react-dom";
import style from './App.module.scss';
import AppHome from '../components/Home/AppHome';
import AppLogin from '../components/Login/Login';
import { AuthProvider, AuthContext } from '../context/AuthContext';
import { BrowserRouter as Router, Route, Routes, Navigate } from "react-router-dom";

interface PrivateRouteProps {
  element: React.ReactElement;
  path: string;
}

const App: React.FC = () => {
  return (
    <>
      <AuthProvider>
        <Router>
          <Routes>
            <Route path="/" element={<AppLogin />} />
            <Route path="/AppHome" element={<AppHome />} />
          </Routes>
        </Router>
      </AuthProvider>
    </>
  );
};


const PrivateRoute: React.FC<PrivateRouteProps> = ({ element, path }) => {
  const authContext = React.useContext(AuthContext);

  return (
    <>
      <Route
        path={path}
        element={
          authContext && authContext.authToken ? (
            element
          ) : (
            <Navigate to="/login" />
          )
        }
      />    
    </>
  );

};


ReactDOM.render(
  <React.StrictMode>
   <App />
  </React.StrictMode>,
  document.getElementById('root')
);

export default App;


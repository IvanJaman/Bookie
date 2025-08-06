import React from 'react';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import Login from './pages/Login/Login';
import Welcome from './pages/Welcome/Welcome';
import RegisterUser from './pages/RegisterUser/RegisterUser';
import RegisterPublisher from './pages/RegisterPublisher/RegisterPublisher';
import Home from './pages/Home/Home';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Welcome />} />
        <Route path="/register-user" element={<RegisterUser />} />
        <Route path="/register-publisher" element={<RegisterPublisher />} />
        <Route path="/login" element={<Login />} /> 
        <Route path="/home" element={<Home />} />
        
      </Routes>
    </Router>
  );
}

export default App;

import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/Login/Login';

import Welcome from './pages/Welcome/Welcome';
import RegisterUser from './pages/RegisterUser/RegisterUser';
import RegisterPublisher from './pages/RegisterPublisher/RegisterPublisher';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Welcome />} />
        <Route path="/register-user" element={<RegisterUser />} />
        <Route path="/register-publisher" element={<RegisterPublisher />} />
        <Route path="/login" element={<Login />} /> 
      </Routes>
    </Router>
  );
}

export default App;

import React, { useEffect, useState } from 'react';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import Login from './pages/Login/Login';
import Welcome from './pages/Welcome/Welcome';
import RegisterUser from './pages/RegisterUser/RegisterUser';
import RegisterPublisher from './pages/RegisterPublisher/RegisterPublisher';
import Home from './pages/Home/Home';
import Layout from './components/Layout';
import { getUserRole } from './utils/auth';

function App() {
  const [role, setRole] = useState(getUserRole());

  useEffect(() => {
    const checkRole = () => setRole(getUserRole());
    window.addEventListener("storage", checkRole);

    const interval = setInterval(checkRole, 1000);

    return () => {
      window.removeEventListener("storage", checkRole);
      clearInterval(interval);
    };
  }, []);

  return (
    <Router>
      <Routes>
        <Route path="/" element={<Welcome />} />
        <Route path="/register-user" element={<RegisterUser />} />
        <Route path="/register-publisher" element={<RegisterPublisher />} />
        <Route path="/login" element={<Login />} /> 
        <Route
          path="/home"
          element={
            <Layout userRole={role}>
              <Home />
            </Layout>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;

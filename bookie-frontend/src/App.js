import React, { useEffect, useState } from 'react';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import Login from './pages/Login/Login';
import Welcome from './pages/Welcome/Welcome';
import RegisterUser from './pages/RegisterUser/RegisterUser';
import RegisterPublisher from './pages/RegisterPublisher/RegisterPublisher';
import Home from './pages/Home/Home';
import Layout from './components/Layout';
import AddNewBook from './pages/AddNewBook/AddNewBook';
import MyProfile from './pages/MyProfile/MyProfile';
import { getUserRole } from './utils/auth';
import UpdateProfile from './pages/UpdateProfile/UpdateProfile';
import ChangePassword from './pages/ChangePassword/ChangePassword';
import CreateShelf from './pages/CreateShelf/CreateShelf';
import ShelfDetail from './pages/ShelfDetail/ShelfDetail';

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
        <Route
          path="/add-new-book"
          element={
            <Layout userRole={role}>
              <AddNewBook />
            </Layout>
          }
        />
        <Route 
          path="/myProfile" 
          element={
            <Layout userRole={role}>
              <MyProfile />
            </Layout>
          } 
        />
        <Route 
          path="/update-profile" 
          element={
            <Layout userRole={role}>
              <UpdateProfile />
            </Layout>
          } 
        />
        <Route 
          path="/change-password" 
          element={
            <Layout userRole={role}>
              <ChangePassword />
            </Layout>
          } 
        />
        <Route 
          path="/create-shelf" 
          element={
            <Layout userRole={role}>
              <CreateShelf />
            </Layout>
          } 
        />
        <Route 
          path="/myshelves/:shelfId"
          element={
            <Layout userRole={role}>
            <ShelfDetail />
          </Layout>
          } 
        />
      </Routes>
    </Router>
  );
}

export default App;

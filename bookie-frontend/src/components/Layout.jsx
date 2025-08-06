import React from 'react';
import Navbar from './Navbar';

function Layout({ children, userRole }) {
  return (
    <>
      <Navbar userRole={userRole} />
      <main className="container mt-4">{children}</main>
    </>
  );
}

export default Layout;

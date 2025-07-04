import React, { ReactNode } from 'react';
import './Layout.css';

interface LayoutProps {
  isLoggedIn: boolean;
  onLogout: () => void;
  onShowLogin: () => void;
  onShowRegister: () => void;
  children:ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ isLoggedIn, onLogout, onShowLogin, onShowRegister, children }) => {
  return (
  <div className="layout-container">
    <nav className="navbar">
      <span className="navbar-title">LIA Recensionen</span>
      <div className="navbar-buttons">
        {!isLoggedIn && (
          <>
            <button className="nav-button" onClick={onShowLogin}>Logga in</button>
            <button className="nav-button" onClick={onShowRegister}>Registrera</button>
          </>
        )}
        {isLoggedIn && (
          <button className="nav-button" onClick={onLogout}>Logga ut</button>
        )}
      </div>
    </nav>

    <main className="main-content">
      {children}
    </main>

    <footer className="footer">
      © 2025 LIA Recensioner. All rights reserved.
    </footer>
  </div>
);    
  
};

export default Layout;

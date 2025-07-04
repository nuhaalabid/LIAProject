import React, { useState, useEffect } from 'react';
import Login from './Login';
import UserProfile from './UserProfile';
import Layout from './Layout';
import Register from './Register'; 

function App() {
  const [token, setToken] = useState<string | null>(null);
  const [view, setView] = useState<'login' | 'register' | 'profile'>('login');

  useEffect(() => {
    const savedToken = localStorage.getItem('token');
    if (savedToken) {
      setToken(savedToken);
      setView('profile');
    }
  }, []);

  const handleLoginSuccess = (token: string) => {
    localStorage.setItem('token', token);
    setToken(token);
    setView('profile');
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    setToken(null);
    setView('login');
  };

  const handleShowLogin = () => setView('login');
  const handleShowRegister = () => setView('register');

  return (
    <Layout
      isLoggedIn={!!token}
      onLogout={handleLogout}
      onShowLogin={handleShowLogin}
      onShowRegister={handleShowRegister}
    >
      {view === 'login' && <Login onLoginSuccess={handleLoginSuccess} />}
      {view === 'register' && <Register onRegisterSuccess={handleShowLogin} />}
      {view === 'profile' && token && <UserProfile onLogout={handleLogout} />}
    </Layout>
  );
}

export default App;

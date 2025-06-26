import React, { useState } from 'react';
import api from './axios';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [token, setToken] = useState('');

  const handleLogin = async () => {
    try {
      const response = await api.post('/Users/login', {
        email,
        password,
      });

      const data = response.data;
      localStorage.setItem('token', data.token);
      setToken(data.token);
      alert('Inloggning lyckades!');
    } catch (error) {
      alert('Fel e-post eller lösenord');
    }
  };

  return (
    <div>
      <h2>Logga in</h2>
      <input
        type="text"
        placeholder="E-post"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <input
        type="password"
        placeholder="Lösenord"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin}>Logga in</button>
      {token && <p> Token sparad!</p>}
    </div>
  );
};

export default Login;

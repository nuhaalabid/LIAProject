import React, { useState } from 'react';
import api from './axios';

interface LoginProps {
  onLoginSuccess: (token: string) => void;
}

const Login: React.FC<LoginProps> = ({ onLoginSuccess }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async () => {
    try {
      const response = await api.post('/Users/login', {
        email,
        password,
      });

      const data = response.data;
      onLoginSuccess(data.token); 
      alert('Inloggning lyckades!');
    } catch (error) {
      alert('Fel e-post eller lösenord');
    }
  };

  return (
    <div className="login-container">
      <h2>Logga in</h2>
      <input
        type="text"
        placeholder="E-post"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        className="login-input"
      />
      <input
        type="password"
        placeholder="Lösenord"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        className="login-input"
      />
      <button onClick={handleLogin} className="login-button">Logga in</button>
    </div>
  );

}
export default Login;

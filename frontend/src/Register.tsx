import React, { useState } from 'react';
import api from './axios';
interface RegisterProps {
  onRegisterSuccess: () => void;
}

  const Register: React.FC<RegisterProps> = ({ onRegisterSuccess }) => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState('Student');
  const handleRegister = async () => {
    try {
      const response = await api.post('/Users/register', {
        name,
        email,
        password,
        role
      });
      alert('Registrering lyckades!');
      console.log(response.data);
      onRegisterSuccess(); 
    } catch (error) {
      alert('Fel vid registrering');
      console.error(error);
    }
  };

  return (
    <div className="register-container">
      <h2>Registrera</h2>
      <input
        type="text"
        placeholder="Namn"
        value={name}
        onChange={(e) => setName(e.target.value)}
        className="register-input"
      />
      <input
        type="text"
        placeholder="E-post"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        className="register-input"
      />
      <input
        type="password"
        placeholder="Lösenord"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        className="register-input"
      />

      <select
        value={role}
        onChange={(e) => setRole(e.target.value)}
        className="register-input"
      >
        <option value="Student">Student</option>
        <option value="Admin">Admin</option>
      </select>

      <button onClick={handleRegister} className="register-button">
        Registrera
      </button>
    </div>
  );
};

export default Register;

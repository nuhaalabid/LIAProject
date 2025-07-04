import React from 'react';

interface Company {
  id: number;
  name: string;
  description: string; 
}

interface CompanyListProps {
  companies: Company[];
}

const CompanyList: React.FC<CompanyListProps> = ({ companies }) => (
  <ul>
    {companies.map(c => (
      <li key={c.id}>
        <strong>{c.name}</strong><br />
        {c.description}
      </li>
    ))}
  </ul>
);

export default CompanyList;

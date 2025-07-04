import React from 'react';

interface Company {
  id: number;
  name: string;
}

interface Review {
  id: number;
  title: string;
  comment: string;
  rating: number;
  companyId: number; 
}

interface ReviewListProps {
  reviews: Review[];
  companies: Company[];
}

const ReviewList: React.FC<ReviewListProps> = ({ reviews, companies }) => {
  const getCompanyName = (companyId: number) => {
    const company = companies.find(c => c.id === companyId);
    return company ? company.name : 'Okänt företag';
  };

  return (
    <ul>
      {reviews.map(r => (
        <li key={r.id}>
          <strong>{getCompanyName (r.companyId)}</strong><br />
          {r.comment}<br />
          <em>{r.rating}/5</em>
          
        </li>
      ))}
    </ul>
  );
};

export default ReviewList;

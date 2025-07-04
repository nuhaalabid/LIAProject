import React, { useEffect, useState } from 'react';
import api from './axios';
import CompanyList from './CompanyList';
import ReviewList from './ReviewList';
import ReviewForm from './ReviewForm';
import './UserProfile.css';

interface UserProfileProps {
  onLogout: () => void;
}

interface User {
  name: string;
  email: string;
  role: string;
}

interface Company {
  id: number;
  name: string;
  description: string;
}

interface Review {
  id: number;
  title: string;
  comment: string;
  rating: number;
  companyId: number; 
}

const UserProfile: React.FC<UserProfileProps> = ({ onLogout }) => {
  const [user, setUser] = useState<User | null>(null);
  const [companies, setCompanies] = useState<Company[]>([]);
  const [reviews, setReviews] = useState<Review[]>([]);

  const fetchData = async () => {
    try {
      const [userRes, companiesRes, reviewsRes] = await Promise.all([
        api.get('/Users'),
        api.get('/Companies'),
        api.get('/Reviews'),
      ]);

      setUser(userRes.data);
      setCompanies(companiesRes.data);
      setReviews(reviewsRes.data);
    } catch (error) {
      console.error(error);
      alert('Kunde inte hämta data, token saknas eller ogiltig');
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  if (!user) return <p>Laddar...</p>;

  return (
     <div className="page-wrapper">
      <div className="user-profile-container">
        <h2>Inloggad: {user.name}</h2>

        <section className="company-section">
          <h3>Företag</h3>
          <CompanyList companies={companies} />
        </section>

        <section className="review-section">
          <h3>Recensioner</h3>
          <ReviewList reviews={reviews} companies={companies} />
        </section>

        <section className="review-form-section">
          <h3>Skriv ny recension</h3>
          <ReviewForm companies={companies} onReviewSubmit={fetchData} />
        </section>

        <button onClick={onLogout} className="logout-button">Logga ut</button>
      </div>
    </div>

  );

};

export default UserProfile;

import React, { useState } from 'react';
import api from './axios';

interface ReviewFormProps {
  companies: { id: number; name: string }[];
  onReviewSubmit: () => void;
}

const ReviewForm: React.FC<ReviewFormProps> = ({ companies, onReviewSubmit }) => {
  const [companyId, setCompanyId] = useState<number>(companies[0]?.id || 0);
  const [title, setTitle] = useState('');
  const [comment, setComment] = useState('');
  const [rating, setRating] = useState(1);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await api.post('/Reviews', {
        companyId,
        title,
        comment,
        rating,
      });

      alert('Recension skickad!');
      setTitle('');
      setComment('');
      setRating(1);
      onReviewSubmit(); 
    } catch (error) {
      console.error(error);
      alert('Fel vid skapande av recension');
    }
  };

  return (
    <form className="review-form" onSubmit={handleSubmit}>
      <label htmlFor="company">Företag</label>
      <select
        id="company"
        value={companyId}
        onChange={(e) => setCompanyId(Number(e.target.value))}
      >
        {companies.map((c) => (
          <option key={c.id} value={c.id}>{c.name}</option>
        ))}
      </select>

      <label htmlFor="title">Titel</label>
      <input
        type="text"
        id="title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        required
      />

      <label htmlFor="comment">Kommentar</label>
      <textarea
        id="comment"
        value={comment}
        onChange={(e) => setComment(e.target.value)}
        required
      />

      <label htmlFor="rating">Betyg (1-5)</label>
      <input
        type="number"
        id="rating"
        min="1"
        max="5"
        value={rating}
        onChange={(e) => setRating(Number(e.target.value))}
        required
      />

      <button type="submit">Skicka Recension</button>
    </form>
  );
};

export default ReviewForm;

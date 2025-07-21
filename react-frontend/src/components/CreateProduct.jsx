import { useState } from 'react';
import { createProduct } from '../services/productService';

function CreateProduct() {
  const [name, setName] = useState('');
  const [price, setPrice] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createProduct({ productName: name, price: parseFloat(price) });
      setSuccess('Product created successfully!');
      setName('');
      setPrice('');
    } catch (err) {
      setError('Failed to create product.');
    }
  };

  return (
    <div className="card p-4">
      <h2>Create Product</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      {success && <div className="alert alert-success">{success}</div>}
      <div className="mb-3">
        <label htmlFor="name" className="form-label">Product Name</label>
        <input
          type="text"
          className="form-control"
          id="name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
      </div>
      <div className="mb-3">
        <label htmlFor="price" className="form-label">Price</label>
        <input
          type="number"
          className="form-control"
          id="price"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
          required
        />
      </div>
      <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Create</button>
    </div>
  );
}

export default CreateProduct;
import { useState, useEffect } from 'react';
import { searchProducts, addToCart, removeFromCart, getCart } from '../services/productService';

function ProductSearch() {
  const [searchTerm, setSearchTerm] = useState('');
  const [products, setProducts] = useState([]);
  const [cart, setCart] = useState([]);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchCart = async () => {
      try {
        const cartItems = await getCart();
        setCart(cartItems);
      } catch (err) {
        setError('Failed to load cart.');
      }
    };
    fetchCart();
  }, []);

  const handleSearch = async () => {
    try {
      const results = await searchProducts(searchTerm);
      setProducts(results);
    } catch (err) {
      setError('Failed to search products.');
    }
  };

  const handleAddToCart = async (productId) => {
    try {
      await addToCart(productId);
      const updatedCart = await getCart();
      setCart(updatedCart);
    } catch (err) {
      setError('Failed to add to cart.');
    }
  };

  const handleRemoveFromCart = async (productId) => {
    try {
      await removeFromCart(productId);
      const updatedCart = await getCart();
      setCart(updatedCart);
    } catch (err) {
      setError('Failed to remove from cart.');
    }
  };

  return (
    <div className="card p-4">
      <h2>Product Search & Cart</h2>
      {error && <div className="alert alert-danger">{error}</div>}
      <div className="mb-3">
        <input
          type="text"
          className="form-control"
          placeholder="Search products..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <button className="btn btn-primary mt-2" onClick={handleSearch}>Search</button>
      </div>
      <div className="product-list">
        {products.map((product) => (
          <div key={product.id} className="product-item">
            <span>{product.productName} - <span>&#8358;</span>{product.price}</span>
            <button className="btn btn-success btn-sm ms-2" onClick={() => handleAddToCart(product.id)}>
              Add to Cart
            </button>
          </div>
        ))}
      </div>
      <h3 className="mt-4">Cart</h3>
      <div className="cart-list">
        {cart.map((item) => (
          <div key={item.id} className="cart-item">
            <span>{item.productName} - <span>&#8358;</span>{item.price}</span>
            <button className="btn btn-danger btn-sm" onClick={() => handleRemoveFromCart(item.productId)}>
              Remove
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ProductSearch;
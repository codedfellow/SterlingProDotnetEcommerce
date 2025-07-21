import axios from 'axios';

const API_URL = 'http://localhost:28705';

export async function createProduct(product) {
  const token = localStorage.getItem('token');
  await axios.post(`${API_URL}/api/product/add`, product, {
    headers: { Authorization: `Bearer ${token}` }
  });
}

export async function searchProducts(searchTerm) {
  const token = localStorage.getItem('token');
  const response = await axios.get(`${API_URL}/api/product/search?searchTerm=${searchTerm}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  return response.data;
}

export async function addToCart(productId) {
  const token = localStorage.getItem('token');
  await axios.post(`${API_URL}/api/cart/add-to-cart`, { productId }, {
    headers: { Authorization: `Bearer ${token}` }
  });
}

export async function removeFromCart(productId) {
  const token = localStorage.getItem('token');
  await axios.delete(`${API_URL}/api/cart/remove-from-cart/${productId}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
}

export async function getCart() {
  const token = localStorage.getItem('token');
  const response = await axios.get(`${API_URL}/api/cart/get-cart-items`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  return response.data;
}
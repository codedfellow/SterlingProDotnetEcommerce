import axios from 'axios';
import { jwtDecode }from 'jwt-decode';

const API_URL = 'http://localhost:28705';

export async function login(email, password) {
  const response = await axios.post(`${API_URL}/api/Auth/login`, { email, password });
  console.log('Login response:', response.data);
  return response.data;
}

export async function register(email, userName, password) {
  await axios.post(`${API_URL}/api/auth/register`, { email, userName, password });
}

export function isAuthenticated() {
  const token = localStorage.getItem('token');
  if (!token) return false;

  try {
    const decoded = jwtDecode(token);
    const currentTime = Date.now() / 1000;
    return decoded.exp > currentTime; 
  } catch (err) {
    return false;
  }
}
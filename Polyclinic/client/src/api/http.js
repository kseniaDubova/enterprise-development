import axios from 'axios';

const API_URL = 'https://localhost:7270/api';

const http = axios.create({
    baseURL: API_URL,
});

export default http;
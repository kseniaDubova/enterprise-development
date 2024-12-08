import axios from 'axios';

const API_URL = 'https://192.168.0.104:7270/api';

const http = axios.create({
    baseURL: API_URL,
});

export default http;
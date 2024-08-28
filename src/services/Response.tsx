import axios from "axios";

const apiUrl = 'http://localhost:7119'

const api = axios.create({
    baseURL: apiUrl
});

export default api
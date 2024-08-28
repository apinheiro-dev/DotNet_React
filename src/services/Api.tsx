import axios from "axios";

const apiUrl = 'http://localhost:5160'

const api = axios.create({
    baseURL: apiUrl
});

export default api
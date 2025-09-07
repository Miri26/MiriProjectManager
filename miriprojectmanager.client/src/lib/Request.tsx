import axios from "axios";
import { getCookie, tokenCookieName } from "../components/Authentication";

const apiService = axios.create({
  baseURL: "https://localhost:7266/api",
  timeout: 10000,
  headers: {},
});

apiService.interceptors.request.use(
    (request) => {
        const token = getCookie(tokenCookieName); // custom cookie getter
    if (token) {
      request.headers.Authorization = `Bearer ${token}`;
    } else {
      delete request.headers.Authorization; // ensure clean requests
    }
    return request;
  },
  (error) => Promise.reject(error)
);

export { apiService };

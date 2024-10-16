import { JwtPayload } from "@/types";
import axios from "axios";
import { decodeToken, isTokenExpired } from "@/utils/jwt";
import { CONFIG } from "@/config/appConfig";
import { history } from "@/main";

const https = axios.create({
  baseURL: CONFIG.API.BASE_URL,
  headers: CONFIG.API.HEADERS,
  timeout: CONFIG.API.TIMEOUT,
});

https.interceptors.request.use(
  async (config: any) => {
    const user = localStorage.getItem('user');

    if (user) {
      const { accessToken } = JSON.parse(user);

      const decodedToken: JwtPayload | null = decodeToken(accessToken);

      if (isTokenExpired(accessToken)) {
        alert('Token has expired. Please login again.');
        history.push('/login');
        return Promise.reject(new Error('Token has expired'));
      }

      // Thêm accessToken vào header
      config.headers.Authorization = `Bearer ${accessToken}`;

      // Kiểm tra role của người dùng (ví dụ nếu cần role đặc biệt để truy cập)
      if (decodedToken?.Role === 'Admin') {
        console.log('User is an admin');
      }
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);


export default https;


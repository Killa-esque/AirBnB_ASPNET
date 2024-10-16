import https from "@/services/axiosClient";
import { storage } from "@/utils";
import { createAsyncThunk } from "@reduxjs/toolkit";

const loginAsync = createAsyncThunk(
  'auth/login',
  async ({ email, password }: { email: string; password: string }, { rejectWithValue }) => {
    try {
      const response = await https.post('/login', { email, password });

      const { accessToken, refreshToken, user } = response.data;

      storage.set('accessToken', accessToken);
      storage.set('refreshToken', refreshToken);
      storage.set('user', user);

      return { accessToken, refreshToken, user };
    } catch (error) {
      
      return rejectWithValue('Login failed');
    }
  }
);

export { loginAsync };

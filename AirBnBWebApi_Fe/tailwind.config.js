/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      container: {
        center: true,
      },
      fontFamily: {
        cereal: ['Cereal', 'sans-serif'],
      },
      colors: {
        // Light Mode Colors
        primary: '#FFFFFF',   // Màu nền chính (trắng)
        secondary: '#F0F0F0', // Màu nền phụ (xám nhạt)
        accent: '#FF5A5F',    // Màu nhấn (đỏ cam Airbnb)
        textLight: '#222222', // Màu văn bản chính (đen nhạt)

        // Dark Mode Colors
        darkPrimary: '#121212',   // Nền chính (xám rất đậm)
        darkSecondary: '#333333', // Nền phụ (xám đậm)
        darkAccent: '#FF5A5F',    // Màu nhấn (làm dịu đi so với light mode)
        textDark: '#E0E0E0',      // Màu văn bản (trắng nhẹ)
      },
    },
  },
  plugins: [],
};

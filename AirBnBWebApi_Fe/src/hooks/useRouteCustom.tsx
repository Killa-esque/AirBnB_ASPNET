// src/hooks/useRouteCustom.tsx
import React, { Suspense } from "react";
import { useRoutes, Navigate } from "react-router-dom";
import { AdminTemplate, UserTemplate } from "@/templates";
import { useAuth } from "./useAuth";


// Các page dành cho Admin/Host
const AdminDashboard = React.lazy(() => import('@/pages/Admin/DashBoard'));
const LocationManagementPage = React.lazy(() => import('@/pages/Admin/Location'));
const RoomManagementPage = React.lazy(() => import('@/pages/Admin/Room'));
const UserManagementPage = React.lazy(() => import('@/pages/Admin/User'));
const AdminProfilePage = React.lazy(() => import('@/pages/Admin/Profile'));
const AdminLoginPage = React.lazy(() => import('@/pages/Admin/Login'));
const RevenueManagementPage = React.lazy(() => import('@/pages/Admin/Revenue'));
const SupportManagementPage = React.lazy(() => import('@/pages/Admin/Support'));
const RoomApprovalPage = React.lazy(() => import('@/pages/Admin/RoomApproval'));

// Các page dành cho User
const HomePage = React.lazy(() => import('@/pages/User/Home'));
const LoginPage = React.lazy(() => import('@/pages/User/Login'));
const RegisterPage = React.lazy(() => import('@/pages/User/Register'));
const ProfilePage = React.lazy(() => import('@/pages/User/Profile'));
const DetailPage = React.lazy(() => import('@/pages/User/Detail'));
const SearchPage = React.lazy(() => import('@/pages/User/Search'));
const BookingPage = React.lazy(() => import('@/pages/User/Booking'));
const BookingHistoryPage = React.lazy(() => import('@/pages/User/BookingHistory'));
const FavouritesPage = React.lazy(() => import('@/pages/User/Favourites'));

const NotFoundPage = React.lazy(() => import('@/pages/NotFound'));

const useRouteCustom = () => {
  const { user } = useAuth();

  // Kiểm tra role của người dùng
  const isAdmin = user?.isAdmin || user?.isHost;

  const guestRoutes = [
    {
      path: "/",
      element: <Suspense fallback={<div>Loading...</div>}><HomePage /></Suspense>,
    },
    {
      path: "/login",
      element: <Suspense fallback={<div>Loading...</div>}><LoginPage /></Suspense>,
    },
    {
      path: "/register",
      element: <Suspense fallback={<div>Loading...</div>}><RegisterPage /></Suspense>,
    },
    {
      path: "*",
      element: <Navigate to="/" />,
    },
  ];

  const userRoutes = [
    {
      path: "/",
      element: <UserTemplate />,
      children: [
        { path: "/", element: <Suspense fallback={<div>Loading...</div>}><HomePage /></Suspense> },
        { path: "profile", element: <Suspense fallback={<div>Loading...</div>}><ProfilePage /></Suspense> },
        { path: "detail/:roomId", element: <Suspense fallback={<div>Loading...</div>}><DetailPage /></Suspense> },
        { path: "search", element: <Suspense fallback={<div>Loading...</div>}><SearchPage /></Suspense> },
        { path: "booking", element: <Suspense fallback={<div>Loading...</div>}><BookingPage /></Suspense> },
        { path: "my-bookings", element: <Suspense fallback={<div>Loading...</div>}><MyBookingsPage /></Suspense> },
        { path: "favorites", element: <Suspense fallback={<div>Loading...</div>}><FavoritesPage /></Suspense> },
        { path: "*", element: <Navigate to="/" /> },
      ],
    },
  ];

  // Định nghĩa route cho Admin (bao gồm cả Host)
  const adminRoutes = [
    {
      path: "/admin",
      element: <AdminTemplate />,  // Layout dành cho Admin/Host
      children: [
        { path: "dashboard", element: <Suspense fallback={<div>Loading...</div>}><DashBoard /></Suspense> },
        { path: "location-management", element: <Suspense fallback={<div>Loading...</div>}><LocationManagementPage /></Suspense> },
        { path: "room-management", element: <Suspense fallback={<div>Loading...</div>}><RoomManagementPage /></Suspense> },
        { path: "user-management", element: <Suspense fallback={<div>Loading...</div>}><UserManagementPage /></Suspense> },
        { path: "profile", element: <Suspense fallback={<div>Loading...</div>}><AdminProfilePage /></Suspense> },
        { path: "login", element: <Suspense fallback={<div>Loading...</div>}><AdminLoginPage /></Suspense> },
        { path: "revenue-management", element: <Suspense fallback={<div>Loading...</div>}><RevenueManagementPage /></Suspense> },
        { path: "support-management", element: <Suspense fallback={<div>Loading...</div>}><SupportManagementPage /></Suspense> },
        { path: "room-approval", element: <Suspense fallback={<div>Loading...</div>}><RoomApprovalPage /></Suspense> },
        { path: "*", element: <Navigate to="/admin/dashboard" /> },  // Điều hướng về dashboard nếu không tìm thấy route
      ],
    },
  ];

  // Kiểm tra trạng thái đăng nhập và phân quyền route
  const routes = isAuthenticated
    ? isAdmin
      ? adminRoutes  // Nếu là Admin/Host, sử dụng adminRoutes
      : userRoutes  // Nếu là User, sử dụng userRoutes
    : guestRoutes;  // Nếu chưa đăng nhập, sử dụng guestRoutes

  return useRoutes(routes);  // Trả về các route phù hợp
};

export default useRouteCustom;

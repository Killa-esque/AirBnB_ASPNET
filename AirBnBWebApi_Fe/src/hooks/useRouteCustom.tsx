// src/hooks/useRouteCustom.tsx
import React, { Suspense, useEffect } from "react";
import { useLocation, useRoutes } from "react-router-dom";
import { AdminTemplate, UserTemplate } from "@/templates";
import Loading from "@/components/Common/Loading";
import useAuth from "./useAuth";
import ROUTES from "@/constants/routes";
import routeTitles from "@/constants/routeTitles";

// Admin
const AdminDashboard = React.lazy(() => import('@/pages/Admin/DashBoard'));
const LocationManagementPage = React.lazy(() => import('@/pages/Admin/Location'));
const RoomManagementPage = React.lazy(() => import('@/pages/Admin/Room'));
const UserManagementPage = React.lazy(() => import('@/pages/Admin/User'));
const AdminProfilePage = React.lazy(() => import('@/pages/Admin/Profile'));
const AdminLoginPage = React.lazy(() => import('@/pages/Admin/Login'));
const RevenueAdminPage = React.lazy(() => import('@/pages/Admin/Revenue'));
const SupportManagementPage = React.lazy(() => import('@/pages/Admin/Support'));

// Host
const HostDashboard = React.lazy(() => import('@/pages/Host/DashBoard'));
const HostProfilePage = React.lazy(() => import('@/pages/Host/Profile'));
const RevenueHostPage = React.lazy(() => import('@/pages/Host/Revenue'));
const HostLoginPage = React.lazy(() => import('@/pages/Host/Login'));
const HostRegisterPage = React.lazy(() => import('@/pages/Host/Register'));
const AirBnBManagementPage = React.lazy(() => import('@/pages/Host/AirBnB'));
const BookingRequestsPage = React.lazy(() => import('@/pages/Host/BookingRequest'));

// User
const HomePage = React.lazy(() => import('@/pages/User/Home'));
const LoginPage = React.lazy(() => import('@/pages/User/Login'));
const RegisterPage = React.lazy(() => import('@/pages/User/Register'));
const ProfilePage = React.lazy(() => import('@/pages/User/Profile'));
const DetailPage = React.lazy(() => import('@/pages/User/Detail'));
const SearchPage = React.lazy(() => import('@/pages/User/Search'));
const BookingPage = React.lazy(() => import('@/pages/User/Booking'));
const BookingHistoryPage = React.lazy(() => import('@/pages/User/BookingHistory'));
const FavouritesPage = React.lazy(() => import('@/pages/User/Favourites'));

// Trang 404 Not Found
const NotFoundPage = React.lazy(() => import('@/pages/NotFound'));

const useRouteCustom = () => {
  const { user } = useAuth();
  const location = useLocation();

  const isAdmin = user?.isAdmin;
  const isHost = user?.isHost;

  // Cập nhật title dựa trên route
  useEffect(() => {
    const path = location.pathname;
    const title = routeTitles[path] || "Default Title";
    document.title = title;
  }, [location.pathname]);

  // Guest routes (login, register)
  const guestRoutes = [
    { path: ROUTES.HOME, element: <Suspense fallback={<Loading />}><HomePage /></Suspense> },
    { path: ROUTES.LOGIN, element: <Suspense fallback={<Loading />}><LoginPage /></Suspense> },
    { path: ROUTES.REGISTER, element: <Suspense fallback={<Loading />}><RegisterPage /></Suspense> },
    // Admin và Host login đều nằm ngoài các templates
    { path: ROUTES.ADMIN_LOGIN, element: <Suspense fallback={<Loading />}><AdminLoginPage /></Suspense> },
    { path: ROUTES.HOST_LOGIN, element: <Suspense fallback={<Loading />}><HostLoginPage /></Suspense> },
    { path: ROUTES.NOT_FOUND, element: <Suspense fallback={<Loading />}><NotFoundPage /></Suspense> },
  ];

  // Admin routes
  const adminRoutes = [
    {
      path: ROUTES.ADMIN_DASHBOARD,
      element: <AdminTemplate />, // Template chính dành cho Admin
      children: [
        { path: ROUTES.ADMIN_DASHBOARD, element: <Suspense fallback={<Loading />}><AdminDashboard /></Suspense> },
        { path: ROUTES.ADMIN_LOCATION_MANAGEMENT, element: <Suspense fallback={<Loading />}><LocationManagementPage /></Suspense> },
        { path: ROUTES.ADMIN_ROOM_MANAGEMENT, element: <Suspense fallback={<Loading />}><RoomManagementPage /></Suspense> },
        { path: ROUTES.ADMIN_USER_MANAGEMENT, element: <Suspense fallback={<Loading />}><UserManagementPage /></Suspense> },
        { path: ROUTES.ADMIN_REVENUE, element: <Suspense fallback={<Loading />}><RevenueAdminPage /></Suspense> },
        { path: ROUTES.ADMIN_PROFILE, element: <Suspense fallback={<Loading />}><AdminProfilePage /></Suspense> },
        { path: ROUTES.ADMIN_SUPPORT_MANAGEMENT, element: <Suspense fallback={<Loading />}><SupportManagementPage /></Suspense> },
        { path: ROUTES.NOT_FOUND, element: <Suspense fallback={<Loading />}><NotFoundPage /></Suspense> },
      ],
    },
  ];

  // Host routes
  const hostRoutes = [
    {
      path: ROUTES.HOST_DASHBOARD,
      element: <AdminTemplate />, // Template chính dành cho Host
      children: [
        { path: ROUTES.HOST_DASHBOARD, element: <Suspense fallback={<Loading />}><HostDashboard /></Suspense> },
        { path: ROUTES.HOST_AIRBNB_MANAGEMENT, element: <Suspense fallback={<Loading />}><AirBnBManagementPage /></Suspense> },
        { path: ROUTES.HOST_BOOKING_REQUESTS, element: <Suspense fallback={<Loading />}><BookingRequestsPage /></Suspense> },
        { path: ROUTES.HOST_REVENUE, element: <Suspense fallback={<Loading />}><RevenueHostPage /></Suspense> },
        { path: ROUTES.HOST_PROFILE, element: <Suspense fallback={<Loading />}><HostProfilePage /></Suspense> },
        { path: ROUTES.NOT_FOUND, element: <Suspense fallback={<Loading />}><NotFoundPage /></Suspense> },
      ],
    },
  ];

  // User routes
  const userRoutes = [
    {
      path: ROUTES.HOME,
      element: <UserTemplate />,
      children: [
        { path: ROUTES.HOME, element: <Suspense fallback={<Loading />}><HomePage /></Suspense> },
        { path: ROUTES.USER_PROFILE, element: <Suspense fallback={<Loading />}><ProfilePage /></Suspense> },
        { path: ROUTES.ROOM_DETAIL, element: <Suspense fallback={<Loading />}><DetailPage /></Suspense> },
        { path: ROUTES.SEARCH, element: <Suspense fallback={<Loading />}><SearchPage /></Suspense> },
        { path: ROUTES.BOOKING, element: <Suspense fallback={<Loading />}><BookingPage /></Suspense> },
        { path: ROUTES.BOOKING_HISTORY, element: <Suspense fallback={<Loading />}><BookingHistoryPage /></Suspense> },
        { path: ROUTES.FAVORITES, element: <Suspense fallback={<Loading />}><FavouritesPage /></Suspense> },
        { path: ROUTES.NOT_FOUND, element: <Suspense fallback={<Loading />}><NotFoundPage /></Suspense> },
      ],
    },
  ];

  const routes = user
    ? isAdmin
      ? adminRoutes
      : isHost
        ? hostRoutes
        : userRoutes
    : guestRoutes;

  return useRoutes(routes);
};

export default useRouteCustom;

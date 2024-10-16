// src/constants/routes.ts
const ROUTES = {
  HOME: "/",
  LOGIN: "/login",
  REGISTER: "/register",
  USER_PROFILE: "/profile",
  ROOM_DETAIL: "/detail/:roomId",
  SEARCH: "/search",
  BOOKING: "/booking",
  BOOKING_HISTORY: "/booking-history",
  FAVORITES: "/favorites",

  // Admin routes
  ADMIN_DASHBOARD: "/admin/dashboard",
  ADMIN_LOCATION_MANAGEMENT: "/admin/location-management",
  ADMIN_ROOM_MANAGEMENT: "/admin/room-management",
  ADMIN_USER_MANAGEMENT: "/admin/user-management",
  ADMIN_REVENUE: "/admin/revenue",
  ADMIN_PROFILE: "/admin/profile",
  ADMIN_SUPPORT_MANAGEMENT: "/admin/support-management",
  ADMIN_LOGIN: "/admin/login",

  // Host routes
  HOST_DASHBOARD: "/host/dashboard",
  HOST_AIRBNB_MANAGEMENT: "/host/airbnb-management",
  HOST_BOOKING_REQUESTS: "/host/booking-requests",
  HOST_REVENUE: "/host/revenue",
  HOST_PROFILE: "/host/profile",
  HOST_LOGIN: "/host/login",
  HOST_REGISTER: "/host/register",

  // Not Found
  NOT_FOUND: "*",
};

export default ROUTES;

const pathRoot = {
  homePage: '/',
  notFound: '*',
  search: '/search',
  detail: '/detail/:propertyId',
  login: '/login',
  register: '/register',
};

const pathGuest = {
  ...pathRoot,
  about: '/about',
  contact: '/contact',
  faq: '/faq',
};

const pathUser = {
  profile: '/user/profile',
  bookingHistory: '/user/booking-history',
  favorite: '/user/favorite',
  settings: '/user/settings',
  bookingDetail: (bookingId: string) => `/user/booking/${bookingId}`,
};

const pathHost = {
  dashboard: '/host/dashboard',
  manageProperties: '/host/manage-properties',
  addProperty: '/host/add-property',
  viewBookings: (propertyId: string) => `/host/property/${propertyId}/bookings`,
  editProperty: (propertyId: string) => `/host/property/${propertyId}/edit`,
};


export { pathRoot, pathGuest, pathUser, pathHost };

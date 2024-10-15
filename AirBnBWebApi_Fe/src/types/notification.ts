export interface Notification {
  id: number;
  message: string;
  type: 'success' | 'error' | 'info';
}

export interface NotificationContextProps {
  notifications: Notification[];
  addNotification: (notification: Notification) => void;
  removeNotification: (id: number) => void;
}

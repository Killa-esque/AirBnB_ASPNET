import { NotificationContextType } from "@/types";
import React, { createContext, useContext } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const useNotification = (): NotificationContextType => {
  const context = useContext(NotificationContext);
  if (!context) {
    throw new Error("useNotification must be used within a NotificationProvider");
  }
  return context;
};

// NotificationProvider cung cấp logic thông báo cho toàn bộ ứng dụng
export const NotificationProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const handleNotification = (content: string, type: "info" | "success" | "warning" | "error") => {
    return toast[type](content, {
      position: "top-right",
      autoClose: 5000,
      hideProgressBar: false,
      pauseOnHover: true,
    });
  };

  return (
    <NotificationContext.Provider value={{ handleNotification }}>
      {children}
      <ToastContainer />
    </NotificationContext.Provider>
  );
};

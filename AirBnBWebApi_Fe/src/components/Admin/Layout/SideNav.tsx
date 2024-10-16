import { Menu } from "antd";
import { NavLink, useLocation } from "react-router-dom";
import {
  DashboardOutlined,
  ToolOutlined,
  UserOutlined,
  TeamOutlined,
  EnvironmentOutlined,
} from '@ant-design/icons';

import logo from "@/assets/images/logo.png";

interface MenuItem {
  key: string;
  icon: React.ReactNode;
  label: string;
  path: string;
}

const menuItems: MenuItem[] = [
  { key: "dashboard", icon: <DashboardOutlined />, label: "Dashboard", path: "/dashboard" },
  { key: "rooms", icon: <ToolOutlined />, label: "Rooms", path: "/rooms" },
  { key: "locations", icon: <EnvironmentOutlined />, label: "Locations", path: "/locations" },
  { key: "users", icon: <TeamOutlined />, label: "Users", path: "/users" },
  { key: "profile", icon: <UserOutlined />, label: "Profile", path: "/profile" },
];

function SideNav() {
  const { pathname } = useLocation();
  const selectedKey = pathname.split("/")[1] || "dashboard";

  return (
    <>
      <div className="brand">
        <img src={logo} alt="Logo" />
        <span>Admin Dashboard</span>
      </div>
      <hr />
      <Menu theme="light" mode="inline" selectedKeys={[selectedKey]}>
        {menuItems.map((item) => (
          <Menu.Item key={item.key} icon={item.icon}>
            <NavLink to={item.path}>{item.label}</NavLink>
          </Menu.Item>
        ))}
      </Menu>
    </>
  );
}

export { SideNav };

// src/templates/AdminTemplate.tsx
import { useContext, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { Layout, Drawer } from "antd";
import { AuthContext } from "@/contexts";
import { Footer, Header, SideNav } from "@/components/Admin/Layout";

const { Header: AntHeader, Sider } = Layout;

function AdminTemplate() {
  const { user } = useContext(AuthContext);
  const [drawerVisible, setDrawerVisible] = useState<boolean>(false);
  const { pathname } = useLocation();

  const toggleDrawer = () => setDrawerVisible(!drawerVisible);

  if (!user) {
    return <Navigate to="/login" />;
  }

  return (
    <Layout className="layout-dashboard">
      <Drawer
        title={null}
        placement="left"
        closable={false}
        onClose={() => setDrawerVisible(false)}
        open={drawerVisible}
        width={250}
        className="drawer-sidebar"
      >
        <SideNav />
      </Drawer>

      <Sider
        breakpoint="lg"
        collapsedWidth="0"
        onBreakpoint={(broken) => {
          if (broken) setDrawerVisible(false);
        }}
        trigger={null}
        width={250}
        theme="light"
        className="sider-primary ant-layout-sider-primary"
      >
        <SideNav />
      </Sider>

      <Layout>
        <AntHeader>
          <Header onPress={toggleDrawer} name={pathname} subName={pathname} />
        </AntHeader>

        <Outlet />
        <Footer />
      </Layout>
    </Layout>
  );
}

export { AdminTemplate };

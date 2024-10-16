// src/components/Admin/Layout/Header.tsx
import { useContext, useState } from "react";
import { Row, Col, Breadcrumb, Button, Drawer, Typography } from "antd";
import { NavLink } from "react-router-dom";
import { SettingOutlined, MenuOutlined, UserOutlined } from '@ant-design/icons';
import { AuthContext } from "@/contexts";

const { Title, Text } = Typography;

interface HeaderProps {
  name: string;
  subName: string;
  onPress: () => void;
}

function Header({ name, subName, onPress }: HeaderProps) {
  const { user, signOut } = useContext(AuthContext);
  const [visible, setVisible] = useState<boolean>(false);

  const showDrawer = () => setVisible(true);
  const hideDrawer = () => setVisible(false);

  return (
    <>
      <Row gutter={[24, 0]}>
        <Col span={24} md={6}>
          <Breadcrumb>
            <Breadcrumb.Item>
              <NavLink to="/">Pages</NavLink>
            </Breadcrumb.Item>
            <Breadcrumb.Item>{name.replace("/", "")}</Breadcrumb.Item>
          </Breadcrumb>
          <div className="ant-page-header-heading">
            <span className="ant-page-header-heading-title" style={{ textTransform: "capitalize" }}>
              {subName.replace("/", "")}
            </span>
          </div>
        </Col>
        <Col span={24} md={18} className="header-control">
          <Button type="link" onClick={showDrawer}>
            <SettingOutlined />
          </Button>

          <Button
            type="link"
            className="sidebar-toggler"
            onClick={onPress}
          >
            <MenuOutlined />
          </Button>

          <Drawer
            className="settings-drawer"
            mask={true}
            width={360}
            onClose={hideDrawer}
            placement="right"
            open={visible}
          >
            <div>
              <div className="header-top">
                <Title level={4}>
                  Configurator
                  <Text className="subtitle">See our dashboard options.</Text>
                </Title>
              </div>

              <div className="buttonSignOut">
                <Button type="primary" onClick={signOut}>
                  Sign Out
                </Button>
              </div>
            </div>
          </Drawer>

          {user ? (
            <NavLink to="/profile" className="btn-sign-in">
              <UserOutlined />
              <span>{user.fullName}</span>
            </NavLink>
          ) : (
            <NavLink to="/login" className="btn-sign-in">
              <UserOutlined />
              <span>Sign in</span>
            </NavLink>
          )}
        </Col>
      </Row>
    </>
  );
}

export { Header };

import { Row, Col, Breadcrumb, Button, Drawer, Typography, Switch } from "antd";
import { NavLink, Link } from "react-router-dom";

import { SettingOutlined, MenuOutlined, UserOutlined } from '@ant-design/icons';
import { useDrawer } from "@/hooks";
import { storage } from "@/utils";



type Props = {
  name: string;
  subName: string;
  onPress: () => void;
};

function Header({
  name,
  subName,
  onPress,
}: Props) {
  const { Title, Text } = Typography;

  const { visible, showDrawer, hideDrawer } = useDrawer();

  // Render Login logic
  const renderLogin = () => {
    if (storage.get(USER_LOGIN)) {
      return (
        <Link to="/profile" className="btn-sign-in">
          <UserOutlined />
          <span>{getStorageJson(USER_LOGIN).name}</span>
        </Link>
      );
    } else {
      return (
        <Link to="/login" className="btn-sign-in">
          <UserOutlined />
          <span>Sign in</span>
        </Link>
      );
    }
  };

  // Sidenav type mặc định là 'transparent'
  const sidenavType = "transparent";

  return (
    <>
      <div className="setting-drwer" onClick={showDrawer}>
        <SettingOutlined />
      </div>
      <Row gutter={[24, 0]}>
        <Col span={24} md={6}>
          <Breadcrumb>
            <Breadcrumb.Item>
              <NavLink to="/">Pages</NavLink>
            </Breadcrumb.Item>
            <Breadcrumb.Item>
              {name.replace("/", "")}
            </Breadcrumb.Item>
          </Breadcrumb>
          <div className="ant-page-header-heading">
            <span
              className="ant-page-header-heading-title"
              style={{ textTransform: "capitalize" }}
            >
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
            onClick={() => onPress()}
          >
            <MenuOutlined />
          </Button>

          <Drawer
            className="settings-drawer"
            mask={true}
            width={360}
            onClose={hideDrawer}
            placement={'right'}
            open={visible}
          >
            <div>
              <div className="header-top">
                <Title level={4}>
                  Configurator
                  <Text className="subtitle">See our dashboard options.</Text>
                </Title>
              </div>

              <div className="sidebarnav-color mb-2">
                <Title level={5}>Sidenav Type</Title>
                <Text>Sidenav is set to: {sidenavType.toUpperCase()}</Text>
              </div>

            </div>

            <div className="buttonSignOut">
              <button onClick={() => {
                window.localStorage.clear();
                window.location.reload();
              }}>Sign Out</button>
            </div>
          </Drawer>
          {renderLogin()}
        </Col>
      </Row>
    </>
  );
}

export default Header;

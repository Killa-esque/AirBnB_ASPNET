// src/components/Admin/Layout/Footer.tsx
import { Layout, Row, Col } from "antd";
import { HeartFilled } from "@ant-design/icons";
import { NavLink } from "react-router-dom";

const { Footer: AntFooter } = Layout;

function Footer() {
  return (
    <AntFooter style={{ background: "#fafafa" }}>
      <Row justify="space-between" align="middle">
        <Col xs={24} md={12} lg={12}>
          <div className="copyright">
            © 2024, made with <HeartFilled style={{ color: 'red' }} /> by{" "}
            <NavLink to="/about" className="font-weight-bold">
              Phu Vinh
            </NavLink>
            {" "}for a better web.
          </div>
        </Col>
        <Col xs={24} md={12} lg={12}>
          <div className="footer-menu">
            <ul>
              <li className="nav-item">
                <NavLink to="/about" className="nav-link text-muted">
                  Phu Vinh
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink to="/about-us" className="nav-link text-muted">
                  About Us
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink to="/blog" className="nav-link text-muted">
                  Blog
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink to="/license" className="nav-link pe-0 text-muted">
                  License
                </NavLink>
              </li>
            </ul>
          </div>
        </Col>
      </Row>
    </AntFooter>
  );
}

export { Footer };

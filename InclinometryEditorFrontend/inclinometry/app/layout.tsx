

import { Layout } from "antd";
import "./globals.css";
import { Content } from "antd/es/layout/layout";

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Layout style = { {minHeight: "100vh"}}>
          <Content style = {{padding: "0.48px"}}>
            {children}
          </Content>
        </Layout>
      </body>
    </html>
  );
}

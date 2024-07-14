"use client"

import { Layout } from "antd";
import "./globals.css";
import { Content } from "antd/es/layout/layout";
import Keycloak from 'keycloak-js';
import {useKeycloak, ReactKeycloakProvider} from "@react-keycloak/web";


export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {

  const configKeycloak = {
    url: "http://localhost:8080/",
    realm: "InclinometryAuth",
    clientId: "inclinometryapp-client"
  }

  const  authInstance = new Keycloak(configKeycloak);
  

  return (
    <html lang="en">

        <body>
          <Layout style = { {minHeight: "100vh"}}>
            <Content style = {{padding: "0.48px"}}>
            <ReactKeycloakProvider
                authClient={authInstance}>
              {children}
            </ReactKeycloakProvider>
            </Content>
          </Layout>
        </body>

    </html>
  );
  
}

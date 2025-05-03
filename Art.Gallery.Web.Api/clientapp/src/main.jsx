import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
// ==============|Components|============== //
import AppRouter from "./configs/router/AppRoutes";
// ==============|Css Files|============== //
import "./assets/css/style-rtl.css";
import "./assets/css/Custom.css";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <AppRouter />
  </StrictMode>
);

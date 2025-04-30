import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
// ==============|Components|============== //
import AppRouter from "./configs/router/AppRoutes";
// ==============|Css Files|============== //
import "./assets/css/style-rtl.css";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <AppRouter />
  </StrictMode>
);

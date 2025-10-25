import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import MainLayout from "./layouts/MainLayout";
import Home from "./client/pages/Home";
import Headphones from "./client/pages/Headphones";
import Speakers from "./client/pages/Speakers";
import Earphones from "./client/pages/Earphones";
import ProductDetail from "./client/components/ProductDetail";
import Login from "./client/pages/Login";
import Register from "./client/pages/Register";
import AdminLayout from "./layouts/AdminLayout";
import Dashboard from "./admin/Pages/Dashboard";
import Customers from "./admin/Pages/Customers";
import Catalog from "./admin/Pages/Catalog";
import Orders from "./admin/Pages/Orders";
import AdminHome from "./admin/Pages/AdminHome";

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainLayout />,
    children: [
      { index: true, element: <Home /> },
      { path: "/home", element: <Home /> },
      { path: "/headphones", element: <Headphones /> },
      { path: "/speakers", element: <Speakers /> },
      { path: "/earphones", element: <Earphones /> },
      { path: ":category/:id", element: <ProductDetail /> },
    ],
  },
  {
    path: "/admin",
    element: <AdminLayout />,
    children: [
      { index: true, element: <AdminHome /> },
      { path: "dashboard", element: <Dashboard /> },
      { path: "customers", element: <Customers /> },
      { path: "products", element: <Catalog /> },
      { path: "orders", element: <Orders /> },
    ],
  },
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/register",
    element: <Register />,
  },
]);

function App() {
  return (
    <div>
      <ToastContainer position="bottom-right" theme="dark" autoClose={3000} />
      <RouterProvider router={router} />
    </div>
  );
}

export default App;

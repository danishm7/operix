import { Button } from "@/components/ui/button";
import { useAuth } from "@/features/auth/AuthContext";
import { Outlet, useNavigate } from "react-router-dom";

function AppLayout() {
  const { logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login", { replace: true });
  };

  return (
    <div className="min-h-screen">
      <h1 className="p-8 text-2xl font-semibold">Operix</h1>

      <Button variant="outline" onClick={handleLogout}>
        Logout
      </Button>

      <Outlet />
    </div>
  );
}

export default AppLayout;

import { LogOut } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { useAuth } from "@/features/auth/AuthContext";

function AppHeader() {
  const { logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login", { replace: true });
  };

  return (
    <header className="flex h-16 shrink-0 items-center justify-end border-b bg-card px-6">
      <div className="flex items-center gap-4">
        <div className="text-right">
          <p className="text-sm font-medium text-foreground">Danish Malak</p>

          <p className="text-xs text-muted-foreground">danish@example.com</p>
        </div>

        <div className="flex size-9 items-center justify-center rounded-full bg-primary text-sm font-medium text-primary-foreground">
          DM
        </div>

        <div className="h-6 w-px bg-border" />

        <button
          type="button"
          onClick={handleLogout}
          className="flex items-center gap-2 rounded-md px-3 py-2 text-sm text-muted-foreground transition-colors hover:bg-muted hover:text-foreground"
        >
          <LogOut className="size-4" />
          <span>Sign out</span>
        </button>
      </div>
    </header>
  );
}

export default AppHeader;

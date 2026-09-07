import { LogOut, UserRound } from "lucide-react";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";

import { useAuth } from "@/features/auth/AuthContext";

function AppHeader() {
  const { logout, user } = useAuth();
  const navigate = useNavigate();

  const [isProfileOpen, setIsProfileOpen] = useState(false);
  const profileRef = useRef<HTMLDivElement>(null);

  // Close the profile dropdown when clicking outside or pressing Escape
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        profileRef.current &&
        !profileRef.current.contains(event.target as Node)
      ) {
        setIsProfileOpen(false);
      }
    };

    const handleEscape = (event: KeyboardEvent) => {
      if (event.key === "Escape") {
        setIsProfileOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    document.addEventListener("keydown", handleEscape);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
      document.removeEventListener("keydown", handleEscape);
    };
  }, []);

  const handleLogout = () => {
    setIsProfileOpen(false);
    logout();
    navigate("/login", { replace: true });
  };

  return (
    <header className="flex h-16 shrink-0 items-center justify-end border-b bg-card px-6">
      <div ref={profileRef} className="relative">
        <button
          type="button"
          onClick={() => setIsProfileOpen((open) => !open)}
          aria-expanded={isProfileOpen}
          aria-haspopup="menu"
          className="flex items-center gap-3 rounded-md px-2 py-1.5 transition-colors hover:bg-muted"
        >
          <div className="text-right">
            <p className="text-sm font-medium text-foreground">
              {user ? `${user.firstName} ${user.lastName ?? ""}`.trim() : ""}
            </p>

            <p className="text-xs text-muted-foreground">{user?.email}</p>
          </div>

          <div className="flex size-9 items-center justify-center rounded-full bg-primary text-sm font-medium text-primary-foreground">
            {user
              ? `${user.firstName[0]}${user.lastName?.[0] ?? ""}`.toUpperCase()
              : ""}
          </div>
        </button>

        {isProfileOpen && (
          <div
            role="menu"
            className="absolute right-0 top-full z-50 mt-2 w-64 overflow-hidden rounded-lg border bg-card shadow-md"
          >
            <button
              type="button"
              role="menuitem"
              onClick={() => setIsProfileOpen(false)}
              className="flex w-full items-center gap-3 rounded-md px-3 py-2 text-sm text-foreground transition-colors hover:bg-muted"
            >
              <UserRound className="size-4 text-muted-foreground" />

              <span>Profile</span>
            </button>

            <button
              type="button"
              role="menuitem"
              onClick={handleLogout}
              className="flex w-full items-center gap-3 rounded-md px-3 py-2 text-sm text-foreground transition-colors hover:bg-muted"
            >
              <LogOut className="size-4 text-muted-foreground" />

              <span>Sign out</span>
            </button>
          </div>
        )}
      </div>
    </header>
  );
}

export default AppHeader;

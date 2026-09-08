import { ChevronDown, LogOut, Search, UserRound } from "lucide-react";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";

import { useAuth } from "@/features/auth/AuthContext";

function AppHeader() {
  const { logout, currentUser } = useAuth();
  const navigate = useNavigate();

  const [isProfileOpen, setIsProfileOpen] = useState(false);
  const profileRef = useRef<HTMLDivElement>(null);

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
    <header className="flex h-16 shrink-0 items-center justify-between border-b border-border bg-card/80 px-6 backdrop-blur-sm">
      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2 rounded-xl border border-border bg-muted px-3 py-2 text-sm text-muted-foreground shadow-sm">
          <Search className="size-4 text-muted-foreground" />
          <span>Search assets, work orders...</span>
        </div>
      </div>

      <div className="flex items-center gap-3">
        {/* <button
          type="button"
          className="flex items-center gap-2 rounded-xl border border-border bg-muted px-3 py-2 text-sm font-medium text-muted-foreground transition-colors hover:bg-accent hover:text-accent-foreground"
        >
          <CalendarDays className="size-4" />
          This week
        </button> */}

        {/* <button
          type="button"
          className="flex size-10 items-center justify-center rounded-xl border border-border bg-muted text-muted-foreground transition-colors hover:bg-accent hover:text-accent-foreground"
          aria-label="Notifications"
        >
          <Bell className="size-4" />
        </button> */}

        <div ref={profileRef} className="relative">
          <button
            type="button"
            onClick={() => setIsProfileOpen((open) => !open)}
            aria-expanded={isProfileOpen}
            aria-haspopup="menu"
            className="flex items-center gap-3 border-l border-border px-3 py-1.5 text-left transition-colors hover:bg-muted/50"
          >
            <div className="flex size-9 items-center justify-center rounded-full bg-primary text-xs font-semibold text-primary-foreground">
              {currentUser
                ? `${currentUser.firstName[0]}${currentUser.lastName?.[0] ?? ""}`.toUpperCase()
                : ""}
            </div>

            <div>
              <p className="text-sm font-semibold leading-tight text-card-foreground">
                {currentUser
                  ? `${currentUser.firstName} ${currentUser.lastName ?? ""}`.trim()
                  : ""}
              </p>

              <p className="mt-0.5 text-xs leading-tight text-muted-foreground">
                {currentUser?.email}
              </p>
            </div>

            <ChevronDown
              className="ml-1 size-4 text-muted-foreground"
              aria-hidden="true"
            />
          </button>

          {isProfileOpen && (
            <div
              role="menu"
              className="absolute right-0 top-full z-50 mt-2 w-64 overflow-hidden rounded-2xl border border-border bg-popover shadow-lg"
            >
              <button
                type="button"
                role="menuitem"
                onClick={() => setIsProfileOpen(false)}
                className="flex w-full items-center gap-3 px-3 py-2.5 text-sm text-popover-foreground transition-colors hover:bg-muted"
              >
                <UserRound className="size-4 text-muted-foreground" />
                <span>Profile</span>
              </button>

              <button
                type="button"
                role="menuitem"
                onClick={handleLogout}
                className="flex w-full items-center gap-3 px-3 py-2.5 text-sm text-popover-foreground transition-colors hover:bg-muted"
              >
                <LogOut className="size-4 text-muted-foreground" />
                <span>Sign out</span>
              </button>
            </div>
          )}
        </div>
      </div>
    </header>
  );
}

export default AppHeader;

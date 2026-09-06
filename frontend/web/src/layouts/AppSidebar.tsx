import {
  Building2,
  LayoutDashboard,
  MapPin,
  Shield,
  Users,
} from "lucide-react";
import { NavLink } from "react-router-dom";

import OperixLogo from "@/components/OperixLogo";

const navigationItems = [
  {
    title: "Dashboard",
    url: "/dashboard",
    icon: LayoutDashboard,
  },
];

const administrationItems = [
  {
    title: "Users",
    url: "/users",
    icon: Users,
  },
  {
    title: "Roles",
    url: "/roles",
    icon: Shield,
  },
  {
    title: "Departments",
    url: "/departments",
    icon: Building2,
  },
  {
    title: "Locations",
    url: "/locations",
    icon: MapPin,
  },
];

interface NavigationItemProps {
  title: string;
  url: string;
  icon: typeof LayoutDashboard;
}

function NavigationItem({ title, url, icon: Icon }: NavigationItemProps) {
  return (
    <NavLink
      to={url}
      className={({ isActive }) =>
        [
          "flex items-center gap-3 rounded-md px-3 py-2 text-sm transition-colors",
          isActive
            ? "bg-muted font-medium text-foreground"
            : "text-muted-foreground hover:bg-muted hover:text-foreground",
        ].join(" ")
      }
    >
      {({ isActive }) => (
        <>
          <Icon
            className={[
              "size-4 shrink-0",
              isActive ? "text-primary" : "text-muted-foreground",
            ].join(" ")}
          />

          <span>{title}</span>
        </>
      )}
    </NavLink>
  );
}

function AppSidebar() {
  return (
    <aside className="flex h-screen w-64 shrink-0 flex-col border-r bg-card">
      <div className="flex h-16 items-center border-b px-6">
        <OperixLogo className="text-2xl" />
      </div>

      <nav className="flex-1 space-y-6 overflow-y-auto px-3 py-5">
        <div className="space-y-1">
          {navigationItems.map((item) => (
            <NavigationItem key={item.title} {...item} />
          ))}
        </div>

        <div>
          <div className="mb-2 px-3 text-xs font-medium uppercase tracking-wider text-muted-foreground">
            Administration
          </div>

          <div className="space-y-1">
            {administrationItems.map((item) => (
              <NavigationItem key={item.title} {...item} />
            ))}
          </div>
        </div>
      </nav>
    </aside>
  );
}

export default AppSidebar;

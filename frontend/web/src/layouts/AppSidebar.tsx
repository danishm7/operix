import {
  BarChart3,
  Building2,
  ClipboardList,
  Factory,
  Gauge,
  LayoutDashboard,
  MapPin,
  Package,
  Settings,
  Shield,
  Users,
  Wrench,
} from "lucide-react";
import { NavLink } from "react-router-dom";

import OperixLogo from "@/components/OperixLogo";

const navigationGroups = [
  {
    label: "Overview",
    items: [
      { title: "Dashboard", url: "/dashboard", icon: LayoutDashboard },
      { title: "Operations", url: "/operations", icon: Gauge },
      { title: "Reports", url: "/reports", icon: BarChart3 },
    ],
  },
  {
    label: "Maintenance",
    items: [
      { title: "Assets", url: "/assets", icon: Factory },
      { title: "Work Orders", url: "/work-orders", icon: ClipboardList },
      { title: "Preventive", url: "/preventive", icon: Wrench },
      { title: "Inventory", url: "/inventory", icon: Package },
    ],
  },
  {
    label: "Administration",
    items: [
      { title: "Users", url: "/users", icon: Users },
      { title: "Roles", url: "/roles", icon: Shield },
      { title: "Departments", url: "/departments", icon: Building2 },
      { title: "Locations", url: "/locations", icon: MapPin },
      { title: "Settings", url: "/settings", icon: Settings },
    ],
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
          "flex items-center gap-3 rounded-xl px-3 py-2.5 text-sm font-medium transition-all",
          isActive
            ? "bg-primary text-primary-foreground shadow-sm"
            : "text-muted-foreground hover:bg-muted hover:text-foreground",
        ].join(" ")
      }
    >
      {({ isActive }) => (
        <>
          <Icon
            className={[
              "size-4 shrink-0",
              isActive ? "text-primary-foreground" : "text-muted-foreground",
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
    <aside className="flex h-screen w-72 shrink-0 flex-col border-r border-border bg-background/80 backdrop-blur-sm">
      <div className="flex h-20 items-center border-b border-border px-6">
        <OperixLogo className="text-2xl" />
      </div>

      <nav className="flex-1 space-y-7 overflow-y-auto px-4 py-5">
        {navigationGroups.map((group) => (
          <div key={group.label} className="space-y-2">
            <div className="px-3 text-[10px] font-semibold uppercase tracking-[0.18em] text-muted-foreground">
              {group.label}
            </div>

            <div className="space-y-1.5">
              {group.items.map((item) => (
                <NavigationItem key={item.title} {...item} />
              ))}
            </div>
          </div>
        ))}
      </nav>
    </aside>
  );
}

export default AppSidebar;

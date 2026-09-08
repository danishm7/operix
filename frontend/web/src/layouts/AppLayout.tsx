import { Outlet } from "react-router-dom";

import AppHeader from "@/layouts/AppHeader";
import AppSidebar from "@/layouts/AppSidebar";

function AppLayout() {
  return (
    <div className="flex h-screen overflow-hidden bg-background">
      <AppSidebar />

      <div className="flex min-w-0 flex-1 flex-col">
        <AppHeader />

        <main className="app-main-surface min-h-0 flex-1 overflow-y-auto bg-background p-6">
          <div className="mx-auto max-w-7xl">
            <Outlet />
          </div>
        </main>
      </div>
    </div>
  );
}

export default AppLayout;

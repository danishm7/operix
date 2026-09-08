import { Pencil, Plus, UserX } from "lucide-react";

import { DataTable, type DataTableColumn } from "@/components/DataTable";
import ErrorMessage from "@/components/ErrorMessage";
import Loader from "@/components/Loader";
import StatusBadge from "@/components/StatusBadge";
import Tooltip from "@/components/Tooltip";
import { useAuth } from "@/features/auth/AuthContext";
import type { User } from "@/features/users/usersApi";
import { useUsers } from "@/features/users/usersQuery";
import { useNavigate } from "react-router-dom";

function UsersPage() {
  const { currentUser } = useAuth();

  if (!currentUser) return null;

  const {
    data: users,
    isLoading,
    isError,
  } = useUsers(currentUser.organizationId);
  const navigate = useNavigate();

  const columns: DataTableColumn<User>[] = [
    {
      key: "name",
      header: "Name",
      render: (user) => (
        <span className="font-medium">
          {user.firstName} {user.lastName}
        </span>
      ),
    },
    {
      key: "email",
      header: "Email",
      render: (user) => (
        <span className="text-muted-foreground">{user.email}</span>
      ),
    },
    {
      key: "isActive",
      header: "Status",
      render: (user) => <StatusBadge isActive={user.isActive} />,
    },
    {
      key: "actions",
      header: "Actions",
      width: "100px",
      align: "right",
      render: (user) => (
        <div className="flex items-center justify-end gap-1">
          <Tooltip content="Edit user">
            <button
              type="button"
              aria-label="Edit user"
              onClick={() => navigate(`/users/${user.id}/edit`)}
              className="rounded-md p-1.5 text-muted-foreground transition-colors hover:bg-muted hover:text-foreground"
            >
              <Pencil className="size-4" />
            </button>
          </Tooltip>

          <Tooltip content="Deactivate user">
            <button
              type="button"
              aria-label="Deactivate user"
              onClick={() => {}}
              className="rounded-md p-1.5 text-muted-foreground transition-colors hover:bg-destructive/10 hover:text-destructive"
            >
              <UserX className="size-4" />
            </button>
          </Tooltip>
        </div>
      ),
    },
  ];

  if (isLoading) return <Loader message="Loading users..." />;
  if (isError) return <ErrorMessage message="Unable to load users." />;

  return (
    <div className="space-y-6">
      {/* Header with title and "Add User" button */}
      <div className="flex items-start justify-between">
        <div>
          <h1 className="text-2xl font-semibold tracking-tight">Users</h1>
        </div>

        <button
          type="button"
          className="flex items-center gap-2 rounded-md bg-primary px-4 py-2 text-sm font-medium text-primary-foreground transition-colors hover:bg-primary-hover"
          onClick={() => navigate("/users/new")}
        >
          <Plus className="size-4" />
          Add User
        </button>
      </div>

      {/* Users table */}
      <DataTable
        data={users || []}
        columns={columns}
        emptyMessage="No users found."
      />
    </div>
  );
}

export default UsersPage;

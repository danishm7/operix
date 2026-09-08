import { zodResolver } from "@hookform/resolvers/zod";
import { ArrowLeft, Loader2 } from "lucide-react";
import { useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";

import Dropdown, { type DropdownOption } from "@/components/Dropdown";
import ErrorMessage from "@/components/ErrorMessage";
import { showToast } from "@/components/Toast";
import { useAuth } from "@/features/auth/AuthContext";
import { useRoles } from "@/features/roles/rolesQuery";
import {
  userCreateSchema,
  userUpdateSchema,
  type UserFormData,
} from "@/features/users/userSchema";
import { createUser, updateUser } from "@/features/users/usersApi";
import { useUser, useUserRoles } from "@/features/users/usersQuery";
import { getApiErrorMessage } from "@/services/api/error";

function UserFormPage() {
  const { currentUser } = useAuth();

  if (!currentUser) return null;

  const navigate = useNavigate();
  const { id } = useParams();
  const userId = Number(id);
  const isEditMode = Boolean(id);

  const { data: user, isLoading, isError } = useUser(userId);
  const { data: userRoles = [], isLoading: areUserRolesLoading } =
    useUserRoles(userId);
  const { data: roles = [], isLoading: areRolesLoading } = useRoles(
    currentUser.organizationId,
  );

  const {
    register,
    handleSubmit,
    control,
    reset,
    formState: { errors, isSubmitting, isDirty },
  } = useForm<UserFormData>({
    resolver: zodResolver(isEditMode ? userUpdateSchema : userCreateSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      departmentId: null,
      roleIds: [],
      password: "",
      confirmPassword: "",
      isActive: true,
    },
  });

  const departmentOptions: DropdownOption[] = [
    { value: "1", label: "Maintenance" },
    { value: "2", label: "Operations" },
    { value: "3", label: "Engineering" },
  ];

  const roleOptions: DropdownOption[] = roles.map((role) => ({
    value: String(role.id),
    label: role.name,
  }));

  useEffect(() => {
    if (!user) return;

    reset({
      firstName: user.firstName,
      lastName: user.lastName ?? "",
      email: user.email,
      departmentId: user.departmentId,
      roleIds: userRoles.map((role) => role.id),
      password: "",
      confirmPassword: "",
      isActive: user.isActive,
    });
  }, [user, userRoles, reset]);

  const onSubmit = async (data: UserFormData) => {
    try {
      if (isEditMode) {
        if (!isDirty) {
          showToast("No changes made.", "info");
          navigate("/users");
          return;
        }

        await updateUser(userId, {
          departmentId: data.departmentId,
          roleIds: data.roleIds,
          firstName: data.firstName,
          lastName: data.lastName || null,
          email: data.email,
          isActive: data.isActive,
        });

        showToast("User updated successfully.", "success");
        navigate("/users");
        return;
      }

      await createUser({
        organizationId: currentUser.organizationId,
        departmentId: data.departmentId,
        roleIds: data.roleIds,
        firstName: data.firstName,
        lastName: data.lastName || null,
        email: data.email,
        password: data.password!,
      });

      showToast("User created successfully.", "success");
      navigate("/users");
    } catch (error) {
      showToast(getApiErrorMessage(error), "error");
    }
  };

  if (isEditMode && (isLoading || areUserRolesLoading)) {
    return (
      <div className="flex items-center justify-center gap-2 rounded-lg border bg-card py-12 text-sm text-muted-foreground">
        <Loader2 className="size-4 animate-spin" aria-hidden="true" />
        Loading user...
      </div>
    );
  }
  if (isEditMode && isError)
    return <ErrorMessage message="Unable to load user." />;

  return (
    <div className="space-y-6">
      <div className="flex items-start gap-4">
        <button
          type="button"
          onClick={() => navigate("/users")}
          aria-label="Back to users"
          className="mt-1 inline-flex size-9 shrink-0 items-center justify-center rounded-lg border border-border text-muted-foreground transition-colors hover:bg-muted hover:text-foreground"
        >
          <ArrowLeft className="size-4" />
        </button>

        <div>
          <h1 className="text-3xl font-semibold tracking-tight text-foreground">
            {isEditMode ? "Edit user" : "Add user"}
          </h1>

          <p className="mt-1 text-sm text-muted-foreground">
            {isEditMode
              ? "Update the user profile and access settings."
              : "Create a user and assign them to the right role(s)."}
          </p>
        </div>
      </div>

      <form
        onSubmit={handleSubmit(onSubmit)}
        className="overflow-hidden rounded-2xl border border-border bg-card shadow-sm"
      >
        <div className="grid gap-6 p-6 lg:grid-cols-2">
          <div className="space-y-2">
            <label
              htmlFor="firstName"
              className="text-sm font-medium text-card-foreground"
            >
              First name
            </label>

            <input
              id="firstName"
              type="text"
              placeholder="Enter first name"
              autoComplete="given-name"
              {...register("firstName")}
              className="h-11 w-full rounded-xl border border-input bg-muted px-3 text-sm text-card-foreground placeholder:text-muted-foreground transition-colors focus:border-ring focus:bg-card focus:ring-4 focus:ring-ring/20"
            />

            {errors.firstName && (
              <p className="text-sm text-destructive">
                {errors.firstName.message}
              </p>
            )}
          </div>

          <div className="space-y-2">
            <label
              htmlFor="lastName"
              className="text-sm font-medium text-card-foreground"
            >
              Last name
            </label>

            <input
              id="lastName"
              type="text"
              placeholder="Enter last name"
              autoComplete="family-name"
              {...register("lastName")}
              className="h-11 w-full rounded-xl border border-input bg-muted px-3 text-sm text-card-foreground placeholder:text-muted-foreground transition-colors focus:border-ring focus:bg-card focus:ring-4 focus:ring-ring/20"
            />

            {errors.lastName && (
              <p className="text-sm text-destructive">
                {errors.lastName.message}
              </p>
            )}
          </div>

          <div className="space-y-2 lg:col-span-2">
            <label
              htmlFor="email"
              className="text-sm font-medium text-card-foreground"
            >
              Email address
            </label>

            <input
              id="email"
              type="email"
              placeholder="Enter email address"
              autoComplete="email"
              {...register("email")}
              className="h-11 w-full rounded-xl border border-input bg-muted px-3 text-sm text-card-foreground placeholder:text-muted-foreground transition-colors focus:border-ring focus:bg-card focus:ring-4 focus:ring-ring/20"
            />

            {errors.email && (
              <p className="text-sm text-destructive">{errors.email.message}</p>
            )}
          </div>

          <div className="space-y-2 lg:col-span-2">
            <Controller
              name="departmentId"
              control={control}
              render={({ field }) => (
                <Dropdown
                  data={departmentOptions}
                  label="Department"
                  placeholder="Select department"
                  value={
                    departmentOptions.find(
                      (option) => option.value === String(field.value),
                    ) ?? null
                  }
                  onChange={(option) =>
                    field.onChange(option ? Number(option.value) : null)
                  }
                  error={errors.departmentId?.message}
                />
              )}
            />
          </div>

          <div className="space-y-2 lg:col-span-2">
            <Controller
              name="roleIds"
              control={control}
              render={({ field }) => (
                <Dropdown
                  data={roleOptions}
                  label="Roles"
                  placeholder="Select roles"
                  searchPlaceholder="Search roles..."
                  emptyMessage="No roles available."
                  isMulti
                  searchable
                  loading={areRolesLoading}
                  value={roleOptions.filter((option) =>
                    field.value.includes(Number(option.value)),
                  )}
                  onChange={(options) =>
                    field.onChange(
                      options.map((option) => Number(option.value)),
                    )
                  }
                  error={errors.roleIds?.message}
                />
              )}
            />
          </div>
        </div>

        {!isEditMode && (
          <div className="grid gap-6 px-6 pb-6 lg:grid-cols-2">
            <div className="space-y-2">
              <label
                htmlFor="password"
                className="text-sm font-medium text-card-foreground"
              >
                Password
              </label>

              <input
                id="password"
                type="password"
                placeholder="Enter password"
                autoComplete="new-password"
                {...register("password")}
                className="h-11 w-full rounded-xl border border-input bg-muted px-3 text-sm text-card-foreground placeholder:text-muted-foreground transition-colors focus:border-ring focus:bg-card focus:ring-4 focus:ring-ring/20"
              />

              {errors.password && (
                <p className="text-sm text-destructive">
                  {errors.password.message}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <label
                htmlFor="confirmPassword"
                className="text-sm font-medium text-card-foreground"
              >
                Confirm password
              </label>

              <input
                id="confirmPassword"
                type="password"
                placeholder="Confirm password"
                autoComplete="new-password"
                {...register("confirmPassword")}
                className="h-11 w-full rounded-xl border border-input bg-muted px-3 text-sm text-card-foreground placeholder:text-muted-foreground transition-colors focus:border-ring focus:bg-card focus:ring-4 focus:ring-ring/20"
              />

              {errors.confirmPassword && (
                <p className="text-sm text-destructive">
                  {errors.confirmPassword.message}
                </p>
              )}
            </div>
          </div>
        )}

        {isEditMode && (
          <div className="px-6 pb-6">
            <label className="flex items-center gap-3 rounded-xl border border-border bg-muted px-4 py-3">
              <input
                id="is-active"
                type="checkbox"
                {...register("isActive")}
                className="size-4 rounded border-input accent-primary"
              />

              <span className="text-sm font-medium text-card-foreground">
                Active user
              </span>
            </label>
          </div>
        )}

        <div className="flex items-center justify-end gap-3 border-t border-border bg-muted px-6 py-4">
          <button
            type="button"
            onClick={() => navigate("/users")}
            className="rounded-xl border border-border bg-card px-4 py-2.5 text-sm font-medium text-card-foreground transition-colors hover:bg-accent"
          >
            Cancel
          </button>

          <button
            type="submit"
            disabled={isSubmitting}
            className="rounded-xl bg-primary px-4 py-2.5 text-sm font-medium text-primary-foreground transition-colors hover:bg-primary-hover disabled:cursor-not-allowed disabled:opacity-60"
          >
            {isSubmitting ? (
              <Loader2 className="size-4 animate-spin" aria-label="Saving" />
            ) : isEditMode ? (
              "Save changes"
            ) : (
              "Create user"
            )}
          </button>
        </div>
      </form>
    </div>
  );
}

export default UserFormPage;

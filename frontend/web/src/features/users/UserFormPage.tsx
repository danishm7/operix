import { zodResolver } from "@hookform/resolvers/zod";
import { ArrowLeft } from "lucide-react";
import { useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";

import ErrorMessage from "@/components/ErrorMessage";
import Loader from "@/components/Loader";
import { showToast } from "@/components/Toast";
import { useAuth } from "@/features/auth/AuthContext";
import {
  userCreateSchema,
  userUpdateSchema,
  type UserFormData,
} from "@/features/users/userSchema";
import { createUser, updateUser } from "@/features/users/usersApi";
import { useUser } from "@/features/users/usersQuery";
import { getApiErrorMessage } from "@/services/api/error";
import { useEffect } from "react";

function UserFormPage() {
  const navigate = useNavigate();
  const { id } = useParams();
  const userId = Number(id);
  const isEditMode = Boolean(id);

  const { data: user, isLoading, isError } = useUser(userId);
  const { currentUser } = useAuth();

  if (!currentUser) return null;

  // Initialize the form with react-hook-form and zod validation
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting, isDirty },
  } = useForm<UserFormData>({
    resolver: zodResolver(isEditMode ? userUpdateSchema : userCreateSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      departmentId: null,
      password: "",
      confirmPassword: "",
      isActive: true,
    },
  });

  // If in edit mode, populate the form with the user's existing data
  useEffect(() => {
    if (!user) return;

    reset({
      firstName: user.firstName,
      lastName: user.lastName ?? "",
      email: user.email,
      departmentId: user.departmentId,
      password: "",
      confirmPassword: "",
      isActive: user.isActive,
    });
  }, [user, reset]);

  // Handle form submission for both create and edit modes
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

  if (isEditMode && isLoading) return <Loader message="Loading user..." />;
  if (isEditMode && isError)
    return <ErrorMessage message="Unable to load user." />;

  return (
    <div className="space-y-6">
      <div>
        <button
          type="button"
          onClick={() => navigate("/users")}
          className="mb-4 flex items-center gap-2 text-sm text-muted-foreground transition-colors hover:text-foreground"
        >
          <ArrowLeft className="size-4" />
          Back to users
        </button>

        <h1 className="text-2xl font-semibold tracking-tight">
          {isEditMode ? "Edit user" : "Add user"}
        </h1>

        <p className="mt-1 text-sm text-muted-foreground">
          {isEditMode
            ? "Update the user's information."
            : "Create a new user in your organization."}
        </p>
      </div>

      <form
        onSubmit={handleSubmit(onSubmit)}
        className="w-full overflow-hidden rounded-lg border bg-card"
      >
        <div className="grid gap-5 p-6 sm:grid-cols-2">
          <div className="space-y-2">
            <label htmlFor="firstName" className="text-sm font-medium">
              First name
            </label>

            <input
              id="firstName"
              type="text"
              placeholder="Enter first name"
              autoComplete="given-name"
              {...register("firstName")}
              className="h-10 w-full rounded-md border bg-background px-3 text-sm outline-none transition-colors placeholder:text-muted-foreground focus:border-ring focus:ring-2 focus:ring-ring/20"
            />

            {errors.firstName && (
              <p className="text-sm text-destructive">
                {errors.firstName.message}
              </p>
            )}
          </div>

          <div className="space-y-2">
            <label htmlFor="lastName" className="text-sm font-medium">
              Last name
            </label>

            <input
              id="lastName"
              type="text"
              placeholder="Enter last name"
              autoComplete="family-name"
              {...register("lastName")}
              className="h-10 w-full rounded-md border bg-background px-3 text-sm outline-none transition-colors placeholder:text-muted-foreground focus:border-ring focus:ring-2 focus:ring-ring/20"
            />

            {errors.lastName && (
              <p className="text-sm text-destructive">
                {errors.lastName.message}
              </p>
            )}
          </div>

          <div className="space-y-2">
            <label htmlFor="email" className="text-sm font-medium">
              Email
            </label>

            <input
              id="email"
              type="email"
              placeholder="Enter email address"
              autoComplete="email"
              {...register("email")}
              className="h-10 w-full rounded-md border bg-background px-3 text-sm outline-none transition-colors placeholder:text-muted-foreground focus:border-ring focus:ring-2 focus:ring-ring/20"
            />

            {errors.email && (
              <p className="text-sm text-destructive">{errors.email.message}</p>
            )}
          </div>

          <div className="space-y-2">
            <label htmlFor="departmentId" className="text-sm font-medium">
              Department
            </label>

            <select
              id="departmentId"
              {...register("departmentId", {
                setValueAs: (value) => (value === "" ? null : Number(value)),
              })}
              className="h-10 w-full rounded-md border bg-background px-3 text-sm outline-none transition-colors focus:border-ring focus:ring-2 focus:ring-ring/20"
            >
              <option value="">Select department</option>
              <option value="1">Maintenance</option>
              <option value="2">Operations</option>
              <option value="3">Engineering</option>
            </select>

            {errors.departmentId && (
              <p className="text-sm text-destructive">
                {errors.departmentId.message}
              </p>
            )}
          </div>
        </div>

        {!isEditMode && (
          <div className="px-6">
            <div className="grid gap-5 sm:grid-cols-2">
              <div className="space-y-2">
                <label htmlFor="password" className="text-sm font-medium">
                  Password
                </label>

                <input
                  id="password"
                  type="password"
                  placeholder="Enter password"
                  autoComplete="new-password"
                  {...register("password")}
                  className="h-10 w-full rounded-md border bg-background px-3 text-sm outline-none transition-colors placeholder:text-muted-foreground focus:border-ring focus:ring-2 focus:ring-ring/20"
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
                  className="text-sm font-medium"
                >
                  Confirm password
                </label>

                <input
                  id="confirmPassword"
                  type="password"
                  placeholder="Confirm password"
                  autoComplete="new-password"
                  {...register("confirmPassword")}
                  className="h-10 w-full rounded-md border bg-background px-3 text-sm outline-none transition-colors placeholder:text-muted-foreground focus:border-ring focus:ring-2 focus:ring-ring/20"
                />

                {errors.confirmPassword && (
                  <p className="text-sm text-destructive">
                    {errors.confirmPassword.message}
                  </p>
                )}
              </div>
            </div>
          </div>
        )}

        {isEditMode && (
          <div className="flex items-start gap-3 px-6 py-5">
            <input
              id="is-active"
              type="checkbox"
              {...register("isActive")}
              className="mt-0.5 size-4 rounded border-input accent-primary"
            />

            <div>
              <label
                htmlFor="is-active"
                className="block cursor-pointer text-sm font-medium"
              >
                Active
              </label>
            </div>
          </div>
        )}

        <div className="flex justify-end gap-3 g-muted/20 px-6 py-4">
          <button
            type="button"
            onClick={() => navigate("/users")}
            className="rounded-md border bg-card px-4 py-2 text-sm font-medium transition-colors hover:bg-muted"
          >
            Cancel
          </button>

          <button
            type="submit"
            disabled={isSubmitting}
            className="rounded-md bg-primary px-4 py-2 text-sm font-medium text-primary-foreground transition-colors hover:bg-primary-hover"
          >
            {isSubmitting
              ? isEditMode
                ? "Saving..."
                : "Creating..."
              : isEditMode
                ? "Save changes"
                : "Create user"}
          </button>
        </div>
      </form>
    </div>
  );
}

export default UserFormPage;

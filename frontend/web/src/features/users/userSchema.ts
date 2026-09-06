import { z } from "zod";

const baseUserSchema = {
  firstName: z.string().trim().min(1, "First name is required"),
  lastName: z.string().trim().min(1, "Last name is required"),
  email: z.email("Enter a valid email address"),
  departmentId: z.number().nullable(),
  password: z.string().optional(),
  confirmPassword: z.string().optional(),
  isActive: z.boolean(),
};

export const userCreateSchema = z
  .object(baseUserSchema)
  .superRefine((data, context) => {
    if (!data.password) {
      context.addIssue({
        code: "custom",
        path: ["password"],
        message: "Password is required",
      });
    }

    if (!data.confirmPassword) {
      context.addIssue({
        code: "custom",
        path: ["confirmPassword"],
        message: "Confirm password is required",
      });
    }

    if (
      data.password &&
      data.confirmPassword &&
      data.password !== data.confirmPassword
    ) {
      context.addIssue({
        code: "custom",
        path: ["confirmPassword"],
        message: "Passwords do not match",
      });
    }
  });

export const userUpdateSchema = z.object(baseUserSchema);

export type UserFormData = z.infer<typeof userUpdateSchema>;

import { apiClient } from "@/services/api/client";

export interface User {
  id: number;
  organizationId: number;
  departmentId: number | null;
  firstName: string;
  lastName: string;
  email: string;
  isActive: boolean;
}

export async function getUsers(organizationId: number): Promise<User[]> {
  const response = await apiClient.get<User[]>("/api/users", {
    params: {
      organizationId,
    },
  });

  return response.data;
}

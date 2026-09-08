import { apiClient } from "@/services/api/client";

export interface Role {
  id: number;
  organizationId: number | null;
  name: string;
  description: string | null;
  isActive: boolean;
}

export async function getRoles(organizationId: number): Promise<Role[]> {
  const response = await apiClient.get<Role[]>("/api/roles", {
    params: { organizationId },
  });

  return response.data;
}

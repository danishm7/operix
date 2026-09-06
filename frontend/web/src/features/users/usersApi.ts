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

export interface CreateUserRequest {
  organizationId: number;
  departmentId: number | null;
  firstName: string;
  lastName: string | null;
  email: string;
  password: string;
}

export interface UpdateUserRequest {
  departmentId: number | null;
  firstName: string;
  lastName: string | null;
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

export async function getUser(userId: number): Promise<User> {
  const response = await apiClient.get<User>(`/api/users/${userId}`);

  return response.data;
}

export async function createUser(request: CreateUserRequest): Promise<User> {
  const response = await apiClient.post<User>("/api/users", request);

  return response.data;
}

export async function updateUser(
  userId: number,
  request: UpdateUserRequest,
): Promise<User> {
  const response = await apiClient.put<User>(`/api/users/${userId}`, request);

  return response.data;
}

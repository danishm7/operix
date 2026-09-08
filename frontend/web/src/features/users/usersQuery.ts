import { useQuery } from "@tanstack/react-query";

import { getUser, getUserRoles, getUsers } from "@/features/users/usersApi";

export function useUsers(organizationId: number) {
  return useQuery({
    queryKey: ["users", organizationId],
    queryFn: () => getUsers(organizationId),
  });
}

export function useUser(userId: number) {
  return useQuery({
    queryKey: ["user", userId],
    queryFn: () => getUser(userId),
    enabled: userId > 0, // Only fetch for valid user id
  });
}

export function useUserRoles(userId: number) {
  return useQuery({
    queryKey: ["user-roles", userId],
    queryFn: () => getUserRoles(userId),
    enabled: userId > 0,
  });
}

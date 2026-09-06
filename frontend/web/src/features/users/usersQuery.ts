import { useQuery } from "@tanstack/react-query";

import { getUser, getUsers } from "@/features/users/usersApi";

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

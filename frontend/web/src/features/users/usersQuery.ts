import { useQuery } from "@tanstack/react-query";

import { getUsers } from "@/features/users/usersApi";

export function useUsers(organizationId: number) {
  return useQuery({
    queryKey: ["users", organizationId],
    queryFn: () => getUsers(organizationId),
  });
}

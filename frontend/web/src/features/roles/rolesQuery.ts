import { useQuery } from "@tanstack/react-query";

import { getRoles } from "@/features/roles/rolesApi";

export function useRoles(organizationId: number) {
  return useQuery({
    queryKey: ["roles", organizationId],
    queryFn: () => getRoles(organizationId),
    enabled: organizationId > 0,
  });
}

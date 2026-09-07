export interface AuthUser {
  id: number;
  firstName: string;
  lastName: string | null;
  email: string;
}

interface JwtPayload {
  sub: string;
  email: string;
  given_name: string;
  family_name?: string;
}

export function getUserFromToken(accessToken: string): AuthUser {
  const payload = accessToken.split(".")[1];

  const decodedPayload = JSON.parse(
    atob(payload.replace(/-/g, "+").replace(/_/g, "/")),
  ) as JwtPayload;

  return {
    id: Number(decodedPayload.sub),
    firstName: decodedPayload.given_name,
    lastName: decodedPayload.family_name ?? null,
    email: decodedPayload.email,
  };
}

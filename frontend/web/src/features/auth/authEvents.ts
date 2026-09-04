type AuthEvent = "unauthorized";

type AuthEventHandler = () => void;

const handlers = new Map<AuthEvent, Set<AuthEventHandler>>();

export function subscribeToAuthEvent(
  event: AuthEvent,
  handler: AuthEventHandler,
): () => void {
  if (!handlers.has(event)) {
    handlers.set(event, new Set());
  }

  handlers.get(event)!.add(handler);

  return () => {
    handlers.get(event)?.delete(handler);
  };
}

export function publishAuthEvent(event: AuthEvent): void {
  handlers.get(event)?.forEach((handler) => handler());
}

export type ToastType = "success" | "error" | "warning" | "info";

type ToastListener = (message: string, type: ToastType) => void;

const listeners = new Set<ToastListener>();

export function subscribeToToast(listener: ToastListener) {
  listeners.add(listener);

  return () => {
    listeners.delete(listener);
  };
}

export function showToast(message: string, type: ToastType = "success") {
  listeners.forEach((listener) => listener(message, type));
}

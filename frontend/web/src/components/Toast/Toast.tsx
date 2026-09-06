import { useEffect, useState } from "react";
import { subscribeToToast, type ToastType } from "./toastService";

interface ToastState {
  message: string;
  type: ToastType;
}

function Toast() {
  const [toast, setToast] = useState<ToastState | null>(null);

  useEffect(() => {
    return subscribeToToast((message, type) => {
      setToast({ message, type });
    });
  }, []);

  useEffect(() => {
    if (!toast) {
      return;
    }

    const timeout = setTimeout(() => {
      setToast(null);
    }, 3000);

    return () => clearTimeout(timeout);
  }, [toast]);

  if (!toast) {
    return null;
  }

  return (
    <div className="fixed bottom-6 right-6 z-50">
      <div
        className={[
          "rounded-md border px-4 py-3 text-sm shadow-lg",
          toast.type === "success" && "border-success bg-success text-white",
          toast.type === "error" &&
            "border-destructive bg-destructive text-white",
          toast.type === "warning" &&
            "border-warning bg-warning text-foreground",
          toast.type === "info" && "border-border bg-card text-foreground",
        ]
          .filter(Boolean)
          .join(" ")}
      >
        {toast.message}
      </div>
    </div>
  );
}

export default Toast;

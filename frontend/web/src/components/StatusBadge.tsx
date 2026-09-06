interface StatusBadgeProps {
  isActive: boolean;
}

function StatusBadge({ isActive }: StatusBadgeProps) {
  return (
    <span
      className={[
        "inline-flex items-center gap-1.5 rounded-full px-2.5 py-1 text-xs font-medium",
        isActive
          ? "bg-green-50 text-green-700"
          : "bg-muted text-muted-foreground",
      ].join(" ")}
    >
      <span
        className={[
          "size-1.5 rounded-full",
          isActive ? "bg-green-600" : "bg-muted-foreground",
        ].join(" ")}
      />

      {isActive ? "Active" : "Inactive"}
    </span>
  );
}

export default StatusBadge;

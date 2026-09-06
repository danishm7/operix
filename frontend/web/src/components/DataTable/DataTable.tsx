import type { ReactNode } from "react";

export interface DataTableColumn<T> {
  key: string;
  header: string;
  align?: "left" | "center" | "right";
  width?: string;
  render?: (row: T) => ReactNode;
}

interface DataTableProps<T> {
  data: T[];
  columns: DataTableColumn<T>[];
  isLoading?: boolean;
  emptyMessage?: string;
}

function DataTable<T>({
  data,
  columns,
  isLoading = false,
  emptyMessage = "No records found.",
}: DataTableProps<T>) {
  if (isLoading) {
    return (
      <div className="flex items-center justify-center rounded-lg border bg-card py-12 text-sm text-muted-foreground">
        Loading...
      </div>
    );
  }

  return (
    <div className="overflow-x-auto rounded-lg border bg-card">
      <table className="w-full text-sm">
        <thead>
          <tr className="border-b bg-muted/40">
            {columns.map((column) => (
              <th
                key={column.key}
                style={{ width: column.width }}
                className={[
                  "px-4 py-3 font-medium",
                  getAlignmentClass(column.align),
                ].join(" ")}
              >
                {column.header}
              </th>
            ))}
          </tr>
        </thead>

        <tbody className="divide-y">
          {data.length === 0 ? (
            <tr>
              <td
                colSpan={columns.length}
                className="px-4 py-12 text-center text-sm text-muted-foreground"
              >
                {emptyMessage}
              </td>
            </tr>
          ) : (
            data.map((row, rowIndex) => (
              <tr
                key={rowIndex}
                className="transition-colors hover:bg-muted/30"
              >
                {columns.map((column) => (
                  <td
                    key={column.key}
                    className={[
                      "px-4 py-3",
                      getAlignmentClass(column.align),
                    ].join(" ")}
                  >
                    {column.render
                      ? column.render(row)
                      : getValue(row, column.key)}
                  </td>
                ))}
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
}

function getAlignmentClass(align: DataTableColumn<unknown>["align"]): string {
  switch (align) {
    case "center":
      return "text-center";
    case "right":
      return "text-right";
    default:
      return "text-left";
  }
}

function getValue<T>(row: T, key: string): ReactNode {
  return (row as Record<string, unknown>)[key] as ReactNode;
}

export default DataTable;

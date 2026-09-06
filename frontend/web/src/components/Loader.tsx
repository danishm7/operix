interface LoaderProps {
  message?: string;
}

function Loader({ message = "Loading..." }: LoaderProps) {
  return (
    <div className="flex items-center justify-center rounded-lg border bg-card py-12 text-sm text-muted-foreground">
      {message}
    </div>
  );
}

export default Loader;

interface ErrorMessageProps {
  message?: string;
}

function ErrorMessage({
  message = "Something went wrong.",
}: ErrorMessageProps) {
  return (
    <div className="rounded-lg border border-destructive/20 bg-destructive/5 p-4 text-sm text-destructive">
      {message}
    </div>
  );
}

export default ErrorMessage;

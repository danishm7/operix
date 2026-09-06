interface OperixLogoProps {
  className?: string;
}

function OperixLogo({ className = "" }: OperixLogoProps) {
  return (
    <div
      className={`font-semibold tracking-tight text-foreground ${className}`}
    >
      <span className="text-primary">O</span>perix
    </div>
  );
}

export default OperixLogo;

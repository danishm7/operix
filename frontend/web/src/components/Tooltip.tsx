import { useEffect, useRef, useState, type ReactNode } from "react";
import { createPortal } from "react-dom";

interface TooltipProps {
  content: string;
  children: ReactNode;
}

function Tooltip({ content, children }: TooltipProps) {
  const [isVisible, setIsVisible] = useState(false);
  const [position, setPosition] = useState({
    top: 0,
    left: 0,
  });

  const triggerRef = useRef<HTMLDivElement>(null);

  const updatePosition = () => {
    if (!triggerRef.current) {
      return;
    }

    const rect = triggerRef.current.getBoundingClientRect();

    setPosition({
      top: rect.bottom + 8,
      left: rect.right,
    });
  };

  useEffect(() => {
    if (!isVisible) {
      return;
    }

    updatePosition();

    window.addEventListener("scroll", updatePosition, true);
    window.addEventListener("resize", updatePosition);

    return () => {
      window.removeEventListener("scroll", updatePosition, true);
      window.removeEventListener("resize", updatePosition);
    };
  }, [isVisible]);

  return (
    <div
      ref={triggerRef}
      className="relative inline-flex"
      onMouseEnter={() => setIsVisible(true)}
      onMouseLeave={() => setIsVisible(false)}
    >
      {children}

      {isVisible &&
        createPortal(
          <div
            role="tooltip"
            className="pointer-events-none fixed z-[9999] -translate-x-full whitespace-nowrap rounded-md bg-foreground px-2.5 py-1.5 text-xs font-medium text-background shadow-md"
            style={{
              top: position.top,
              left: position.left,
            }}
          >
            {content}
          </div>,
          document.body,
        )}
    </div>
  );
}

export default Tooltip;

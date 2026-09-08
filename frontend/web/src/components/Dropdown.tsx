import { Check, ChevronDown, Loader2, Search, X } from "lucide-react";
import { useEffect, useRef, useState } from "react";

export interface DropdownOption {
  value: string;
  label: string;
  disabled?: boolean;
}

interface DropdownBaseProps {
  data: DropdownOption[];
  placeholder?: string;
  searchPlaceholder?: string;
  emptyMessage?: string;
  label?: string;
  required?: boolean;
  disabled?: boolean;
  searchable?: boolean;
  clearable?: boolean;
  loading?: boolean;
  error?: string;
  className?: string;
}

interface SingleDropdownProps extends DropdownBaseProps {
  isMulti?: false;
  value?: DropdownOption | null;
  onChange?: (value: DropdownOption | null) => void;
}

interface MultiDropdownProps extends DropdownBaseProps {
  isMulti: true;
  value?: DropdownOption[];
  onChange?: (value: DropdownOption[]) => void;
}

type DropdownProps = SingleDropdownProps | MultiDropdownProps;

function Dropdown(props: DropdownProps) {
  const {
    data,
    placeholder = "Select...",
    searchPlaceholder = "Search...",
    emptyMessage = "No results found.",
    label,
    required,
    disabled = false,
    searchable = false,
    clearable = false,
    loading = false,
    error,
    className = "",
  } = props;

  const [isOpen, setIsOpen] = useState(false);
  const [search, setSearch] = useState("");

  const dropdownRef = useRef<HTMLDivElement>(null);

  const selectedOptions = props.isMulti
    ? (props.value ?? [])
    : props.value
      ? [props.value]
      : [];

  const selectedValues = selectedOptions.map((item) => item.value);

  const filteredOptions = data.filter((option) =>
    option.label.toLowerCase().includes(search.toLowerCase()),
  );

  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target as Node)
      ) {
        setIsOpen(false);
      }
    }

    document.addEventListener("mousedown", handleClickOutside);

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  useEffect(() => {
    if (!isOpen) {
      setSearch("");
    }
  }, [isOpen]);

  const handleSelect = (option: DropdownOption) => {
    if (option.disabled) {
      return;
    }

    if (props.isMulti) {
      const currentValue = props.value ?? [];

      const exists = currentValue.some((item) => item.value === option.value);

      if (exists) {
        props.onChange?.(
          currentValue.filter((item) => item.value !== option.value),
        );
      } else {
        props.onChange?.([...currentValue, option]);
      }

      return;
    }

    props.onChange?.(option);
    setIsOpen(false);
  };

  const handleClear = (event: React.MouseEvent) => {
    event.stopPropagation();

    if (props.isMulti) {
      props.onChange?.([]);
    } else {
      props.onChange?.(null);
    }
  };

  const renderValue = () => {
    if (selectedOptions.length === 0) {
      return <span className="text-muted-foreground">{placeholder}</span>;
    }

    if (props.isMulti) {
      return (
        <div className="flex min-w-0 flex-1 items-center gap-1.5 overflow-hidden">
          {selectedOptions.map((option) => (
            <span
              key={option.value}
              className="inline-flex shrink-0 items-center gap-1 rounded-md bg-muted px-2 py-0.5 text-xs font-medium text-foreground"
            >
              {option.label}

              <button
                type="button"
                className="rounded-sm text-muted-foreground hover:text-foreground"
                onClick={(event) => {
                  event.stopPropagation();

                  props.onChange?.(
                    selectedOptions.filter(
                      (item) => item.value !== option.value,
                    ),
                  );
                }}
              >
                <X className="size-3" />
              </button>
            </span>
          ))}
        </div>
      );
    }

    return (
      <span className="truncate text-foreground">
        {selectedOptions[0].label}
      </span>
    );
  };

  return (
    <div ref={dropdownRef} className={`relative space-y-2 ${className}`}>
      {label && (
        <label className="block text-sm font-medium text-foreground">
          {label}

          {required && <span className="ml-1 text-destructive">*</span>}
        </label>
      )}

      <button
        type="button"
        disabled={disabled || loading}
        onClick={() => setIsOpen((value) => !value)}
        className={[
          "flex min-h-10 w-full items-center justify-between gap-2 rounded-xl border bg-background px-3 py-2 text-sm",
          "transition-colors outline-none",
          "focus:border-ring focus:ring-2 focus:ring-ring/20",
          "disabled:cursor-not-allowed disabled:opacity-50",
          error ? "border-destructive" : "border-border",
        ].join(" ")}
      >
        {renderValue()}

        <div className="flex shrink-0 items-center gap-1">
          {loading && (
            <Loader2 className="size-4 animate-spin text-muted-foreground" />
          )}

          {!loading && clearable && selectedOptions.length > 0 && (
            <span
              role="button"
              tabIndex={0}
              className="rounded-sm p-0.5 text-muted-foreground hover:bg-muted hover:text-foreground"
              onClick={handleClear}
              onKeyDown={(event) => {
                if (event.key === "Enter" || event.key === " ") {
                  handleClear(event as unknown as React.MouseEvent);
                }
              }}
            >
              <X className="size-4" />
            </span>
          )}

          {!loading && (
            <ChevronDown
              className={[
                "size-4 text-muted-foreground transition-transform",
                isOpen ? "rotate-180" : "",
              ].join(" ")}
            />
          )}
        </div>
      </button>

      {isOpen && !disabled && (
        <div className="absolute z-50 mt-2 w-full overflow-hidden rounded-xl border border-border bg-popover shadow-lg">
          {searchable && (
            <div className="flex items-center gap-2 border-b border-border px-3">
              <Search className="size-4 shrink-0 text-muted-foreground" />

              <input
                type="text"
                value={search}
                onChange={(event) => setSearch(event.target.value)}
                placeholder={searchPlaceholder}
                autoFocus
                className="h-10 min-w-0 flex-1 bg-transparent text-sm outline-none placeholder:text-muted-foreground"
              />
            </div>
          )}

          <div className="max-h-60 overflow-y-auto p-1.5">
            {filteredOptions.length === 0 ? (
              <div className="px-3 py-6 text-center text-sm text-muted-foreground">
                {loading ? "Loading..." : emptyMessage}
              </div>
            ) : (
              filteredOptions.map((option) => {
                const isSelected = selectedValues.includes(option.value);

                return (
                  <button
                    key={option.value}
                    type="button"
                    disabled={option.disabled}
                    onClick={() => handleSelect(option)}
                    className={[
                      "flex w-full items-center gap-3 rounded-lg px-3 py-2 text-left text-sm",
                      "transition-colors",
                      option.disabled
                        ? "cursor-not-allowed opacity-50"
                        : "hover:bg-muted",
                    ].join(" ")}
                  >
                    {props.isMulti ? (
                      <span
                        className={[
                          "flex size-4 shrink-0 items-center justify-center rounded border",
                          isSelected
                            ? "border-primary bg-primary text-primary-foreground"
                            : "border-input",
                        ].join(" ")}
                      >
                        {isSelected && <Check className="size-3" />}
                      </span>
                    ) : (
                      isSelected && (
                        <Check className="size-4 shrink-0 text-primary" />
                      )
                    )}

                    <span className="min-w-0 flex-1 truncate">
                      {option.label}
                    </span>
                  </button>
                );
              })
            )}
          </div>
        </div>
      )}

      {error && <p className="text-sm text-destructive">{error}</p>}
    </div>
  );
}

export default Dropdown;

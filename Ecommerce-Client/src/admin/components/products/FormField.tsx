import type { FieldError } from "react-hook-form";

type FormFieldProps = {
  id: string;
  label: string;
  error?: FieldError;
  required?: boolean;
  type?: string;
  placeHolder?: string;
};

export default function FormField({
  id,
  label,
  error,
  required,
  type,
  placeHolder,
  ...props
}: FormFieldProps) {
  const errorId = `${id}-error`;
  return (
    <div className="flex flex-col gap-1">
      <label htmlFor={id} className="font-medium">
        {label}
      </label>
      <input
        id={id}
        aria-invalid={error ? "true" : "false"}
        aria-describedby={error ? errorId : undefined}
        aria-required={required}
        {...props}
        type={type}
        placeholder={placeHolder}
        className="input"
      />
      {error && (
        <span id={errorId} className="text-sm text-red-600" role="alert">
          {error.message}
        </span>
      )}
    </div>
  );
}

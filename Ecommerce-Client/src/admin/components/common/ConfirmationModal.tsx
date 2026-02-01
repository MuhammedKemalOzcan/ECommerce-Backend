import { useEffect, useRef } from "react";
import { X } from "lucide-react";
import { variantConfig } from "../../../utils/variant";

type ConfirmationModalVariant = "danger" | "warning" | "info";

type ConfirmationModalProps = {
  // Modal state
  isOpen: boolean;

  // Content
  title: string;
  message: string;

  // Actions
  onConfirm: () => void | Promise<void>;
  onCancel: () => void;

  // Customization
  confirmText?: string;
  cancelText?: string;
  variant?: ConfirmationModalVariant;

  // State
  isLoading?: boolean;

  // Advanced
  showCloseButton?: boolean;
  closeOnOutsideClick?: boolean;
};

export default function ConfirmationModal({
  isOpen,
  title,
  message,
  onConfirm,
  onCancel,
  confirmText = "Confirm",
  cancelText = "Cancel",
  variant = "warning",
  isLoading = false,
  showCloseButton = true,
  closeOnOutsideClick = true,
}: ConfirmationModalProps) {
  const modalRef = useRef<HTMLDivElement>(null);
  const confirmButtonRef = useRef<HTMLButtonElement>(null);

  const config = variantConfig[variant];
  const Icon = config.icon;

  // modal açıldığı gibi confirm butonuna odaklanır.
  useEffect(() => {
    if (isOpen && confirmButtonRef.current) {
      confirmButtonRef.current.focus();
    }
  }, [isOpen]);

  // Keyboard handlers
  useEffect(() => {
    if (!isOpen) return;

    const handleKeyDown = (e: KeyboardEvent) => {
      // ESC to close
      if (e.key === "Escape" && !isLoading) {
        onCancel();
      }

      // Enter to confirm (if not loading)
      if (e.key === "Enter" && !isLoading) {
        e.preventDefault();
        handleConfirm();
      }

      // Tab trap (basic implementation): Odağın modal dışına çıkmasını engeller.
      if (e.key === "Tab" && modalRef.current) {
        const focusableElements = modalRef.current.querySelectorAll(
          'button:not([disabled]), [tabindex]:not([tabindex="-1"])'
        );
        const firstElement = focusableElements[0] as HTMLElement;
        const lastElement = focusableElements[
          focusableElements.length - 1
        ] as HTMLElement;

        if (e.shiftKey && document.activeElement === firstElement) {
          e.preventDefault();
          lastElement.focus();
        } else if (!e.shiftKey && document.activeElement === lastElement) {
          e.preventDefault();
          firstElement.focus();
        }
      }
    };

    document.addEventListener("keydown", handleKeyDown);
    return () => document.removeEventListener("keydown", handleKeyDown);
  }, [isOpen, isLoading, onCancel]);

  // Body scroll lock
  useEffect(() => {
    if (isOpen) {
      document.body.style.overflow = "hidden";
    } else {
      document.body.style.overflow = "unset";
    }

    return () => {
      document.body.style.overflow = "unset";
    };
  }, [isOpen]);

  // Handlers
  const handleConfirm = async () => {
    try {
      await onConfirm();
    } catch (error) {
      console.error("Confirmation error:", error);
      // Error handling parent'ta yapılmalı
    }
  };

  const handleOutsideClick = (e: React.MouseEvent<HTMLDivElement>) => {
    if (closeOnOutsideClick && !isLoading && e.target === e.currentTarget) {
      onCancel();
    }
  };

  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50 p-4"
      onClick={handleOutsideClick}
      role="dialog"
      aria-modal="true"
      aria-labelledby="modal-title"
      aria-describedby="modal-description"
    >
      <div
        ref={modalRef}
        className="relative w-full max-w-md bg-white rounded-lg shadow-xl transform transition-all"
      >
        {/* Close Button (X) */}
        {showCloseButton && (
          <button
            type="button"
            onClick={onCancel}
            disabled={isLoading}
            className="absolute top-4 right-4 text-gray-400 hover:text-gray-600 transition disabled:opacity-50"
            aria-label="Kapat"
          >
            <X size={20} />
          </button>
        )}

        {/* Content */}
        <div className="p-6 text-center">
          {/* Icon */}
          <div
            className={`mx-auto w-16 h-16 ${config.iconBgClass} rounded-full flex items-center justify-center mb-4`}
          >
            <Icon className={config.iconColor} size={32} />
          </div>

          {/* Title */}
          <h2 id="modal-title" className="text-xl font-bold text-gray-900 mb-2">
            {title}
          </h2>

          {/* Message */}
          <p id="modal-description" className="text-sm text-gray-600 mb-6">
            {message}
          </p>

          {/* Actions */}
          <div className="flex flex-col gap-3">
            <button
              ref={confirmButtonRef}
              type="button"
              onClick={handleConfirm}
              disabled={isLoading}
              className={`
                w-full px-4 py-2.5 rounded-lg text-white font-medium
                transition focus:outline-none focus:ring-2 focus:ring-offset-2
                disabled:opacity-50 disabled:cursor-not-allowed
                ${config.confirmButtonClass}
              `}
              aria-busy={isLoading}
            >
              {isLoading ? "İşleniyor..." : confirmText}
            </button>

            <button
              type="button"
              onClick={onCancel}
              disabled={isLoading}
              className="w-full px-4 py-2.5 rounded-lg bg-gray-200 text-gray-900 font-medium hover:bg-gray-300 transition focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {cancelText}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

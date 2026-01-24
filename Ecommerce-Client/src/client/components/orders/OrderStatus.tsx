interface StatusProps {
  currentConfig: {
    text: string;
    bg: string;
    icon: React.JSX.Element;
  };
  status: string;
}

export default function OrderStatus({ currentConfig, status }: StatusProps) {
  return (
    <div className="flex items-center lg:items-start gap-2">
      <span
        className={`p-2 rounded-full ${currentConfig.text} ${currentConfig.bg}`}
      >
        {currentConfig.icon}
      </span>
      <h1 className={`${currentConfig.text}`}>{status}</h1>
    </div>
  );
}

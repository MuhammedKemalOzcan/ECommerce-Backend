interface StatusProps {
  currentConfig: {
    style: string;
    icon: React.JSX.Element;
  };
  status: string;
}

export default function OrderStatus({ currentConfig, status }: StatusProps) {
  return (
    <div className="flex items-center lg:items-start gap-2">
      <span className={`p-2 rounded-full ${currentConfig.style}`}>
        {currentConfig.icon}
      </span>
      <h1>{status.toUpperCase()}</h1>
    </div>
  );
}

type Props = {
  stock: number;
};

export default function StatusBadge({ stock }: Props) {
  const isAvailable = stock > 0;
  return (
    <span
      className={`${
        isAvailable ? "bg-green-200 text-green-900 " : "bg-red-900 text-red-200"
      } text-sm px-3 py-1.5 inline-flex items-center justiy-center rounded-full font-medium whitespace-nowrap `}
    >
      {isAvailable ? "Availabe" : "Out Of Stock"}
    </span>
  );
}

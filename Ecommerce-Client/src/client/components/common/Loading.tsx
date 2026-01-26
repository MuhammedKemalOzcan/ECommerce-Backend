import { PacmanLoader } from "react-spinners";

export default function Loading() {
  return (
    <div className="absolute inset-0 flex items-center justify-center bg-white/80 rounded-lg z-50">
      <PacmanLoader />
    </div>
  );
}

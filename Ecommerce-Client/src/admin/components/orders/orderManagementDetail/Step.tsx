interface StepProps {
    icon: React.JSX.Element;
    label: string;
    date: string
    active: boolean;
}

export default function Step({ icon, label, date, active }: StepProps) {
    return (
        <div className="flex flex-col items-center gap-2 relative z-10 bg-white px-2">
            <div
                className={`w-10 h-10 rounded-full flex items-center justify-center shadow-md transition-all ${active ? "bg-orange-400 text-white border-4 border-orange-50" : "bg-white text-gray-300 border border-gray-100"}`}
            >
                {icon}
            </div>
            <div className="text-center">
                <p
                    className={`text-xs font-bold ${active ? "text-slate-800" : "text-gray-400"}`}
                >
                    {label}
                </p>
                <p className="text-[10px] text-gray-400 mt-0.5">{date ? date : "--"}</p>
            </div>
        </div>
    );
}
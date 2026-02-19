import { Cancel } from "@mui/icons-material";
import { ORDER_STEPS } from "../../../../constants/orderSteps";
import { statusConfig } from "../../../../utils/statusConfig";
import Step from "./Step";
import { dateFormat } from "../../../../utils/format";

interface OrderProgressProps {
    status: string;
    shippedDate?: string;
    deliveredDate?: string;
    orderDate?: string;
}




export default function OrderProgress({ status, shippedDate, deliveredDate, orderDate }: OrderProgressProps) {
    const currentStatusKey = String(status);
    if (currentStatusKey === "4") {
        return (
            <div className="flex items-center justify-center gap-3 text-red-600 bg-red-50 p-4 rounded-lg">
                <Cancel />
                <span className="font-bold">This order has been canceled.</span>
            </div>
        );
    }

    const currentStepIndex = ORDER_STEPS.findIndex(
        (s) => s.id === currentStatusKey,
    );
    const progressWidth =
        currentStepIndex >= 0
            ? (currentStepIndex / (ORDER_STEPS.length - 1)) * 100
            : 0;


    const stepDates = {
        "1": orderDate,
        "2": shippedDate,
        "3": deliveredDate
    };

    return (
        <>
            <div className="flex justify-between items-center relative z-10">
                {ORDER_STEPS.map((step, index) => {
                    const isActive = index <= currentStepIndex;
                    const config = statusConfig[step.id];
                    const stepDate = stepDates[step.id as keyof typeof stepDates] || "--";

                    return (
                        <Step
                            icon={config.icon}
                            label={config.title}
                            date={dateFormat(stepDate)}
                            active={isActive}
                        />
                    );
                })}
            </div>

            {/* Progress Line */}
            <div className="absolute top-[52px] left-16 right-16 h-1 bg-gray-100 z-0">
                <div
                    className="h-full bg-orange-400 transition-all duration-700 ease-in-out"
                    style={{ width: `${progressWidth}%` }}
                ></div>
            </div>
        </>
    )
}
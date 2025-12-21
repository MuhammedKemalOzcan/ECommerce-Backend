import type { ReactNode } from "react";

type CartProps = {
  count: string;
  title: string;
  children: ReactNode;
};

export default function CheckoutCart({ count, title, children }: CartProps) {
  return (
    <div className="flex flex-col bg-white fill-[#FFFFFF] w-full p-10 gap-4 rounded-3xl shadow">
      <div className="flex gap-3 items-center">
        <h1 className="flex text-white border border-[#D87D4A] bg-[#D87D4A] rounded-full w-8 items-center justify-center ">
          {count}
        </h1>
        <h1>{title}</h1>
      </div>
      {children && <div className="mt-4">{children}</div>}
    </div>
  );
}

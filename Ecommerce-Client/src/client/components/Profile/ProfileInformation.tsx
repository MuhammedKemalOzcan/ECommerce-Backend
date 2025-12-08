import { useForm, type SubmitHandler } from "react-hook-form";
import type { AddCustomer } from "../../../types/customer";
import { useCustomerStore } from "../../../stores/customerStore";

export default function ProfileInformation() {
  const { register, handleSubmit } = useForm<AddCustomer>({
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
    },
  });

  const createCustomer = useCustomerStore((state) => state.createCustomer);

  const handleFormSubmit: SubmitHandler<AddCustomer> = async (data) => {
    console.log("Profile Information:", data);
    createCustomer(data);
  };

  return (
    <form
      onSubmit={handleSubmit(handleFormSubmit)}
      className="flex flex-col w-full p-12 bg-gray-200"
    >
      <h1 className="border-b h-8 border-black mb-8 ">Personal Information</h1>
      <div className="flex flex-col gap-4  p-8 rounded-lg w-[30%] h-[400px]">
        <div className="flex flex-col ">
          <label>Name</label>
          <input
            {...register("firstName", { required: true })}
            type="text"
            className="input"
          />
        </div>
        <div className="flex flex-col">
          <label>Surname</label>
          <input
            {...register("lastName", { required: true })}
            type="text"
            className="input"
          />
        </div>
        <div className="flex flex-col">
          <label>Email</label>
          <input
            {...register("email", { required: true })}
            type="text"
            className="input"
          />
        </div>
        <button type="submit" className="btn-1 p-3 mt-4 w-60">
          Update Information
        </button>
      </div>
    </form>
  );
}

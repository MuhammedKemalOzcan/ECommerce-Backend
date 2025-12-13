import { useForm, type SubmitHandler } from "react-hook-form";
import type { Customer } from "../../../types/customer";
import { useCustomerStore } from "../../../stores/customerStore";
import { useEffect } from "react";
import { useShallow } from "zustand/shallow";
import { toast } from "react-toastify";
import { BeatLoader } from "react-spinners";

export default function ProfileInformation() {
  const { customer, updateCustomer, loading, addAddress } = useCustomerStore(
    useShallow((s) => ({
      customer: s.customer,
      updateCustomer: s.updateCustomer,
      loading: s.loading,
      addAddress: s.addAddress,
    }))
  );

  const {
    register,
    handleSubmit,
    reset,
    formState: { isDirty },
  } = useForm<Customer>({
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
    },
  });

  useEffect(() => {
    if (customer) {
      reset({
        firstName: customer?.firstName,
        lastName: customer?.lastName,
        email: customer?.email,
        phoneNumber: customer?.phoneNumber,
      });
    }
  }, [customer, reset]);

  const handleFormSubmit: SubmitHandler<Customer> = async (data) => {
    await updateCustomer(data);
    toast.info("Bilgiler güncellendi");
    console.log("Data:", data);
  };

  if (loading)
    return (
      <div className="absolute inset-0 z-50 flex items-center justify-center rounded-lg bg-white/80 backdrop-blur-sm">
        <BeatLoader />
      </div>
    );

  return (
    <form
      onSubmit={handleSubmit(handleFormSubmit)}
      className="w-full min-h-screen bg-gradient-to-br from-gray-50 to-gray-100 p-8 md:p-12"
    >
      {/* Header */}
      <div className="mb-12">
        <h1 className="text-3xl md:text-4xl font-bold text-gray-900 pb-4 border-b-2 border-[#D87D4A]">
          Personal Information
        </h1>
        <p className="text-gray-600 text-sm mt-2">
          Update your profile details
        </p>
      </div>

      {/* Form Container */}
      <div className="w-full max-w-md bg-white p-8 md:p-10 rounded-2xl shadow-lg border border-gray-200">
        {/* Name Field */}
        <div className="flex flex-col gap-2 mb-6">
          <label className="text-sm font-semibold text-gray-700 uppercase tracking-wide">
            First Name
          </label>
          <input
            {...register("firstName", { required: true })}
            type="text"
            placeholder="Enter your first name"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-[#D87D4A] focus:border-transparent transition-all duration-200 bg-gray-50 text-gray-900"
          />
        </div>

        {/* Surname Field */}
        <div className="flex flex-col gap-2 mb-6">
          <label className="text-sm font-semibold text-gray-700 uppercase tracking-wide">
            Last Name
          </label>
          <input
            {...register("lastName", { required: true })}
            type="text"
            placeholder="Enter your last name"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-[#D87D4A] focus:border-transparent transition-all duration-200 bg-gray-50 text-gray-900"
          />
        </div>

        {/* Email Field */}
        <div className="flex flex-col gap-2 mb-8">
          <label className="text-sm font-semibold text-gray-700 uppercase tracking-wide">
            Email Address
          </label>
          <input
            {...register("email", { required: true })}
            type="email"
            placeholder="Enter your email address"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-[#D87D4A] focus:border-transparent transition-all duration-200 bg-gray-50 text-gray-900"
          />
        </div>

        <div className="flex flex-col gap-2 mb-8">
          <label className="text-sm font-semibold text-gray-700 uppercase tracking-wide">
            Email Address
          </label>
          <input
            {...register("phoneNumber", { required: true })}
            type="text"
            placeholder="Phone number"
            className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-[#D87D4A] focus:border-transparent transition-all duration-200 bg-gray-50 text-gray-900"
          />
        </div>

        {/* Submit Button */}
        <button
          disabled={!isDirty}
          type="submit"
          className={`w-full bg-[#D87D4A] text-white py-3 px-6 rounded-lg font-semibold hover:bg-[#cc6b35] transition-all duration-200 active:scale-95 shadow-md hover:shadow-lg uppercase tracking-wider disabled:cursor-not-allowed disabled:bg-orange-100`}
        >
          Update Information
        </button>
      </div>
    </form>
  );
}

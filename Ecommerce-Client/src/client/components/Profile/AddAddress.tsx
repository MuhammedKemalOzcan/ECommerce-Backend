import { MapPinned, Plus, Trash2Icon, X } from "lucide-react";
import { useState } from "react";
import FormField from "../../../admin/components/products/FormField";
import { useForm, type SubmitHandler } from "react-hook-form";
import type { AddAdress } from "../../../types/customer";
import { useCustomerStore } from "../../../stores/customerStore";
import { useShallow } from "zustand/shallow";
import ConfirmationModal from "../../../admin/components/common/ConfirmationModal";
import { useSearchParams } from "react-router-dom";

export default function AddAddress() {
  const [isAdding, setIsAdding] = useState<boolean>(false);
  const [searchParams, setSearchParams] = useSearchParams();
  // const [isDeleting, setIsDeleting] = useState<boolean>(false);
  // const [selectedAddress, setSelectedAddress] = useState<string>("");
  const addressId = searchParams.get("addressId");
  const isDeleteModalOpen = Boolean(addressId);

  const { AddAddress, customer } = useCustomerStore(
    useShallow((s) => ({
      AddAddress: s.AddAddress,
      customer: s.customer,
    }))
  );

  const handleClick = () => {
    setIsAdding(true);
  };

  const handleDelete = (id: string) => {
    setSearchParams({ addressId: id });
    console.log(id);
  };

  // useEffect(() => {
  //   if (customer?.addresses) {
  //     const defaultPrimary = customer.addresses.find((addr) => addr.isPrimary);
  //     if (defaultPrimary) {
  //       setSelectedAddress(defaultPrimary.id);
  //     }
  //   }
  // }, [customer]);

  // const handleChange = async (id: string | null) => {
  //   if (!id) return;
  //   setSelectedAddress(id);
  //   await updatePrimaryAddress(id);
  // };

  // const handleCancelDelete = () => {
  //   setSearchParams({});
  // };

  // const confirmDelete = async () => {
  //   if (addressId === null) return;
  //   try {
  //     setIsDeleting(true);
  //     await deleteAddress(addressId);
  //     setSearchParams({});
  //   } catch (error) {
  //     console.log(error);
  //   } finally {
  //     setIsDeleting(false);
  //   }
  // };

  const { register, handleSubmit } = useForm<AddAdress>({
    defaultValues: {
      title: "",
      location: {
        country: "",
        city: "",
        street: "",
        zipCode: "",
      },
      isPrimary: false,
    },
  });

  const handleFormSubmit: SubmitHandler<AddAdress> = async (data) => {
    await AddAddress(data);
  };

  return (
    <div className="w-full bg-white p-8 rounded-2xl shadow-lg border border-gray-200 mb-12">
      {!isAdding ? (
        <div className="flex flex-col items-center justify-center gap-6 py-8">
          <MapPinned size={48} className="text-[#D87D4A]" />
          <div className="text-center">
            <p className="text-gray-600 font-medium mb-4">
              Add a new delivery address
            </p>
            <button
              onClick={handleClick}
              className="flex items-center gap-2 bg-[#D87D4A] text-white px-6 py-3 rounded-lg hover:bg-[#cc6b35] transition-all duration-200 hover:shadow-lg active:scale-95 font-semibold mx-auto"
            >
              <Plus size={20} />
              <span>Add Address</span>
            </button>
          </div>
        </div>
      ) : (
        <form
          onSubmit={handleSubmit(handleFormSubmit)}
          className="flex flex-col gap-6 relative bg-gray-50 p-8 rounded-xl border border-gray-200"
        >
          <button
            type="button"
            className="absolute right-4 top-4 text-gray-400 hover:text-gray-600 transition-colors p-2"
            onClick={() => setIsAdding(false)}
          >
            <X size={24} />
          </button>

          <h3 className="text-lg font-bold text-gray-900 mb-2">New Address</h3>
          <FormField
            id="title"
            label="Title"
            type="text"
            placeHolder="Enter title"
            {...register("title", { required: true })}
          />
          <div className="flex gap-4">
            <div className="flex-1">
              <FormField
                id="street"
                label="Street"
                type="text"
                placeHolder="Enter street address"
                {...register("location.street", { required: true })}
              />
            </div>
            <div className="flex-1">
              <FormField
                id="city"
                label="City"
                type="text"
                placeHolder="Enter city"
                {...register("location.city", { required: true })}
              />
            </div>
          </div>

          <div className="flex gap-4">
            <div className="flex-1">
              <FormField
                id="country"
                label="Country"
                type="text"
                placeHolder="Enter country"
                {...register("location.country", { required: true })}
              />
            </div>
            <div className="flex-1">
              <FormField
                id="zipcode"
                label="Zip Code"
                type="text"
                placeHolder="Enter zip code"
                {...register("location.zipCode", { required: true })}
              />
            </div>
          </div>

          <div className="flex gap-3 pt-4">
            <button
              type="submit"
              className="flex-1 bg-[#D87D4A] text-white py-3 px-6 rounded-lg font-semibold hover:bg-[#cc6b35] transition-all duration-200 hover:shadow-lg active:scale-95 uppercase tracking-wider"
            >
              Save Address
            </button>
            <button
              type="button"
              onClick={() => setIsAdding(false)}
              className="flex-1 bg-gray-300 text-gray-700 py-3 px-6 rounded-lg font-semibold hover:bg-gray-400 transition-all duration-200 active:scale-95 uppercase tracking-wider"
            >
              Cancel
            </button>
          </div>
        </form>
      )}
      <div className="flex flex-col gap-2">
        {customer?.addresses?.map((address) => (
          <div
            key={address.id}
            className="flex items-start gap-20 border border-dashed w-auto h-auto p-4 relative"
          >
            <input
              type="radio"
              name="address"
              id={address.id}
              value={address.id}
              // checked={selectedAddress === address.id}
              // onChange={() => handleChange(address.id)}
            />
            <label htmlFor={address.id}>
              <h1>{address.title}</h1>
              <p>{address.location?.city}</p>
              <p>{address.location?.country}</p>
              <p>{address.location?.street}</p>
              <p>{address.location?.zipCode}</p>
            </label>
            <button
              onClick={() => handleDelete(address.id)}
              className="absolute right-10 top-10"
            >
              <Trash2Icon color="red" />
            </button>
          </div>
        ))}
        {/* <ConfirmationModal
          isOpen={isDeleteModalOpen}
          title="Delete Address"
          message="Bu işlem geri alınamaz. adres kalıcı olarak silinecek."
          // onConfirm={confirmDelete}
          // onCancel={handleCancelDelete}
          variant="danger"
        /> */}
      </div>
    </div>
  );
}

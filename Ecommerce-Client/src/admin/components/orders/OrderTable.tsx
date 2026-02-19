import OrderList from "./OrderList";
import PaginationControl from "./PaginationControl";
import OrderSearchBar from "./OrderSearchBar";
import { useOrderTableLogic } from "../../../hooks/useOrderTableLogic";

const OrderTable = () => {
  const {
    orders,
    pagination,
    isLoading,
    searchTerm,
    setSearchTerm,
    setCurrentPage,
    itemsPerPage,
  } = useOrderTableLogic();

  return (
    <div className="w-full p-4 bg-gray-50 min-h-screen">
      <div className="max-w-6xl mx-auto bg-white shadow-sm rounded-xl border border-gray-200 overflow-hidden">
        <OrderSearchBar setSearchTerm={setSearchTerm} searchTerm={searchTerm} />
        <OrderList orders={orders} isLoading={isLoading} />
        <PaginationControl
          pagination={pagination}
          setCurrentPage={setCurrentPage}
          itemsPerPage={itemsPerPage}
        />
      </div>
    </div>
  );
};

export default OrderTable;

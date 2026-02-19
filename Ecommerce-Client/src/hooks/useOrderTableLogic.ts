import { useEffect, useState } from "react";
import { useOrderStore } from "../stores/orderStore";
import { useShallow } from "zustand/shallow";

export const useOrderTableLogic = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [debouncedTerm, setDebouncedTerm] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;
  const { getAllOrders, orderInfo, isLoading } = useOrderStore(
    useShallow((state) => ({
      orderInfo: state.orderInfo,
      getAllOrders: state.getAllOrders,
      isLoading: state.isLoading,
    })),
  );

  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedTerm(searchTerm);
    }, 500);

    return () => clearTimeout(timer);
  }, [searchTerm]);

  useEffect(() => {
    setCurrentPage(1);
  }, [debouncedTerm]);

  useEffect(() => {
    getAllOrders(currentPage, itemsPerPage, debouncedTerm);
  }, [getAllOrders, currentPage, debouncedTerm]);

  return {
    orders: orderInfo,
    pagination: {
      currentPage,
      totalPages: orderInfo?.totalPages || 0,
      totalCount: orderInfo?.orderCount || 0,
      hasNext: orderInfo?.hasNextPage || false,
      hasPrev: orderInfo?.hasPreviousPage || false,
      startRange:
        orderInfo?.orderCount === 0 ? 0 : (currentPage - 1) * itemsPerPage + 1,
      endRange: Math.min(
        currentPage * itemsPerPage,
        orderInfo?.orderCount || 0,
      ),
    },
    isLoading,
    searchTerm,
    setSearchTerm,
    setCurrentPage,
    itemsPerPage,
  };
};

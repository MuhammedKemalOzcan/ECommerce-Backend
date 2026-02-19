interface PaginationProps {
  pagination: {
    currentPage: number;
    totalPages: number;
    totalCount: number;
    hasNext: boolean;
    hasPrev: boolean;
    startRange: number;
    endRange: number;
  };
  setCurrentPage: React.Dispatch<React.SetStateAction<number>>;
  itemsPerPage: number;
}

export default function PaginationControl({
  pagination,
  setCurrentPage,
}: PaginationProps) {
  return (
    <div className="p-4 border-t border-gray-100 flex items-center justify-between bg-gray-50">
      <p className="text-sm text-gray-500">
        Toplam <b>{pagination.totalCount}</b> siparişten{" "}
        <b>
          {pagination.startRange} - {pagination.endRange}
        </b>{" "}
        arası gösteriliyor
      </p>
      <div className="flex gap-2">
        <button
          disabled={!pagination.hasPrev}
          onClick={() => setCurrentPage(pagination.currentPage - 1)}
          className="px-4 py-2 border border-gray-300 rounded-md bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 disabled:opacity-50"
        >
          Prev
        </button>
        <div className="flex gap-1">
          {[...Array(pagination.totalPages)].map((_, i) => (
            <button
              key={i}
              onClick={() => setCurrentPage(i + 1)}
              className={`w-9 h-9 flex items-center justify-center rounded-md text-sm font-medium ${pagination.currentPage === i + 1 ? "bg-blue-600 text-white" : "bg-white border border-gray-300 text-gray-700 hover:bg-gray-50"}`}
            >
              {i + 1}
            </button>
          ))}
        </div>
        <button
          disabled={!pagination.hasNext}
          onClick={() => setCurrentPage(pagination.currentPage + 1)}
          className="px-4 py-2 border border-gray-300 rounded-md bg-white text-sm font-medium text-gray-700 hover:bg-gray-50"
        >
          Next
        </button>
      </div>
    </div>
  );
}

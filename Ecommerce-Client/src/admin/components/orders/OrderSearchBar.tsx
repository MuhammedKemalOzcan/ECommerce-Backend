interface Props {
  searchTerm: string;
  setSearchTerm: (value: string) => void;
}

export default function OrderSearchBar({ setSearchTerm }: Props) {
  return (
    <div className="p-5 border-b border-gray-100 flex justify-between items-center bg-white">
      <h2 className="text-lg font-bold text-gray-800">Tüm Siparişler</h2>
      <div className="relative">
        <input
          type="text"
          placeholder="Sipariş ara..."
          onChange={(e) => {
            setSearchTerm(e.target.value);
          }}
          className="pl-10 pr-4 py-2 border border-gray-300 rounded-lg text-sm focus:ring-2 focus:ring-blue-500 focus:outline-none"
        />
        <span className="absolute left-3 top-2.5 text-gray-400">🔍</span>
      </div>
    </div>
  );
}

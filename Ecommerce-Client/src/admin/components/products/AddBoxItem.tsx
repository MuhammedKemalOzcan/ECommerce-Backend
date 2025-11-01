import { useState } from "react";

export default function AddBoxItems() {
  let [isAdding, setIsAdding] = useState([{ name: "", quantity: 0 }]);

  const addItem = () => {
    setIsAdding((prev) => [...prev,])
  };

  return (
    <div className="flex flex-col my-6">
      <h1>Box Items</h1>
      <button type="button" onClick={addItem} className="btn-1 w-20px ">
        Add New Item
      </button>
      {isAdding && (
        <div>
          <input className="input" type="text" />
          <input className="input" type="number" />
        </div>
      )}
    </div>
  );
}

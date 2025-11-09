import { useEffect, useMemo, useState } from "react";
import type { UploadStatus } from "../../../types/Gallery";
import { productGalleryApi } from "../../../api/productGalleryApi";
import { useSearchParams } from "react-router-dom";
import { useProductStore } from "../../../stores/productStore";
export default function AddProductImage() {

  const [files, setFiles] = useState<File[]>([]);
  const [status, setStatus] = useState<UploadStatus>("initial");
  const [primaryIndex, setPrimaryIndex] = useState<number | null>(null);
  const [searchParams, setSearchParams] = useSearchParams();
  
  const refreshById = useProductStore((s) => s.refreshById);

  const productId = searchParams.get("productId");

  const handleSubmit = async () => {
    if (files) {
      try {
        setStatus("uploading");

        const formData = new FormData();
        [...files].forEach((file) => {
          formData.append("files", file);
        });
        if (primaryIndex !== null)
          formData.append("primaryIndex", String(primaryIndex));

        await productGalleryApi.add(productId, formData);
        await refreshById(productId);

        setSearchParams({});
        setStatus("success");
        setFiles([]);
        setPrimaryIndex(null);
      } catch (error) {
        setStatus("fail");
      }
    }
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files) return;
    setStatus("initial");
    const list = e.target.files;
    setFiles(list ? Array.from(list) : []);
  };

  //Eklenen resmi görüntüleme
  const previewUrls = useMemo(() => {
    if (files?.length === 0) return [];
    return files.map((file) => URL.createObjectURL(file));
  }, [files]);

  //Bellekte memory leak olmaması için oluşturulan url'leri unmount anında revoke ediyor.
  useEffect(() => {
    return () => {
      previewUrls?.map((u) => URL.revokeObjectURL(u));
    };
  }, []);

  return (
    <div className="p-12 flex flex-col justify-center items-center p-12 gap-3 w-full max-h-[50%] bg-gray-100 border-2 border-dashed rounded-lg hover:bg-gray-400 hover:text-white">
      <input
        onChange={handleFileChange}
        type="file"
        id="file"
        name="file"
        multiple
      />
      <p className="font-bold text-red-500">
        Please select which image will be the primary image:
      </p>
      {previewUrls.length > 0 && (
        <div className="w-full grid grid-cols-2 ">
          {previewUrls.map((src, i) => (
            <button
              onClick={() => setPrimaryIndex(i)}
              className="focus:border-2 focus:border-blue-500"
            >
              <img key={src} src={src} alt={`preview-${i + 1}`} />
            </button>
          ))}
        </div>
      )}

      {files &&
        [...files].map((file) => (
          <div
            className="flex justify-around items-center text-center text-black w-full"
            key={file.name}
          >
            <ul className="truncate w-auto ">
              <li>Name: {file.name}</li>
              <li>Type: {file.type}</li>
              <li>Size: {file.size} bytes</li>
            </ul>
          </div>
        ))}

      <button
        className="bg-black p-3 rounded-lg text-white hover:bg-white hover:text-black"
        onClick={handleSubmit}
      >
        Confirm
      </button>

      <Result status={status} />
    </div>
  );
}

const Result = ({ status }: { status: string }) => {
  if (status === "success") {
    return <p>✅ Files uploaded successfully!</p>;
  } else if (status === "fail") {
    return <p>❌ Files upload failed!</p>;
  } else if (status === "uploading") {
    return <p>⏳ Uploading selected files...</p>;
  } else {
    return null;
  }
};

import { useEffect, useMemo, useState } from "react";
import type { UploadStatus } from "../../../types/Gallery";
import { X } from "lucide-react";
import { useProductStore } from "../../../stores/productStore";

interface ImageProps {
  productId: string | null;
}

export default function AddProductImage({ productId }: ImageProps) {
  const [files, setFiles] = useState<File[]>([]);
  const [status, setStatus] = useState<UploadStatus>("initial");
  const [primaryIndex, setPrimaryIndex] = useState<number | null>(null);
  let [previewUrls, setPreviewUrls] = useState<string[]>([]);

  const uploadImage = useProductStore((state) => state.uploadImage);

  const handleSubmit = async () => {
    if (files) {
      try {
        setStatus("uploading");
        const formData = new FormData();
        [...files].forEach((file, index) => {
          formData.append(`Files[${index}].File`, file);
          if (index === primaryIndex)
            formData.append(`Files[${index}].IsPrimary`, "true");
        });
        await uploadImage(productId, formData);
        setStatus("success");
        setFiles([]);
        // setPrimaryIndex(null);
      } catch (error) {
        setStatus("fail");
      }
    }
  };

  const handleCancel = (index: number) => {
    URL.revokeObjectURL(previewUrls[index]);
    setPreviewUrls((prev) => prev.filter((_, i) => i !== index));
    setFiles((prev) => prev.filter((_, i) => i !== index));
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files) return;
    setStatus("initial");
    const list = e.target.files;
    setFiles(list ? Array.from(list) : []);
  };

  //Eklenen resmi görüntüleme
  previewUrls = useMemo(() => {
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
    <div className="flex flex-col justify-center items-center p-12 gap-3 w-full max-h-[50%] bg-gray-100 border-2 border-dashed rounded-lg hover:bg-gray-400 hover:text-white">
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
              key={i}
              onClick={() => setPrimaryIndex(i)}
              className="focus:border-2 focus:border-blue-500 relative"
            >
              <img key={src} src={src} alt={`preview-${i + 1}`} />
              <X
                onClick={() => handleCancel(i)}
                className="absolute top-0 right-0"
                color="red"
              />
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

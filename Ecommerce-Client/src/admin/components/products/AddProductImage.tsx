type Props = {
  files: File[];
  onFileChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  previewUrls: string[];
};

export default function AddProductImage({
  files,
  onFileChange,
  previewUrls,
}: Props) {
  return (
    <div className="flex flex-col justify-center items-center p-12 gap-3 w-[30%] bg-gray-100 border-2 rounded-lg border-dashed hover:bg-gray-400 hover:text-white">
      <input
        onChange={onFileChange}
        type="file"
        id="file"
        name="file"
        multiple
      />

      {previewUrls.length > 0 && (
        <div className="w-full grid grid-cols-2 gap-4">
          {previewUrls.map((src, i) => (
            <img key={src} src={src} alt={`preview-${i + 1}`} />
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

      {/* <Result status={status} /> */}
    </div>
  );
}

// const Result = ({ status }: { status: string }) => {
//   if (status === "success") {
//     return <p>✅ Files uploaded successfully!</p>;
//   } else if (status === "fail") {
//     return <p>❌ Files upload failed!</p>;
//   } else if (status === "uploading") {
//     return <p>⏳ Uploading selected files...</p>;
//   } else {
//     return null;
//   }
// };

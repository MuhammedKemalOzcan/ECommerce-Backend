export interface ImageGalleries {
  fileName: string;
  path: string;
  isPrimary: boolean;
}

export type UploadStatus = "initial" | "uploading" | "success" | "fail";

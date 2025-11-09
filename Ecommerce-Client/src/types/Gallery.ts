export interface ImageGalleries {
  id:string
  fileName: string;
  path: string;
  isPrimary: boolean;
}

export type UploadStatus = "initial" | "uploading" | "success" | "fail";

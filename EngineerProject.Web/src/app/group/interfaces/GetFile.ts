export interface GetFile {
    id: number;
    owner: string;
    isOwner: boolean;
    dateAdded: Date;
    fileName: string;
    size: number;
}
import { GetFile } from './GetFile';

export interface NewFile extends GetFile {
    uploaded: boolean;
    errorMessage: string;
}

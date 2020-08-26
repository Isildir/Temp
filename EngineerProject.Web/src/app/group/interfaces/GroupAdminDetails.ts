import { Candidate } from './Candidate';

export interface GroupAdminDetails {
    name: string;
    description: string;
    isPrivate: boolean;
    candidates: Candidate[];
}
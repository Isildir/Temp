import { Comment } from './Comment';

export interface Post extends Comment {
    pinned: boolean;
    title: string;
    editDate: Date;
    comments: Comment[];
}

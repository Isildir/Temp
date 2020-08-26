import { Comment } from './Comment';

export interface Post extends Comment {
    pinned: boolean;
    editDate: Date;
    comments: Comment[];
}

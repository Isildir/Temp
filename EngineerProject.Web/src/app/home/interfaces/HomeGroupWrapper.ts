import { GroupTile } from 'src/app/home/interfaces/GroupTile';

export interface HomeGroupWrapper {
    participant: GroupTile[];
    invited: GroupTile[];
    waiting: GroupTile[];
}

import { GroupTile } from 'src/app/models/GroupTile';

export interface HomeGroupWrapper {
    participant: GroupTile[];
    invited: GroupTile[];
    waiting: GroupTile[];
}

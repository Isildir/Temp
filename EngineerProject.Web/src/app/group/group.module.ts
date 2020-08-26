import { GroupRoutingModule } from './group-routing.module';
import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { GroupPageComponent as GroupPageComponent } from './pages/group-page/group-page.component';
import { PostTileComponent } from './components/post-tile/post-tile.component';
import { GroupDetailsDialogComponent } from './components/group-details-dialog/group-details-dialog.component';
import { FilesManagerComponent } from './components/files-manager/files-manager.component';
import { ChatComponent } from './components/chat/chat.component';

@NgModule({
   declarations: [
      GroupPageComponent,
      PostTileComponent,
      GroupDetailsDialogComponent,
      FilesManagerComponent,
      ChatComponent
   ],
   imports: [
      GroupRoutingModule,
      SharedModule
   ], entryComponents: [GroupDetailsDialogComponent]
})
export class GroupModule { }
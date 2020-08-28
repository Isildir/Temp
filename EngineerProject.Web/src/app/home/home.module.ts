import { SharedModule } from '../shared/shared.module';
import { NgModule } from '@angular/core';
import { HomeRoutingModule } from './home-routing.module';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { GroupTileComponent } from './components/group-tile/group-tile.component';
import { GroupCreateDialogComponent } from './components/group-create-dialog/group-create-dialog.component';

@NgModule({
   declarations: [
      HomePageComponent,
      GroupTileComponent,
      GroupCreateDialogComponent
   ],
   imports: [
      HomeRoutingModule,
      SharedModule
   ], entryComponents: [
      GroupCreateDialogComponent
   ]
})
export class HomeModule { }
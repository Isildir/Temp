import { RequestInterceptor } from './core/interceptors/request-interceptor.service';
import { SharedModule } from './shared/shared.module';
import { ErrorInterceptor } from './core/interceptors/error-interceptor.service';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorComponent } from './core/pages/error-page/error-page.component';

@NgModule({
   declarations: [
      AppComponent,
      ErrorComponent
   ],
   imports: [
      SharedModule,
      AppRoutingModule
   ],
   exports: [
      SharedModule
   ],
   providers: [
      { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

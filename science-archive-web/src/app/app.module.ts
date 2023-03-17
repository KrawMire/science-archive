import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './pages/auth/auth.component';
import { MainComponent } from './pages/main/main.component';
import { SignInFormComponent } from './components/auth/sign-in-form/sign-in-form.component';
import { SignUpFormComponent } from './components/auth/sign-up-form/sign-up-form.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthRequestInterceptor } from './helpers/auth-request.interceptor';

@NgModule({
  declarations: [
    AppComponent,

    AuthComponent,
    SignInFormComponent,
    SignUpFormComponent,

    MainComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthRequestInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

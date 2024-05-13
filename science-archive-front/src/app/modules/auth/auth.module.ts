import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInPageComponent } from './pages/sign-in-page/sign-in-page.component';
import { NzFormModule } from "ng-zorro-antd/form";
import { NzInputModule } from "ng-zorro-antd/input";
import { ReactiveFormsModule } from "@angular/forms";
import { NzCheckboxModule } from "ng-zorro-antd/checkbox";
import { NzButtonModule } from "ng-zorro-antd/button";
import { SignUpPageComponent } from './pages/sign-up-page/sign-up-page.component';
import { SignUpFormComponent } from './components/sign-up-form/sign-up-form.component';
import { SignInFormComponent } from './components/sign-in-form/sign-in-form.component';
import { RouterLink } from "@angular/router";
import { NzSpaceModule } from "ng-zorro-antd/space";
import { CodeConfirmFormComponent } from './components/code-confirm-form/code-confirm-form.component';

@NgModule({
  declarations: [SignInPageComponent, SignUpPageComponent, SignUpFormComponent, SignInFormComponent, CodeConfirmFormComponent],
  imports: [CommonModule, NzFormModule, NzInputModule, ReactiveFormsModule, NzCheckboxModule, NzButtonModule, RouterLink, NzSpaceModule]
})
export class AuthModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './pages/auth/auth.component';
import { MainComponent } from './pages/main/main.component';

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "auth" },
  { path: "auth", component: AuthComponent },
  { path: "main", component: MainComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

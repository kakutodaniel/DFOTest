import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEditUserComponent } from './add-edit-user/add-edit-user.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: 'users', component: UserComponent },
  { path: 'user', component: AddEditUserComponent },
  { path: 'user/:id', component: AddEditUserComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

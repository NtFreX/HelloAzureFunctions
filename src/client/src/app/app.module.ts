import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { UserManagementComponent } from './user/management/user-management.component';
import { AddUserComponent } from './user/add/add-user.component';
import { UserListComponent } from './user/list/user-list.component';

@NgModule({
  declarations: [
    AppComponent,
    UserManagementComponent,
    UserListComponent,
    AddUserComponent
  ],
  imports: [
    NgbModule,
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

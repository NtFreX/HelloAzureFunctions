import { Component, ViewChild } from '@angular/core';
import { UserListComponent } from '../list/user-list.component';

@Component({
    selector: 'app-user-management',
    templateUrl: './user-management.component.html'
})
export class UserManagementComponent {

    @ViewChild(UserListComponent, { static: false }) list: UserListComponent;

    onUserAdded() {
        this.list.load();
    }
}

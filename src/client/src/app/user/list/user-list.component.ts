import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Component } from '@angular/core';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html'
})
export class UserListComponent {
    users: any[];
    isLoading = false;

    constructor(private client: HttpClient) {
        this.load();
    }

    load(): void {
        this.isLoading = true;
        const self = this;
        this.client
            .get(environment.apiBasePath + 'users')
            .subscribe((data: any[]) => {
                self.users = data;
                self.isLoading = false;
            });
    }

    deleteUser(user: any): void {
        this.isLoading = true;
        const self = this;
        this.client
            .delete(environment.apiBasePath + `user/${user.id}`)
            .subscribe(() => self.load());
    }
}

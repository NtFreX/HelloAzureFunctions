import { Output, Component, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-add-user',
    templateUrl: './add-user.component.html',
    styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent {
    @Output()
    userAdded = new EventEmitter();

    isLoading = false;
    model = {
        name: ''
    };

    constructor(private client: HttpClient) { }

    addUser(): void {
        this.isLoading = true;

        const body = JSON.stringify(this.model);
        this.model.name = '';

        const self = this;
        this.client
            .post(environment.apiBasePath + 'user', body)
            .subscribe(() => {
                self.userAdded.next();
                self.isLoading = false;
            });
    }

}
import { Component } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  username: '';
  users: any[];
  isLoading = false;
  errorMessage: string = null;

  private baseUri = 'https://ntfrex-function-helloworld.azurewebsites.net/api/';

  constructor(private client: HttpClient) {
    this.load();
  }

  private handleError() {
      const self = this;
      return catchError((error: HttpErrorResponse) => {
        self.errorMessage = error.message;
        self.isLoading = false;
        self.users = null;
        throw error;
      });
  }

  private load(): void {
    this.isLoading = true;
    const self = this;
    this.client
    .get(this.baseUri + 'users')
    .pipe(this.handleError())
    .subscribe((data: any[]) => {
        self.users = data;
        self.isLoading = false;
    });
  }

  deleteUser(user: any): void {
    this.isLoading = true;
    const self = this;
    this.client
        .delete(this.baseUri + `user/${user.id}`)
        .pipe(this.handleError())
        .subscribe(() => self.load());
  }

  addUser(): void {
    this.isLoading = true;
    const body = JSON.stringify({ name: this.username });
    this.username = '';
    const self = this;
    this.client
      .post(this.baseUri + 'user', body)
      .pipe(this.handleError())
      .subscribe(() => self.load());
  }
}

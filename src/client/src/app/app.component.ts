import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';
  username: '';
  users: any;
  isLoading = false;
  hasError = false;

  constructor(private client: HttpClient) {
    this.load();
  }

  private handleError() {
      const self = this;
      return catchError((error) => {
        self.hasError = true;
        throw error;
      });
  }

  private load(): void {
    this.isLoading = true;
    const self = this;
    this.client
    .get('https://ntfrex-function-helloworld.azurewebsites.net/api/users')
    .pipe(this.handleError())
    .subscribe((data) => {
        self.users = data;
        self.isLoading = false;
    });
  }

  addUser(): void {
    this.isLoading = true;
    const body = JSON.stringify({ name: this.username });
    const self = this;
    this.client
      .post('https://ntfrex-function-helloworld.azurewebsites.net/api/user', body)
      .pipe(this.handleError())
      .subscribe(() => self.load());
  }
}

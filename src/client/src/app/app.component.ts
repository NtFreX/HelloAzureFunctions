import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';
  users: any;

  constructor(client: HttpClient) {
      client
        .get('https://ntfrex-function-helloworld.azurewebsites.net/api/users')
        .subscribe((data) => {
            this.users = data;
        });
  }
}

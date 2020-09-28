import { Component, OnInit } from '@angular/core';

import { NgxSpinnerService } from 'ngx-spinner';

import { ApiService } from '../services/api.service';
import { User } from '../models/user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  users: User[];
  cloneUsers: User[];
  message = null;
  alertType = "info";

  constructor(
    private userService: ApiService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.spinner.show();
    this.getUsers();
  }

  getUsers() {
    this.userService
      .getUsers()
      .subscribe(
        (users: any) => {
          this.users = users.items;
          this.cloneUsers = users.items;
        },
        (err) => {
          if (err.statusCode === 0) {
            this.alertType = "danger"
          } else {
            this.alertType = "warning"
          }
          this.message = err.message;
          setTimeout(() => {
            this.message = null;
          }, 4000);
          console.log(err);
        }
      )
      .add(() => {
        this.spinner.hide();
      });
  }

  applyFilter(vl: string) {
    let filterValue = vl.toLowerCase();
    this.users = this.cloneUsers;
    this.users = this.users.filter(
      (x) => x.name.includes(filterValue) || x.address.includes(filterValue)
    );
  }
}

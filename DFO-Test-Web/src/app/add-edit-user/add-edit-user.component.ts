import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';

import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-add-edit-user',
  templateUrl: './add-edit-user.component.html',
  styleUrls: ['./add-edit-user.component.css'],
})
export class AddEditUserComponent implements OnInit {
  id: string;
  form: FormGroup;
  message = null;
  alertType = 'info';
  saving = false;

  constructor(
    private route: ActivatedRoute,
    private userService: ApiService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private formBuilder: FormBuilder
  ) {
    this.id = this.route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: [null, Validators.required],
      age: [null, Validators.required],
      address: [null, Validators.required],
      hashId: [null],
    });

    if (this.id !== undefined) {
      this.spinner.show();
      this.getUserById();
    }
  }

  getUserById() {
    this.userService
      .getUserById(this.id)
      .subscribe(
        (users: any) => {
          this.form.patchValue({
            name: users.items[0].name,
            age: users.items[0].age,
            address: users.items[0].address,
            hashId: users.items[0].hashId,
          });
        },
        (err) => {
          if (err.statusCode === 0) {
            this.alertType = 'danger';
          } else {
            this.alertType = 'warning';
          }
          this.message = err.message;
          setTimeout(() => {
            this.message = null;
          }, 4000);
        }
      )
      .add(() => {
        this.spinner.hide();
      });
  }

  save() {
    this.spinner.show();

    if (this.id !== undefined) {
      this.userService
        .updateUser(this.form.value)
        .subscribe(
          () => {
            this.alertType = 'info';
            this.message = 'User updated successfully!';
            this.saving = true;
            setTimeout(() => {
              this.router.navigate(['/users']);
            }, 3000);
          },
          (err) => {
            if (err.statusCode === 0) {
              this.alertType = 'danger';
            } else {
              this.alertType = 'warning';
            }
            this.message = err.message;
            setTimeout(() => {
              this.message = null;
            }, 4000);
          }
        )
        .add(() => {
          this.spinner.hide();
        });
    } else {
      this.userService
        .saveUser(this.form.value)
        .subscribe(
          () => {
            this.alertType = 'info';
            this.message = 'User created successfully!';
            this.saving = true;
            setTimeout(() => {
              this.router.navigate(['/users']);
            }, 3000);
          },
          (err) => {
            if (err.statusCode === 0) {
              this.alertType = 'danger';
            } else {
              this.alertType = 'warning';
            }
            this.message = err.message;
            setTimeout(() => {
              this.message = null;
            }, 4000);
          }
        )
        .add(() => {
          this.spinner.hide();
        });
    }
  }
}

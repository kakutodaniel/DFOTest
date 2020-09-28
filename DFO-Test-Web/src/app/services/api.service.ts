import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { User } from '../models/user';
import { Resp } from '../models/resp';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private httpClient: HttpClient) {}

  url = API_URL + '/api/user';

  // Headers
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  // get all users
  getUsers(): Observable<User[]> {
    return this.httpClient
      .get<User[]>(this.url)
      .pipe(retry(2), catchError(this.handleError));
  }

  // get by hashId
  getUserById(hashId: string): Observable<User> {
    return this.httpClient
      .get<User>(this.url + '/' + hashId)
      .pipe(retry(2), catchError(this.handleError));
  }

  // save new user
  saveUser(user: User): Observable<User> {
    return this.httpClient
      .post<User>(this.url, JSON.stringify(user), this.httpOptions)
      .pipe(retry(2), catchError(this.handleError));
  }

  // update a user
  updateUser(user: User): Observable<User> {
    return this.httpClient
      .put<User>(
        this.url + '/' + user.hashId,
        JSON.stringify(user),
        this.httpOptions
      )
      .pipe(retry(2), catchError(this.handleError));
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = '';

    if (error.status === 0 || error.status === 500) {
      errorMessage =
        `Un unexcepted error has ocurred. Error code: ${error.status}, ` +
        `Message: ${error.message}`;
    } else if (error.status === 404) {
      errorMessage = 'User Not found';
    } else {
      errorMessage = 'Bad Request';
      if (error.error.errors !== null) {
        error.error.errors.forEach(function (value: string) {
          errorMessage += '</br>' + value;
        });
      }
    }

    let resp: Resp = { statusCode: error.status, message: errorMessage };

    return throwError(resp);
  }
}

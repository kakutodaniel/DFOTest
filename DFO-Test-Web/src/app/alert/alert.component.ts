import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css'],
})
export class AlertComponent implements OnInit {
  @Input() show;
  @Output() notify = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  myEvent = { name: 'Daniel', lastName: 'Kakuto' };
}

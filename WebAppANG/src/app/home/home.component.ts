import { Component, OnInit } from '@angular/core';
import { homeCardLinks } from '../links';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  myLinks = homeCardLinks;

  constructor() { }

  ngOnInit(): void {
  }

}

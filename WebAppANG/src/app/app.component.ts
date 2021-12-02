import { Component } from '@angular/core';
import { navLinks } from './links';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WebAppANG';
  myLinks = navLinks;

}

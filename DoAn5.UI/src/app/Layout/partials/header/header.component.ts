import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

 
  constructor() { }

  ngOnInit() {

   
    this.activeNav();
   
  }

  activeNav() {
    debugger
    const current =  window.location.pathname;
    const links = document.querySelectorAll("li a.nav-link");
    // let hrefs: any[] = [];
    links.forEach(link => {
      const href = link.getAttribute("href");
      if (href == current) {
        link.classList.add("active");
      }
      else {
        link.classList.remove("active");
      }
    });
  }
  
 


}
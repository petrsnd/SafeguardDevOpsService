import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { DevOpsServiceClient } from '../service-client.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  applianceAddress: string;

  constructor(
    private devOpsServiceClient: DevOpsServiceClient,
    private window: Window,
    private authService: AuthService) { }

  ngOnInit(): void {

    this.devOpsServiceClient.getSafeguard().subscribe(
      (data) => {
        if (data?.ApplianceAddress) {
          this.applianceAddress = data.ApplianceAddress;
          this.connect();
        }
      });
  }

  connect(): void {
    // Save this to storage since we are reloading
    this.window.sessionStorage.setItem('ApplianceAddress', this.applianceAddress);

    this.authService.login(this.applianceAddress);
  }

  handleKeyUp(e): void {
    if (e.keyCode === 13) { // Enter key
       this.connect();
    }
  }
}

import { Component } from '@angular/core';
import { AuthService } from '../../AuthService';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToasterService } from '../toaster-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  signupForm: FormGroup;
  showLogin:boolean=true;
 

  
  loginForm: FormGroup;

  constructor(
    private toasterService: ToasterService,
    private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.signupForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      mobileNo: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]]
    });
  
  }

  onLogin() {
    if (this.loginForm.valid) {
      const loginData = this.loginForm.value;
      this.authService.login(loginData).subscribe(
        response => {
          console.log('Login successful', response);
          // Add logic for successful login (e.g., storing token, redirecting)
        },
        error => {
          console.error('Login failed', error.error);
          // Add logic for failed login (e.g., showing error message)
        }
      );
    }
  }




  onSignup() {
    if (this.signupForm.valid) {
      const signupData = this.signupForm.value;
      this.authService.signup(signupData).subscribe(
        response => {
          console.log('Signup successful', response);
          // Clear the signup form
          this.signupForm.reset();
          
          // Toggle back to login form
          this.showLogin = true;
          // Show success toast message
          this.toasterService.showToast('Successful Register. Please login.', 'success');
          // Redirect to login page
          // this.router.navigate(['/login']);
        },
        error => {
          console.error('Signup failed', error);
          // Show error toast message
          this.toasterService.showToast( 'Login failed: ' + error.error, 'error');
        }
      );
    } else {
      // Show error toast message for invalid form
      this.toasterService.showToast('Please fill in all required fields correctly.', 'error');
    }
  }
  login() {
    if (this.loginForm.valid) {
      const loginData = {
        username: this.loginForm.get('username')?.value.trim() || '',
        password: this.loginForm.get('password')?.value || ''
      };

      this.authService.login(loginData).subscribe(
        response => {
          console.log('Login successful', response);
          if (response && response.token) {
            // Store the token in session storage
            sessionStorage.setItem('authToken', response.token);
            // Set isLoggedIn$ to true
            this.authService.setLoggedIn(true);
            // Redirect to the dashboard
            this.router.navigate(['/dashboard']);
          } else {
            console.error('No token received in the response');
          }
        },
        error => {
          console.error('Login failed', error);
          // Add logic for failed login (e.g., showing error message)
        }
      );
    }
  }
}

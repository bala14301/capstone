import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../../AuthService';
import { Subscription } from 'rxjs';
import { RegisterMemberComponent } from '../../_shared-module/register-member/register-member.component';
import { MatDialog } from '@angular/material/dialog';
import { MemberService } from '../../member.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
  isLoggedIn: boolean = false;
  isRefreshed: boolean = false;
  private authSubscription!: Subscription;

  constructor(
    private dialog: MatDialog,
    private router: Router,
    private memberService: MemberService,
    private authService: AuthService) { }

  ngOnInit() {
   
    this.authSubscription = this.authService.isLoggedIn$.subscribe(
      isLoggedIn => this.isLoggedIn = isLoggedIn
    );
  }

  ngOnDestroy() {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  logout() {
    this.authService.logout().subscribe((response:any)=>{
      this.isLoggedIn = false;
      this.authService.setLoggedIn(false);
      sessionStorage.clear();
      this.router.navigate(['/login']);

    });
  }


  createMember(member: any): void {
    this.memberService.createMember(member).subscribe(
      response => {
        console.log('Member created:', response);
        this.memberService.setIsRefreshed(true);
        // You can add additional logic here, such as refreshing the member list or showing a success message
      },
      error => {
        console.error('Error creating member:', error);
        // Handle any errors, such as displaying an error message to the user
      }
    );
  }

  openRegisterMemberModal(): void {
    const dialogRef = this.dialog.open(RegisterMemberComponent, {
      width: '900px',
      height: '500px'
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        // TODO: Implement createMember method or remove this call
        this.createMember(result);
        console.log('Member registration result:', result);
      }
    });
  }



}
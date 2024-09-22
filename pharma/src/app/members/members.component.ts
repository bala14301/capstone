import { Component, OnInit } from '@angular/core';
import { MemberService } from '../member.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { RegisterMemberComponent } from '../_shared-module/register-member/register-member.component';
import { DoctorService, PrescriptionService } from '../app.service';
import { DrugsService } from '../DrugsService';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrl: './members.component.scss'
})
export class MembersComponent implements OnInit {

  public memberForm!: FormGroup;

  ngOnInit() {
    this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers().subscribe(
      (data) => {
        this.members = data;
        console.log('Members loaded:', this.members);
      },
      (error) => {
        console.error('Error loading members:', error);
      }
    );
  }

  createSampleMember() {
    const newMember = {
      name: 'John Doe',
      age: 30,
      emailId: 'john.doe@example.com',
      mobileNo: '1234567890',
      disease: 'None'
    };

    this.memberService.createMember(newMember).subscribe(
      (response) => {
        console.log('Member created:', response);
        this.loadMembers(); // Reload the members list
      },
      (error) => {
        console.error('Error creating member:', error);
      }
    );
  }

  updateSampleMember() {
    // Assuming we want to update the first member in the list
    if (this.members.length > 0) {
      const memberToUpdate = this.members[0];
      memberToUpdate.name = 'Updated Name';

      this.memberService.updateMember(memberToUpdate.id, memberToUpdate).subscribe(
        (response) => {
          console.log('Member updated:', response);
          this.loadMembers(); // Reload the members list
        },
        (error) => {
          console.error('Error updating member:', error);
        }
      );
    }
  }

  deleteSampleMember() {
    // Assuming we want to delete the last member in the list
    if (this.members.length > 0) {
      const memberToDelete = this.members[this.members.length - 1];

      this.memberService.deleteMember(memberToDelete.id).subscribe(
        (response) => {
          console.log('Member deleted:', response);
          this.loadMembers(); // Reload the members list
        },
        (error) => {
          console.error('Error deleting member:', error);
        }
      );
    }
  }

  constructor(private memberService: MemberService, private drugsService: DrugsService,
    private dialog: MatDialog,
    private doctorService: DoctorService,
    private prescriptionService: PrescriptionService) {
    this.memberForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      age: new FormControl('', [Validators.required, Validators.min(0), Validators.max(150)]),
      emailId: new FormControl('', [Validators.required, Validators.email]),
      mobileNo: new FormControl('', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]),
      disease: new FormControl('', [Validators.maxLength(200)])
    });

  }
  members: any[] = [];

  columnDefs = [
    { field: 'id', headerName: 'ID', sortable: true },
    { field: 'name', headerName: 'Name', sortable: true },
    { field: 'age', headerName: 'Age', sortable: true },
    { field: 'emailId', headerName: 'Email', sortable: true },
    { field: 'mobileNo', headerName: 'Mobile No', sortable: true },
    { field: 'disease', headerName: 'Disease', sortable: true }
  ];

  defaultColDef = {
    flex: 1,
    minWidth: 100,
    resizable: true,
  };

  onGridReady(params: any) {
    params.api.sizeColumnsToFit();
  }

  // Model definition
  memberModel = {
    id: 0,
    name: '',
    age: 0,
    emailId: '',
    mobileNo: '',
    disease: ''
  };

  onSubmit() {

  }
  onCancel(): void {

  }

  createMember(member: any): void {
    this.memberService.createMember(member).subscribe(
      response => {
        console.log('Member created:', response);
        this.loadMembers();
        // You can add additional logic here, such as refreshing the member list or showing a success message
      },
      error => {
        console.error('Error creating member:', error);
        // Handle any errors, such as displaying an error message to the user
      }
    );
  }


  openAddMemberModal() {
    const dialogRef = this.dialog.open(RegisterMemberComponent, {
      width: '600px',
      data: { member: this.memberModel }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createMember(result);
      }
    });
  }
}

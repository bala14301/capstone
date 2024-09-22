import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-register-member',
  templateUrl: './register-member.component.html',
  styleUrl: './register-member.component.scss'
})
export class RegisterMemberComponent {
  memberForm!: FormGroup;

  constructor(private dialog: MatDialog , private fb: FormBuilder,
    public dialogRef: MatDialogRef<RegisterMemberComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.memberForm = this.fb.group({
        name: ['', [Validators.required, Validators.maxLength(100)]],
        age: ['', [Validators.required, Validators.min(0), Validators.max(150)]],
        emailId: ['', [Validators.required, Validators.email]],
        mobileNo: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
        disease: ['', [Validators.maxLength(200), Validators.required]]
      });
    }

    
  onSubmit() {
    if (this.memberForm.valid) {
      console.log(this.memberForm.value);
      this.dialogRef.close(this.memberForm.value);
      // Here you would typically call a service to save the new drug
    }
  }
  onCancel(): void {
    this.dialogRef.close();
  }
}

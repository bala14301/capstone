import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RegisterMemberComponent } from '../register-member/register-member.component';

@Component({
  selector: 'app-add-subscription',
  templateUrl: './add-subscription.component.html',
  styleUrl: './add-subscription.component.scss'
})
export class AddSubscriptionComponent  implements OnInit{
  susbcriptionForm!: FormGroup;
  memberLocations: string[] = [
    'Mumbai', 'Delhi', 'Bangalore', 'Hyderabad', 'Chennai', 
    'Kolkata', 'Ahmedabad', 'Pune', 'Jaipur', 'Lucknow',
    'Kanpur', 'Nagpur', 'Indore', 'Thane', 'Bhopal',
    'Visakhapatnam', 'Pimpri-Chinchwad', 'Patna', 'Vadodara', 'Ghaziabad',
    'Ludhiana', 'Agra', 'Nashik', 'Faridabad', 'Meerut',
    'Rajkot', 'Kalyan-Dombivli', 'Vasai-Virar', 'Varanasi', 'Srinagar',
    'Aurangabad', 'Dhanbad', 'Amritsar', 'Navi Mumbai', 'Allahabad',
    'Ranchi', 'Howrah', 'Coimbatore', 'Jabalpur', 'Gwalior',
    'Vijayawada', 'Jodhpur', 'Madurai', 'Raipur', 'Kota',
    'Guwahati', 'Chandigarh', 'Solapur', 'Hubballi-Dharwad', 'Tiruchirappalli'
  ];
  members: any[] =[];
  prescriptions: any[] =[];
  drugs: any[] =[];


  constructor(private dialog: MatDialog , private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddSubscriptionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {

      this.susbcriptionForm = this.fb.group({
        memberId: ['', [Validators.required, Validators.maxLength(10)]],
        subscriptionDate: ['', Validators.required],
        prescriptionId: ['', Validators.required],
        refillOccurrence: ['', Validators.required],
        memberLocation: ['', [Validators.required, Validators.maxLength(100)]],
        endDate: ['', Validators.required],
        startDate: ['', Validators.required],
        subscriptionStatus: ['', [Validators.required, Validators.min(0), Validators.max(1)]]
      });
    }


    ngOnInit():void{
    console.log('Received data:', this.data);
    
    if (this.data) {
     
      this.members = this.data.members;
      this.prescriptions = this.data.prescriptions;
      this.drugs = this.data.drugs;
    }

    }

  updateSubscriptionStatus(event: Event): void {
    const checkbox = event.target as HTMLInputElement;
    const status = checkbox.checked ? 1 : 0;
    this.susbcriptionForm.patchValue({
      subscriptionStatus: status
    });
  }
    
  onSubmit() {
    if (this.susbcriptionForm.valid) {
      console.log(this.susbcriptionForm.value);
      this.dialogRef.close(this.susbcriptionForm.value);
      // Here you would typically call a service to save the new drug
    }
  }
  onCancel(): void {
    this.dialogRef.close();
  }
}

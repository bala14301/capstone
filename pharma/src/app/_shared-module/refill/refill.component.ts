import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddSubscriptionComponent } from '../add-subscription/add-subscription.component';

@Component({
  selector: 'app-refill',
  templateUrl: './refill.component.html',
  styleUrl: './refill.component.scss'
})
export class RefillComponent {
  RefillForm!: FormGroup;
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
  members: any[] = [];
  subscriptions: any[] = [];
  drugs: any[] = [];


  constructor(private dialog: MatDialog, private fb: FormBuilder,
    public dialogRef: MatDialogRef<RefillComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.RefillForm = this.fb.group({
      id: [0, Validators.required],
      subscriptionId: [0, Validators.required],
      refillOrderId: [0, Validators.required],
      drugId: [0, Validators.required],
      quantity: [0, [Validators.required, Validators.min(0)]]
    });
  }


  ngOnInit(): void {
    console.log('Received data:', this.data);

    if (this.data) {
      this.members = this.data.members;
      this.subscriptions = this.data.subscriptions;
      this.drugs = this.data.drugs;
    }

  }

  onSubmit() {
    if (this.RefillForm.valid) {
      console.log(this.RefillForm.value);
      let body={...this.RefillForm.value};
      delete body['id'];
      this.dialogRef.close(this.RefillForm.value);
      // Here you would typically call a service to save the new drug
    }
  }
  onCancel(): void {
    this.dialogRef.close();
  }
}

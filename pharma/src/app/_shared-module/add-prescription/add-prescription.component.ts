import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddSubscriptionComponent } from '../add-subscription/add-subscription.component';

@Component({
  selector: 'app-add-prescription',
  templateUrl: './add-prescription.component.html',
  styleUrl: './add-prescription.component.scss'
})
export class AddPrescriptionComponent implements OnInit {
  prescriptionForm!: FormGroup;
  doctors: any[] = [];
  drugs :any[]=[];
  members :any[]=[];
  isEditing: boolean = false;

  dosageOptions: { value: string; viewValue: string }[] = [
    { value: '5mg', viewValue: '5mg' },
    { value: '10mg', viewValue: '10mg' },
    { value: '20mg', viewValue: '20mg' },
    { value: '50mg', viewValue: '50mg' },
    { value: '100mg', viewValue: '100mg' },
  ];

  frequencyOptions: { value: string; viewValue: string }[] = [
    { value: 'Once daily', viewValue: 'Once daily' },
    { value: 'Twice daily', viewValue: 'Twice daily' },
    { value: 'Three times daily', viewValue: 'Three times daily' },
    { value: 'Every 4 hours', viewValue: 'Every 4 hours' },
    { value: 'As needed', viewValue: 'As needed' },
  ];

  constructor(private dialog: MatDialog, private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddSubscriptionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.prescriptionForm = this.fb.group({
      memberId: ['', Validators.required],
      // drugId: ['', [Validators.required, Validators.min(1)]],
      dosage: ['', Validators.required],
      drugIds:[[],Validators.required],
      frequency: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      refills: ['', [Validators.required, Validators.min(0)]],
      lastRefillDate: [''],
      isActive: [true, Validators.required],
      prescribedBy: ['', Validators.required]
    });

    // Check if we're in edit mode
    this.isEditing = data && data.isEditing === true;
    // If we're in edit mode, populate the form with existing prescription data
    if (this.isEditing && this.data.prescription) {
      this.prescriptionForm.patchValue({
        memberId: this.data.prescription.memberId,
        dosage: this.data.prescription.dosage,
        drugIds: this.data.prescription.drugIds,
        frequency: this.data.prescription.frequency,
        startDate: this.data.prescription.startDate,
        endDate: this.data.prescription.endDate,
        refills: this.data.prescription.refills,
        lastRefillDate: this.data.prescription.lastRefillDate,
        isActive: this.data.prescription.isActive,
        prescribedBy: this.data.prescription.prescribedBy
      });
      // Convert dates to appropriate format if needed
      if (this.data.prescription.startDate) {
        const startDate = new Date(this.data.prescription.startDate);
        this.prescriptionForm.get('startDate')?.setValue(startDate.toISOString().split('T')[0]);
      }
      if (this.data.prescription.endDate) {
        const endDate = new Date(this.data.prescription.endDate);
        this.prescriptionForm.get('endDate')?.setValue(endDate.toISOString().split('T')[0]);
      }
      if (this.data.prescription.lastRefillDate) {
        const lastRefillDate = new Date(this.data.prescription.lastRefillDate);
        this.prescriptionForm.get('lastRefillDate')?.setValue(lastRefillDate.toISOString().split('T')[0]);
      }

      // Trigger change detection
      this.prescriptionForm.updateValueAndValidity();
    // Ensure dates are properly formatted for the form inputs
    if (this.data.prescription.startDate) {
      const startDate = new Date(this.data.prescription.startDate);
      this.prescriptionForm.get('startDate')?.setValue(this.formatDateForInput(startDate));
    }
    if (this.data.prescription.endDate) {
      const endDate = new Date(this.data.prescription.endDate);
      this.prescriptionForm.get('endDate')?.setValue(this.formatDateForInput(endDate));
    }
    if (this.data.prescription.lastRefillDate) {
      const lastRefillDate = new Date(this.data.prescription.lastRefillDate);
      this.prescriptionForm.get('lastRefillDate')?.setValue(this.formatDateForInput(lastRefillDate));
    }

    // Helper function to format date for input
   
    }
  }

  private formatDateForInput(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  onDrugSelectionChange(event: Event, drugId: number): void {
    const checkbox = event.target as HTMLInputElement;
    const drugIds = this.prescriptionForm.get('drugIds')?.value as number[];

    if (checkbox.checked) {
      // Add the drug ID if it's not already in the array
      if (!drugIds.includes(drugId)) {
        drugIds.push(drugId);
      }
    } else {
      // Remove the drug ID from the array
      const index = drugIds.indexOf(drugId);
      if (index > -1) {
        drugIds.splice(index, 1);
      }
    }

    // Update the form control value
    this.prescriptionForm.get('drugIds')?.setValue(drugIds);
  }

  isDrugSelected(drugId: number): boolean {
    const drugIds = this.prescriptionForm.get('drugIds')?.value as number[];
    return drugIds.includes(drugId);
  }

  ngOnInit(): void {
    this.doctors = this.data.doctors;
    // Accept drugs data from the dialog input
    // Accept members data from the dialog input
    if (this.data && this.data.members) {
      this.members = this.data.members;
    } else {
      console.warn('No members data provided to the dialog');
      this.members = [];
    }
    if (this.data && this.data.drugs) {
      this.drugs = this.data.drugs;
    } else {
      console.warn('No drugs data provided to the dialog');
      this.drugs = [];
    }

    // Initialize the drugIds form control as a multi-select
    this.prescriptionForm.get('drugIds')?.setValue([]);

    if (this.isEditing && this.data.prescription) {
      // Populate the form with existing prescription data
      this.prescriptionForm.patchValue(this.data.prescription);
    }
  }

  onSubmit() {
    if (this.prescriptionForm.valid) {
      const result = {
        ...this.prescriptionForm.value,
        id: this.isEditing ? this.data.prescription.id : null
      };
      if(!this.isEditing) delete result['id']
      console.log(result);
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

}

import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-drug-modal',
  templateUrl: './add-drug-component.component.html',
  styleUrls: ['./add-drug-component.component.scss']
})
export class AddDrugModalComponent implements OnInit {
  drugForm!: FormGroup;
  drug: any = {};

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddDrugModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.drugForm = this.fb.group({
      name: ['', Validators.required],
      manufacturer: ['', Validators.required],
      manufacturedDate: ['', Validators.required],
      expiryDate: ['', Validators.required],
      totalQuantity: ['', [Validators.required, Validators.min(0)]],
      location: ['', Validators.required],
      description: [''],
      price: ['', [Validators.required, Validators.min(0)]],
      dosageForm: ['', Validators.required],
      strength: ['', Validators.required],
      quantityAvailable: ['', [Validators.required, Validators.min(0)]]
    });
  }

  

  ngOnInit() {
  }

  onSubmit() {
    if (this.drugForm.valid) {
     
      console.log(this.drugForm.value);
      this.dialogRef.close(this.drugForm.value);
      // Here you would typically call a service to save the new drug
    }
  }
  onCancel(): void {
    this.dialogRef.close();
  }
}
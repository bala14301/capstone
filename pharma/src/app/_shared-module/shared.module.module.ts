import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddDrugModalComponent } from './add-drug-component/add-drug-component.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { RegisterMemberComponent } from './register-member/register-member.component';
import { AddSubscriptionComponent } from './add-subscription/add-subscription.component';
import { AddPrescriptionComponent } from './add-prescription/add-prescription.component';
import { RefillComponent } from './refill/refill.component';

@NgModule({
  declarations: [AddDrugModalComponent, RegisterMemberComponent, AddSubscriptionComponent, AddPrescriptionComponent, RefillComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule  // Add this line
  ],
  exports: [AddDrugModalComponent]
})
export class SharedModule { }
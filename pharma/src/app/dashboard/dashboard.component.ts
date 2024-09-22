import { Component, OnInit } from '@angular/core';
import { DrugsService, Drug } from '../DrugsService';
import { ColDef, GridReadyEvent } from 'ag-grid-community';
import { MatDialog } from '@angular/material/dialog';
import { AddDrugModalComponent } from '../_shared-module/add-drug-component/add-drug-component.component';
import { RegisterMemberComponent } from '../_shared-module/register-member/register-member.component';
import { MemberService } from '../member.service';
import { AddSubscriptionComponent } from '../_shared-module/add-subscription/add-subscription.component';
import { AddPrescriptionComponent } from '../_shared-module/add-prescription/add-prescription.component';
import { DoctorService, PrescriptionService, RefillService, SubscriptionService } from '../app.service';
import { subscribeOn } from 'rxjs';
import { RefillComponent } from '../_shared-module/refill/refill.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  drugs: Drug[] = [];
  drug: Drug | null = null;
  members: any[] = [];
  subscriptions: any[] = [];
  refills: any[] = [];
  doctors: any[] = [];
  prescriptions: any[] = [];
  availability: any;
  searchResults: any;
  searchTerm: string = '';
  location: string = '';

  // ag-Grid properties
  columnDefs: ColDef[] = [
    { field: 'id', sortable: true },
    { field: 'name', sortable: true },
    { field: 'description', sortable: true },
    { field: 'dosageForm', sortable: true },
    { field: 'expiryDate', sortable: true },
    { field: 'location', sortable: true },
    { field: 'manufacturedDate', sortable: true },
    { field: 'manufacturer', sortable: true },
    { field: 'price', sortable: true },
    { field: 'quantityAvailable', sortable: true },
    { field: 'strength', sortable: true },
    { field: 'totalQuantity', sortable: true }
    ,
    {
      field: 'edit',
      headerName: 'Edit',
      sortable: false,
      cellRenderer: (params: any) => {
        return `
        <div title="Edit functionality is disabled" style="cursor: not-allowed;">
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit-2" style="opacity: 0.5;">
            <path d="M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z"></path>
          </svg>
        </div>
      `;
      },
      onCellClicked: (params: any) => {
        // Functionality remains disabled
      }
    },
    {
      field: 'delete',
      headerName: 'Delete',
      sortable: false,
      cellRenderer: (params: any) => {
        return `
        <div title="Delete functionality is disabled" style="cursor: not-allowed;">
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash-2" style="opacity: 0.5;">
            <polyline points="3 6 5 6 21 6"></polyline>
            <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
            <line x1="10" y1="11" x2="10" y2="17"></line>
            <line x1="14" y1="11" x2="14" y2="17"></line>
          </svg>
        </div>
      `;
      },
      onCellClicked: (params: any) => {
        // Disabled functionality
      }
    }
  ];

  prescriptionColumnDefs: ColDef[] = [
    { field: 'id', headerName: 'ID', sortable: true },
    {
      field: 'memberId',
      headerName: 'Member ID',
      sortable: true,
      cellRenderer: (params: any) => {
        return `
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-user">
            <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path>
            <circle cx="12" cy="7" r="4"></circle>
          </svg>
          ${params.value}
        `;
      }
    },
    { field: 'drugIds', headerName: 'Drug IDs', sortable: true },
    {
      field: 'dosage',
      headerName: 'Dosage',
      minWidth: 150,
      sortable: true,
      cellRenderer: (params: any) => {
        return `
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-thermometer">
            <path d="M14 14.76V3.5a2.5 2.5 0 0 0-5 0v11.26a4.5 4.5 0 1 0 5 0z"></path>
          </svg>
          ${params.value}
        `;
      }
    },
    { field: 'frequency', minWidth: 200, headerName: 'Frequency', sortable: true },
    { field: 'startDate', headerName: 'Start Date', sortable: true },
    { field: 'endDate', headerName: 'End Date', sortable: true },
    { field: 'refills', headerName: 'Refills', sortable: true },
    {
      field: 'lastRefillDate',
      headerName: 'Last Refill Date',
      minWidth: 150,
      sortable: true,
      cellRenderer: (params: any) => {
        return `
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-refresh-cw">
            <polyline points="23 4 23 10 17 10"></polyline>
            <polyline points="1 20 1 14 7 14"></polyline>
            <path d="M3.51 9a9 9 0 1 14.85-3.36L23 10M1 14l4.64 4.36A9 9 0 0 0 20.49 15"></path>
          </svg>
          ${params.value}
        `;
      }
    },
    { field: 'isActive', headerName: 'Is Active', sortable: true },
    {
      field: 'prescribedBy',
      headerName: 'Prescribed By',
      minWidth: 220,
      sortable: true,
      cellRenderer: (params: any) => {
        return `
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-user-plus">
            <path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path>
            <circle cx="8.5" cy="7" r="4"></circle>
            <line x1="20" y1="8" x2="20" y2="14"></line>
            <line x1="23" y1="11" x2="17" y2="11"></line>
          </svg>
          ${params.value}
        `;
      }
    }
    ,
    {
      headerName: 'Edit',
      cellRenderer: (params: any) => {
        return `
          <button class="btn-edit" (click)="editPrescription(${params.data.id})">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#0071bd" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit" style="outline: none; border: none;">
              <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path>
              <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path>
            </svg>
          </button>
        `;
      },
      onCellClicked: (params: any) => {
        this.editPrescription(params.data.id);
      }
    },
    {
      headerName: 'Delete',
      cellRenderer: (params: any) => {
        return `
          <button class="btn-delete" (click)="deletePrescription(${params.data.id})">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#0071bd" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash-2" style="outline: none; border: none;">
              <polyline points="3 6 5 6 21 6"></polyline>
              <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
              <line x1="10" y1="11" x2="10" y2="17"></line>
              <line x1="14" y1="11" x2="14" y2="17"></line>
            </svg>
          </button>
        `;
      },
      onCellClicked: (params: any) => {
        this.deletePrescription(params.data.id);
      }
    }
  ];

  memberColumnDefs: ColDef[] = [
    { field: 'id', headerName: 'ID', sortable: true },
    { field: 'name', headerName: 'Name', sortable: true },
    { field: 'age', headerName: 'Age', sortable: true },
    { field: 'emailId', headerName: 'Email', sortable: true },
    { field: 'mobileNo', headerName: 'Mobile No', sortable: true },
    { field: 'disease', headerName: 'Disease', sortable: true }
  ];


  subscriptionColumnDefs: ColDef[] = [
    { field: 'memberId', headerName: 'Member ID', sortable: true },
    {
      field: 'subscriptionDate',
      headerName: 'Subscription Date',
      sortable: true,
      cellRenderer: (params: any) => {
        return new Date(params.value).toLocaleDateString();
      }
    },
    { field: 'prescriptionId', headerName: 'Prescription ID', sortable: true },
    { field: 'refillOccurrence', headerName: 'Refill Occurrence', sortable: true },
    { field: 'memberLocation', headerName: 'Member Location', sortable: true },
    {
      field: 'endDate',
      headerName: 'End Date',
      sortable: true,
      cellRenderer: (params: any) => {
        return new Date(params.value).toLocaleDateString();
      }
    },
    {
      field: 'startDate',
      headerName: 'Start Date',
      sortable: true,
      cellRenderer: (params: any) => {
        return new Date(params.value).toLocaleDateString();
      }
    },
    {
      field: 'subscriptionStatus',
      headerName: 'Subscription Status',
      sortable: true,
      cellRenderer: (params: any) => {
        return params.value === 1 ? 'Active' : 'Inactive';
      }
    }
    ,
    {
      field: 'delete',
      headerName: 'Delete',
      sortable: false,
      cellRenderer: (params: any) => {
        return `
        <div style="cursor: pointer;">
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="red">
            <path d="M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z"/>
          </svg>
        </div>
      `;
      },
      onCellClicked: (params: any) => {
        this.deleteSubscription(params.data);
      }
    }
  ];


  refillsDef: ColDef[] = [
    { field: 'id', headerName: 'ID', sortable: true },
    { field: 'subscriptionId', headerName: 'Subscription ID', sortable: true },
    { field: 'refillOrderId', headerName: 'Refill Order ID', sortable: true },
    { field: 'drugId', headerName: 'Drug ID', sortable: true },
    { field: 'quantity', headerName: 'Quantity', sortable: true }
  ];
  defaultColDef: ColDef = {
    flex: 1,
    minWidth: 100,
    resizable: true,
  };

  isRefreshed: boolean = false;

  constructor(
    private drugsService: DrugsService,
    private dialog: MatDialog,
    private memberService: MemberService,
    private doctorService: DoctorService,
    private prescriptionService: PrescriptionService,
    private subscriptionService: SubscriptionService,
    private refillService: RefillService
  ) { }


  deleteSubscription(subscribtion: any): void {
    if (confirm('Are you sure you want to delete this subscription?')) {
      this.subscriptionService.unSubscribe(subscribtion).subscribe(
        () => {
          console.log('Subscription deleted successfully');
          this.subscriptionService.refreshData();
          this.getAllSubscriptions(); // Refresh the subscription list
        },
        error => {
          console.error('Error deleting subscription:', error);
          // Handle error (e.g., show error message)
        }
      );
    }
  }

  editPrescription(prescriptionId: number): void {
    // Fetch the prescription data
    this.prescriptionService.getPrescriptionById(prescriptionId).subscribe(
      (prescription) => {
        const dialogRef = this.dialog.open(AddPrescriptionComponent, {
          width: '700px',
          height: '500px',
          data: { doctors: this.doctors, drugs: this.drugs, members: this.members, prescription: { ...prescription }, isEditing: true }
        });

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this.prescriptionService.updatePrescription(prescriptionId, result).subscribe(
              updatedPrescription => {
                console.log('Prescription updated:', updatedPrescription);
                this.getAllPrescriptions(); // Refresh the prescription list
              },
              error => {
                console.error('Error updating prescription:', error);
                // Handle error (e.g., show error message)
              }
            );
          }
        });
      },
      error => {
        console.error('Error fetching prescription:', error);
        // Handle error (e.g., show error message)
      }
    );
  }


  deletePrescription(prescriptionId: number): void {
    if (confirm('Are you sure you want to delete this prescription?')) {
      this.prescriptionService.deletePrescription(prescriptionId).subscribe(
        () => {
          console.log('Prescription deleted successfully');
          this.getAllPrescriptions(); // Refresh the prescription list
        },
        error => {
          console.error('Error deleting prescription:', error);
          // Handle error (e.g., show error message)
        }
      );
    }
  }

  editDrug(drug: Drug): void {
    const dialogRef = this.dialog.open(AddDrugModalComponent, {
      width: '500px',
      data: { ...drug }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // this.drugsService.updateDrug(result.id, result).subscribe(
        //   updatedDrug => {
        //     console.log('Drug updated:', updatedDrug);
        //     this.getAllDrugs(); // Refresh the drug list
        //   },
        //   error => {
        //     console.error('Error updating drug:', error);
        //     // Handle error (e.g., show error message)
        //   }
        // );
      }
    });
  }


  deleteDrug(drug: Drug): void {
    if (confirm(`Are you sure you want to delete ${drug.name}?`)) {
      // this.drugsService.deleteDrug(drug.id).subscribe(
      //   () => {
      //     console.log('Drug deleted successfully');
      //     this.getAllDrugs(); // Refresh the drug list
      //   },
      //   error => {
      //     console.error('Error deleting drug:', error);
      //     // Handle error (e.g., show error message)
      //   }
      // );
    }
  }

  ngOnInit(): void {
    // Example usage
    // this.getDrugById('1');
    // this.checkAvailability(1, 'New York');
    // this.searchDrugsByName('Aspirin');
    this.getAllDrugs();
    this.getMembers();
    this.getAllDoctors();
    this.getAllPrescriptions();
    this.getAllSubscriptions();
    this.getAllRefills();

    this.memberService.isRefreshed$.subscribe((isRefreshed: any) => {
      this.isRefreshed = isRefreshed
      this.getMembers();
    });


    this.doctorService.isRefreshed$.subscribe((isRefreshed: any) => {
      this.isRefreshed = isRefreshed;
      this.getAllDoctors();
    });

    this.prescriptionService.isRefreshed$.subscribe((isRefreshed: any) => {
      this.isRefreshed = isRefreshed;
      this.getAllDoctors();
    });
  }


  getAllSubscriptions(): void {
    this.subscriptionService.getAllSubscriptions().subscribe(response => {
      this.subscriptions = response;
      console.log('Subscriptions:', this.subscriptions);
    });
  }

  getMembers(): void {
    this.memberService.getMembers().subscribe(response => {
      this.members = response;
      console.log('Members:', this.members);
    });
  }

  getAllRefills(): void {
    this.refillService.getAllRefills().subscribe(response => {
      this.refills = response;
      console.log('Refills:', this.refills);
    });
  }

  getAllDoctors(): void {
    this.doctorService.getAllDoctors().subscribe(response => {
      this.doctors = response;
      console.log('Doctors:', this.doctors);
    });
  }

  getAllPrescriptions(): void {
    this.prescriptionService.getAllPrescriptions().subscribe(response => {
      this.prescriptions = response;
      console.log('Prescriptions:', this.prescriptions);
    });
  }

  checkAvailability(drugId: number, location: string): void {
    this.drugsService.checkAvailability(drugId, location).subscribe(response => {
      this.availability = response;
      console.log('Availability checked:', this.availability);
    });
  }

  getAllDrugs(): void {
    this.drugsService.getAllDrugs().subscribe(response => {
      this.drugs = response;
      console.log('All drugs:', this.drugs);
    });
  }

  private gridApi: any;

  onGridReady(params: GridReadyEvent) {
    this.gridApi = params.api; // Store the grid API for later use
    params.api.sizeColumnsToFit();
  }

  getAvailabilityByLocation(location: string): void {
    this.drugsService.getAvailabilityByLocation(location).subscribe(response => {
      this.availability = response;
      console.log('Availability by location:', this.availability);
    });
  }

  addDrug(drug: Drug): void {
    this.drugsService.addDrug(drug).subscribe(response => {
      console.log('Drug added:', response);
      this.getAllDrugs(); // Refresh the drug list
    });
  }

  addMultipleDrugs(drugs: Drug[]): void {
    this.drugsService.addMultipleDrugs(drugs).subscribe(response => {
      console.log('Multiple drugs added:', response);
    });
  }

  createMember(member: any): void {
    this.memberService.createMember(member).subscribe(
      response => {
        console.log('Member created:', response);
        // You can add additional logic here, such as refreshing the member list or showing a success message
      },
      error => {
        console.error('Error creating member:', error);
        // Handle any errors, such as displaying an error message to the user
      }
    );
  }

  createSubscription(subscription: any): void {
    this.subscriptionService.createSubscription(subscription).subscribe(
      response => {
        console.log('Subscription created:', response);
        this.subscriptionService.refreshData();
        this.getAllSubscriptions();
        // You can add additional logic here, such as refreshing the subscription list or showing a success message
      },
      error => {
        console.error('Error creating subscription:', error);
      }
    );
  }

  createPrescription(subscription: any): void {
    this.prescriptionService.createPrescription(subscription).subscribe(
      response => {
        console.log('Subscription created:', response);
        this.prescriptionService.refreshData();
        this.getAllPrescriptions();
        // You can add additional logic here, such as refreshing the subscription list or showing a success message
      },
      error => {
        console.error('Error creating subscription:', error);
      }
    );
  }

  searchDrugsByName(drugName: string): void {
    this.drugsService.searchDrugsByName(drugName).subscribe(response => {
      this.searchResults = response;
      console.log('Drugs found by name:', this.searchResults);
    });
  }

  searchDrugsById(drugId: number): void {
    this.drugsService.searchDrugsById(drugId).subscribe(response => {
      this.searchResults = response;
      console.log('Drugs found by ID:', this.searchResults);
    });
  }


  openAddDrugModal(): void {
    const dialogRef = this.dialog.open(AddDrugModalComponent, {
      width: '900px',
      height: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.addDrug(result);
      }
    });
  }


  openRegisterMemberModal(): void {
    const dialogRef = this.dialog.open(RegisterMemberComponent, {
      width: '900px',
      height: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createMember(result);
      }
    });
  }

  openSubscriptionModal(): void {
    const dialogRef = this.dialog.open(AddSubscriptionComponent, {
      width: '900px',
      height: '500px'
      , data: {
        members: this.members,
        prescriptions: this.prescriptions,
        drugs: this.drugs
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createSubscription(result);
        this.subscriptionService.refreshData();
        this.getAllSubscriptions();
      }
    });
  }


  openPrescriptionModal() {
    this.doctorService.getAllDoctors().subscribe(doctors => {
      this.drugsService.getAllDrugs().subscribe(drugs => {
        const dialogRef = this.dialog.open(AddPrescriptionComponent, {
          width: '900px',
          height: '500px',
          data: { doctors: doctors, drugs: drugs, members: this.members }
        });

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this.createPrescription(result);
          }
        });
      });
    });
  }

  openRefillModal() {
    const dialogRef = this.dialog.open(RefillComponent, {
      width: '700px',
      height: '500px'
      , data: {
        drugs: this.drugs,
        subscriptions: this.subscriptions
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createRefill(result);
      }
    });
  }

  createRefill(refill: any): void {
    this.refillService.createRefill(refill).subscribe(response => {
      console.log('Refill created:', response);
      this.refillService.refreshData();
      this.getAllRefills();
    });
  }

  searchDrug(): void {
    if (this.searchTerm.trim() !== '') {
      this.drugsService.searchDrugsByName(this.searchTerm).subscribe({
        next: (result) => {
          if (result.length === 0) {
            alert('No drugs found with the given name.');
          } else {
            this.drugs = result;
            this.updateGridData();
          }
        },
        error: (error) => {
          console.error('Error searching drugs by name:', error);
          alert('An error occurred while searching for drugs. Please try again.');
        }
      });
    } else if (this.location.trim() !== '') {
      this.drugsService.getAvailabilityByLocation(this.location).subscribe({
        next: (result) => {
          if (result.length === 0) {
            alert('No drugs available at the specified location.');
          } else {
            this.drugs = result;
            this.updateGridData();
          }
        },
        error: (error) => {
          console.error('Error getting drug availability by location:', error);
          alert('An error occurred while checking availability. Please try again.');
        }
      });
    } else {
      this.getAllDrugs(); // Refresh the drug list if no search term or location is provided
    }
  }

  private updateGridData(): void {
    if (this.gridApi && this.drugs) {
      this.gridApi.setRowData(this.drugs);
    }
  }
}

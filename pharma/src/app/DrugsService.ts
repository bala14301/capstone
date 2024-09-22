import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DrugsService {
    private apiUrl = 'https://api.example.com/drugs';

    constructor(private http: HttpClient) { }

    // Check availability of a drug
    checkAvailability(drugId: number, location: string): Observable<any> {
        const body: CheckAvailabilityRequest = { drugId, location };
        return this.http.post<any>('http://localhost:5119/api/Drugs/checkAvailability', body);
    }


    getAvailabilityByLocation(location: string): Observable<any> {
        return this.http.get<any>(`http://localhost:5119/api/Drugs/checkAvailabilityByLocation?location=${location}`);
    }


    // Add a single drug
    addDrug(drug: Drug): Observable<any> {
        return this.http.post<any>('http://localhost:5119/api/Drugs/add', drug);
    }

    getAllDrugs(): Observable<any> {
        return this.http.get<any>('http://localhost:5119/api/Drugs/getAllDrugs');
    }

    // Add multiple drugs
    addMultipleDrugs(drugs: Drug[]): Observable<any> {
        return this.http.post<any>('http://localhost:5119/api/Drugs/addMultiple', drugs);
    }


    searchDrugsByName(drugName: string): Observable<any> {
        return this.http.get<any>(`http://localhost:5119/api/Drugs/searchDrugsByName?drugName=${drugName}`);
    }


    searchDrugsById(drugId: number): Observable<any> {
        return this.http.get<any>(`http://localhost:5119/api/Drugs/searchDrugsByID?drugId=${drugId}`);
    }
}




interface CheckAvailabilityRequest {
    drugId: number;
    location: string;
}

// Define the Drug model
export interface Drug {
    id: number;
    name: string;
    manufacturer: string;
    manufacturedDate: string;
    expiryDate: string;
    totalQuantity: number;
    location: string;
    description: string;
    price: number;
    dosageForm: string;
    strength: string;
    quantityAvailable: number;
}
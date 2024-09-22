import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private baseUrl = 'http://localhost:5119/api/Doctor';


  
  private isRefreshedSubject = new BehaviorSubject<boolean>(false);
  isRefreshed$ = this.isRefreshedSubject.asObservable();

  constructor(private http: HttpClient) { }

  refreshData() {
    this.isRefreshedSubject.next(true);
  }

  // Create a new doctor
  createDoctor(doctor: any): Observable<any> {
    return this.http.post(this.baseUrl, doctor);
  }

  // Get all doctors
  getAllDoctors(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl+"/all");
  }

  // Get a specific doctor by ID
  getDoctorById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  // Update a doctor
  updateDoctor(id: number, doctor: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, doctor);
  }

  // Delete a doctor
  deleteDoctor(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}





@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {
  private baseUrl = 'http://localhost:5119/api/Prescription';

  private isRefreshedSubject = new BehaviorSubject<boolean>(false);
  isRefreshed$ = this.isRefreshedSubject.asObservable();

  refreshData() {
    this.isRefreshedSubject.next(true);
  }

  constructor(private http: HttpClient) { }

  // Create a new prescription
  createPrescription(prescription: any): Observable<any> {
    return this.http.post(this.baseUrl, prescription);
  }

  // Get all prescriptions
  getAllPrescriptions(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}`);
  }

  // Get a specific prescription by ID
  getPrescriptionById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  // Update a prescription
  updatePrescription(id: number, prescription: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, prescription);
  }

  // Delete a prescription
  deletePrescription(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}




@Injectable({
  providedIn: 'root'
})
export class MemberService {
  private baseUrl = 'http://localhost:5119/api/Member';

  private isRefreshedSubject = new BehaviorSubject<boolean>(false);
  isRefreshed$ = this.isRefreshedSubject.asObservable();

  constructor(private http: HttpClient) { }

  refreshData() {
    this.isRefreshedSubject.next(true);
  }

  // Get all members
  getMembers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/all`);
  }

  // Get a specific member by ID
  getMemberById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  // Create a new member
  createMember(member: any): Observable<any> {
    return this.http.post(this.baseUrl, member);
  }

  // Update a member
  updateMember(id: number, member: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, member);
  }

  // Delete a member
  deleteMember(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}







@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  private baseUrl = 'http://localhost:5119/api/Subscription';

  constructor(private http: HttpClient) { }


  private isRefreshedSubject = new BehaviorSubject<boolean>(false);
  isRefreshed$ = this.isRefreshedSubject.asObservable();

  refreshData() {
    this.isRefreshedSubject.next(true);
  }

  // Create a new subscription
  createSubscription(subscription: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/subscribe`, subscription);
  }
  unSubscribe(unsubscription: any): Observable<any> {
    let body ={
       
            "memberId": unsubscription.memberId,
            "subscriptionId": unsubscription.id
       
    }
    return this.http.post(`${this.baseUrl}/unsubscribe`, body);
  }

  // Get all subscriptions
  getAllSubscriptions(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/all`);
  }
}



@Injectable({
  providedIn: 'root'
})
export class RefillService {
  private baseUrl = 'http://localhost:5119/api/Refill';

  constructor(private http: HttpClient) { }

  private isRefreshedSubject = new BehaviorSubject<boolean>(false);
  isRefreshed$ = this.isRefreshedSubject.asObservable();

  refreshData() {
    this.isRefreshedSubject.next(true);
  }

  // Create a new refill
  createRefill(refill: any): Observable<any> {
    delete refill['id']
    return this.http.post(`${this.baseUrl}/request`, refill);
  }

  // Get all refills
  getAllRefills(): Observable<any[]> {
    
    return this.http.get<any[]>(`${this.baseUrl}/getAllRefills`);
  }
}

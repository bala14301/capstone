import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  //  http://localhost:5119/api/Member

  private isRefreshedSubject = new BehaviorSubject<boolean>(false);
  public isRefreshed$: Observable<boolean> = this.isRefreshedSubject.asObservable();

  setIsRefreshed(value: boolean): void {
    this.isRefreshedSubject.next(value);
  }

  public isRefreshed: boolean = false;
  private apiUrl = 'http://localhost:5119/api/Member'; // Replace with your actual API endpoint

  constructor(private http: HttpClient) { }

  // Create
  createMember(member: any): Observable<any> {
    return this.http.post(this.apiUrl, member);
  }

  // Read (Get all members)
  getMembers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl+'/all');
  }

  // Read (Get a single member by ID)
  getMemberById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // Update
  updateMember(id: number, member: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, member);
  }

  // Delete
  deleteMember(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}



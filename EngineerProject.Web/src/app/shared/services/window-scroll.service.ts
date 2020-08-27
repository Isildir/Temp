import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WindowScrollService {
  public contentScrolledPercentage = new BehaviorSubject(0);
}

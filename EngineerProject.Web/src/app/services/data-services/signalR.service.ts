import { Observable, BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public messageListener: BehaviorSubject<string>;

  private hubConnection: signalR.HubConnection;

  constructor() { }

  public startConnection(groupId: number) {
    const url = `${environment.signalRUrl}?groupId=${groupId}`;

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url)
      .build();

    let result: string;

    this.hubConnection
      .start()
      .then(() => result = 'Connection started')
      .catch(err => result = 'Error while starting connection: ' + err);

    this.messageListener = new BehaviorSubject<string>(result);

    this.configureMethods();

    return result;
  }

  private configureMethods() {
    this.hubConnection.on('appendMessage', data => { console.log(data); this.messageListener.next(data);});
  }

  public sendMessage(content: string) {
    this.hubConnection.send('SendMessage', content);
  }
}

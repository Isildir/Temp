import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { IHttpConnectionOptions } from '@aspnet/signalr';
import { SharedDataService } from 'src/app/shared/services/shared-data.service';
import { Message } from '../interfaces/Message';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public messageListener: BehaviorSubject<Message>;

  private hubConnection: signalR.HubConnection;

  constructor(private sharedData: SharedDataService) { }

  public startConnection(groupId: number) {
    const url = `${environment.signalRUrl}?groupId=${groupId}`;

    const options: IHttpConnectionOptions = {
      accessTokenFactory: () => this.sharedData.userSubject.value.token
    };

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url, options)
      .build();

    let result: string;

    this.hubConnection
      .start()
      .then(() => result = 'Connection started')
      .catch(err => result = 'Error while starting connection: ' + err);

    this.messageListener = new BehaviorSubject<Message>({ content: result } as Message);

    this.configureMethods();

    return result;
  }

  private configureMethods() {
    this.hubConnection.on('appendMessage', data => this.messageListener.next(data));
  }

  public sendMessage(content: string) {
    this.hubConnection.send('SendMessage', content);
  }
}

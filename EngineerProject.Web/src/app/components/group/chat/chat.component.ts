import { SignalRService } from './../../../services/data-services/signalR.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  @Input() groupId: number;

  public messages = [] as string[];

  constructor(public signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.startConnection(this.groupId);
    this.signalRService.messageListener.subscribe(data => this.messages.push(data));
  }

  public onSendMessage() {
    this.signalRService.sendMessage('asdasdasd');
  }
}

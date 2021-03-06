import { Message } from './../../interfaces/Message';
import { GroupService } from './../../services/group.service';
import { SignalRService } from './../../services/signalR.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, RequiredValidator } from '@angular/forms';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.sass']
})
export class ChatComponent implements OnInit {
  public messages = [] as Message[];
  public messageForm: FormGroup;

  constructor(
    public signalRService: SignalRService,
    private groupService: GroupService,
    private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.messageForm = this.formBuilder.group({
      content: ['', new RequiredValidator()]
    });
  }

  public setComponentData(groupId: number) {
    this.groupService.loadComments(groupId).subscribe(data => this.messages = data.data);
    this.signalRService.startConnection(groupId);
    this.signalRService.messageListener.subscribe(data => this.messages.push(data));
  }

  public onSendMessage() {
    const content = this.messageForm.controls.content.value;

    this.signalRService.sendMessage(content);

    this.messageForm.controls.content.reset();
  }
}
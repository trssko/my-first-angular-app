import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { MemberListComponent } from '../features/members/member-list/member-list.component';
import { MemberDetailedComponent } from '../features/members/member-detailed/member-detailed.component';
import { ListsComponent } from '../features/lists/lists.component';
import { MessagesComponent } from '../features/messages/messages.component';

export const routes: Routes = [
    {path: "", component: Home},
    {path: "members", component: MemberListComponent},
    {path: "members/:id", component: MemberDetailedComponent},
    {path: "lists", component: ListsComponent},
    {path: "messages", component: MessagesComponent},
    {path: "**", component: Home},
];

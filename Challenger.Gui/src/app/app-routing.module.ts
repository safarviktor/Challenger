import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MychallengesComponent } from './mychallenges/mychallenges.component';
import { ChallengeComponent } from './challenge/challenge.component';

const routes: Routes = [
  { path: '', component: MychallengesComponent },
  { path: 'mychallenges', component: MychallengesComponent },
  { path: 'challenge/:id', component: ChallengeComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

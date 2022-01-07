import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MychallengesComponent } from './mychallenges/mychallenges.component';
import { NavComponent } from './nav/nav.component';
import { TrackComponent } from './track/track.component';
import { ChallengeComponent } from './challenge/challenge.component';
import { KeysPipe } from './shared/KeysPipe';

@NgModule({
  declarations: [
    AppComponent,
    MychallengesComponent,
    NavComponent,
    TrackComponent,
    ChallengeComponent,
    KeysPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { ChallengeOverview } from '../shared/models/ChallengeOverview';
import { ChallengeType } from '../shared/models/ChallengeType';

@Component({
  selector: 'app-mychallenges',
  templateUrl: './mychallenges.component.html',
  styleUrls: ['./mychallenges.component.css']
})
export class MychallengesComponent implements OnInit {

  myChallenges: Array<ChallengeOverview>;

  newChallengeName: string;
  newChallengeType: ChallengeType;
  challengeTypes = ChallengeType;
  
  constructor(private api: ApiService) { }

  ngOnInit() {
    this.loadOverview();
  }

  loadOverview(): void{
    this.api.getMyChallenges().subscribe(data => {
      this.myChallenges = data.Data;
    })
  }

  addChallenge() : void
  {
    if(this.newChallengeName != "" && this.newChallengeType > ChallengeType.NA)
    {
      this.api.addChallenge(this.newChallengeName, this.newChallengeType).subscribe(d => {
        this.loadOverview();
        this.newChallengeName = "";
        this.newChallengeType = ChallengeType.NA;
      })
    }    
  }
}

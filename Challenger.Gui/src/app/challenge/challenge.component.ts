import { Component, OnInit, OnDestroy } from '@angular/core';
import { ApiService } from '../api.service';
import { ChallengeDetail } from '../shared/models/ChallengeDetail';
import { ActivatedRoute } from '@angular/router';
import { TrackSetModel } from '../shared/models/TrackSetModel';

@Component({
  selector: 'app-challenge',
  templateUrl: './challenge.component.html',
  styleUrls: ['./challenge.component.css']
})
export class ChallengeComponent implements OnInit, OnDestroy {

  thisChallenge : ChallengeDetail;
  id: number;
  private sub: any;

  newSetCount: Number = 10;
  newSetDate: Date = new Date();

  constructor(private api: ApiService, private route: ActivatedRoute) { }

  ngOnInit() {    

    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number

      this.loadChallengeDetail();
   });   
  }

  loadChallengeDetail(): void
  {
    this.api.getChallenge(this.id).subscribe(data => {
      this.thisChallenge = data.Data;
    })
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  addNewSet()
  {
    if (this.newSetCount > 0)
    {
      var model = new TrackSetModel();
      model.ChallengeId = this.thisChallenge.Id;
      model.Count = this.newSetCount;
      model.Date = this.newSetDate;
      this.api.addChallengeSet(model).subscribe(d => {
        this.loadChallengeDetail();
      });
    }
  }

  updateDateValue(dateString: string)
  {
    this.newSetDate = new Date(dateString);
  }
}

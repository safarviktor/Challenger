import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TrackSetModel } from './shared/models/TrackSetModel';
import { ChallengeType } from './shared/models/ChallengeType';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private client: HttpClient) { }

  getMyChallenges() 
  {    
    return this.client.get<JsonResult>(`${environment.apiUrl}/api/challenges/list`);
  }

  getChallenge(challengeId: number)
  {
    return this.client.get<JsonResult>(`${environment.apiUrl}/api/challenges/details/${challengeId}`);
  }

  addChallenge(name: string, type: ChallengeType)
  {
    return this.client.post(`${environment.apiUrl}/api/challenges/create`, { name, type });
  }

  addChallengeSet(newSet: TrackSetModel)
  {
    return this.client.post(`${environment.apiUrl}/api/sets/create`, newSet );
  }
}

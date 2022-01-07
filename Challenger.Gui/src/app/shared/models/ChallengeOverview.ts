import { ChallengeType } from './ChallengeType';

export class ChallengeOverview
{
    Id: Number;
    UserId: Number;
    Name: string;
    CurrentTotal: number;
    LastEntry: Date;
    LastEntryCount: number;
    TodayCount: number;
    TodayGoal: number;
    TodayTodo: number;
    TargetTotal: number;
    TargetTotalTodo: number;
    Type: ChallengeType;    
}
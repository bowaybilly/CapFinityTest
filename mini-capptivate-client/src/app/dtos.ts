export interface AssessmentResultDto {
  assessmentInstanceId: number;
  assessmentName: string;
  completeDate?: Date;
  email: string;
  totalScore: number;
}

export interface NewInstanceDto {
  assessmentId: number;
  email: string;
}

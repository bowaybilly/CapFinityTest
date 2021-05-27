export interface Assessment {
  id: number;
  name: string;
  questions: Question[];
}

export interface AssessmentInstance {
  id: number;
  assessmentId: number;
  email: string;
  completedDate?: Date;
  answers: Answer[];
}

export interface Question {
  id: number;
  assessmentId: number;
  text: string;
  questionType: QuestionType;
  choices: Choice[];
  min: number;
  max: number;
  maxLabel: string;
  minLabel: string;
  defaultValue: number;
}

export interface Choice {
  id: number;
  questionId: number;
  text: string;
}

export interface Answer {
  id: number;
  userId: number;
  questionId: number;
  text: string;
  choiceId: number;
  question: Question;
  choice: Choice;
}

export enum QuestionType {
  Text = 0,
  Number = 1,
  Choice = 2,
  Slider = 3,
}

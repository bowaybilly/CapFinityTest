import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AssessmentResultDto, NewInstanceDto } from './dtos';
import { Answer, Assessment, AssessmentInstance } from './models';

@Injectable({
  providedIn: 'root',
})
export class AssessmentService {
  //url can be stored in environment variable to allow for testing
  private assessmentsUrl = 'api/assessments';
  email: string;

  constructor(private http: HttpClient) {}

  getAssessments(): Observable<Assessment[]> {
    return this.http.get<Assessment[]>(this.assessmentsUrl);
  }

  getAssessment(id: number): Observable<Assessment> {
    return this.http.get<Assessment>(`${this.assessmentsUrl}/${id}`);
  }

  getInstance(id: number): Observable<AssessmentInstance> {
    return this.http.get<AssessmentInstance>(
      `${this.assessmentsUrl}/instances/${id}`
    );
  }

  getResults(): Observable<AssessmentResultDto[]> {
    return this.http.get<AssessmentResultDto[]>(
      `${this.assessmentsUrl}/results`
    );
  }

  answerQuestion(answer: Answer) {
    // make sure answer.text is a String, otherwise backend will reject the request
    if (answer.text !== null) {
      answer.text = answer.text.toString();
    }

    return this.http.put(
      `${this.assessmentsUrl}/answerQuestion/${answer.id}`,
      answer
    );
  }

  createAssessmentInstance(newInstanceDto: NewInstanceDto) {
    return this.http.put<AssessmentInstance>(
      `${this.assessmentsUrl}/createInstance`,
      newInstanceDto
    );
  }
}

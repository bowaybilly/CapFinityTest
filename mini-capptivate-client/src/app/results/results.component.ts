import { Component, OnInit } from '@angular/core';
import { AssessmentService } from '../assessment.service';
import { AssessmentResultDto } from '../dtos';
import { Assessment } from '../models';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
})
export class ResultsComponent implements OnInit {
  results: AssessmentResultDto[];
  assessments: Assessment[];

  constructor(private assessmentService: AssessmentService) {}

  ngOnInit() {
    this.assessmentService.getResults().subscribe((results) => {
      this.results = results;
    });
    this.assessmentService.getAssessments().subscribe((assessments) => {
      this.assessments = assessments;
    });
  }
  getQuestionsCount(assessment: AssessmentResultDto): number {
    if (assessment != null) {
      return this.assessments.filter(
        (x) => x.name == assessment.assessmentName
      )[0].questions.length;
    }
  }
}

import { Component, Inject, OnInit } from '@angular/core';
import { Assessment } from '../models';
import { AssessmentService } from '../assessment.service';

@Component({
  selector: 'app-assessments',
  templateUrl: './assessments.component.html',
})
export class AssessmentsComponent implements OnInit {
  public assessments: Assessment[];

  constructor(private assessmentService: AssessmentService) {}

  ngOnInit() {
    this.assessmentService.getAssessments().subscribe((assessments) => {
      this.assessments = assessments;
    });
  }
}

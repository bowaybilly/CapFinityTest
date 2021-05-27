import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AssessmentService } from '../assessment.service';
import { AssessmentInstance, QuestionType } from '../models';

@Component({
  selector: 'app-result-detail',
  templateUrl: './result-detail.component.html',
})
export class ResultDetailComponent implements OnInit {
  instance: AssessmentInstance;
  QuestionType = QuestionType;

  constructor(
    private route: ActivatedRoute,
    private assessmentService: AssessmentService) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.assessmentService.getInstance(id).subscribe(instance => {

      this.instance = instance;
    });

  }
}

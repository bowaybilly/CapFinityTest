import { Component, OnInit, Inject } from '@angular/core';
import { Answer, AssessmentInstance, QuestionType } from '../models';
import { ActivatedRoute, Router } from '@angular/router';
import { AssessmentService } from '../assessment.service';

@Component({
  selector: 'app-assessment-instance',
  templateUrl: './assessment-instance.component.html',
})
export class AssessmentInstanceComponent implements OnInit {
  instance: AssessmentInstance;

  QuestionType = QuestionType;

  currentAnswer: Answer;
  currentAnswerIndex = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private assessmentService: AssessmentService
  ) {}

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.assessmentService.getInstance(id).subscribe((instance) => {
      this.instance = instance;
      this.currentAnswer = this.instance.answers[this.currentAnswerIndex];
    });
  }

  saveAndPrev(answer: Answer) {
    this.assessmentService.answerQuestion(answer).subscribe((_) => {
      this.currentAnswerIndex--;
      this.currentAnswer = this.instance.answers[this.currentAnswerIndex];
    });
  }

  saveAndNext(answer: Answer) {
    this.assessmentService.answerQuestion(answer).subscribe((_) => {
      this.currentAnswerIndex++;
      this.currentAnswer = this.instance.answers[this.currentAnswerIndex];
    });
  }
  onSliderValueChange(value: number, answer: Answer) {
    this.instance.answers[this.currentAnswerIndex].text = value.toString();
  }
  finishAssessment(answer: Answer) {
    if (
      confirm(
        "Are you sure you want to finish the assessment? You won't be able to change your answers by navigating to this URL."
      ) === true
    ) {
      this.assessmentService.answerQuestion(answer).subscribe((_) => {
        this.currentAnswer = this.instance.answers[this.currentAnswerIndex];
      });
      this.router.navigate(['/assessments']);
    }
  }
}

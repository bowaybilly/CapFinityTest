import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AssessmentService } from '../assessment.service';
import { Assessment } from '../models';

@Component({
  selector: 'app-take',
  templateUrl: './take.component.html',
  styleUrls: ['./take.component.css']
})
export class TakeComponent implements OnInit {
  assessment: Assessment;
  email: string;
  errorMsg: string;

  constructor(
    private assessmentService: AssessmentService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.getAssessment();
  }

  getAssessment(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.assessmentService.getAssessment(id).subscribe(assessment => this.assessment = assessment);
  }

  startAssessment(): void {
    this.assessmentService
      .createAssessmentInstance({ assessmentId: this.assessment.id, email: this.email })
      .subscribe(
        response => { this.router.navigate(['/instance', response.id]); },
        error => { this.errorMsg = error.error; });
  }

}

import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AssessmentInstanceComponent } from './assessment-instance/assessment-instance.component';
import { AssessmentsComponent } from './assessments/assessments.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ResultDetailComponent } from './result-detail/result-detail.component';
import { ResultsComponent } from './results/results.component';
import { TakeComponent } from './take/take.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SliderComponent } from './slider/slider.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AssessmentsComponent,
    AssessmentInstanceComponent,
    TakeComponent,
    ResultsComponent,
    ResultDetailComponent,
    SliderComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'assessments', component: AssessmentsComponent },
    { path: 'instance/:id', component: AssessmentInstanceComponent },
    { path: 'take/:id', component: TakeComponent },
    { path: 'results', component: ResultsComponent },
    { path: 'results/:id', component: ResultDetailComponent },
], { relativeLinkResolution: 'legacy' }),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

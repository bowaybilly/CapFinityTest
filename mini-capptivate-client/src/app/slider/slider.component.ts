import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.css'],
})
export class SliderComponent implements OnInit {
  constructor() {}
  @Input() minLabel: string;
  @Input() maxLabel: string;
  @Input() min: number;
  @Input() max: number;
  @Output() onValueChange = new EventEmitter<any>();
  @Input() value: number;
  ngOnInit(): void {}
  valueChanged() {
    this.onValueChange.emit(this.value);
  }
}

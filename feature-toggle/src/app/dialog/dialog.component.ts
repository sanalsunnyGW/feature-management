import { Component, Inject } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatCommonModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { IBusiness } from '../interface/feature.interface';


@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [
    FormsModule,
    MatButtonModule,
    MatCommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
  ],
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.scss'
})



export class DialogComponent {
  businessControl = new FormControl<IBusiness | null>(null, Validators.required);

  constructor(
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { businesses: IBusiness[] }
  ) { }

  onCancel(): void {
    this.dialogRef.close();
  }

  onConfirm(): void {
    if (this.businessControl.valid) {
      this.dialogRef.close(this.businessControl.value);
    }
  }
}

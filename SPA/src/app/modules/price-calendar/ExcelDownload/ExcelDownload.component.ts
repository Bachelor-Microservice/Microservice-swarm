import { Component, OnInit, Inject, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-ExcelDownload',
  templateUrl: './ExcelDownload.component.html',
  styleUrls: ['./ExcelDownload.component.css']
})
export class ExcelDownloadComponent implements OnInit {
  htmlToAdd;
  @ViewChild('download' , {static: false}) myDiv: ElementRef;
  constructor(public dialogRef: MatDialogRef<ExcelDownloadComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private renderer:Renderer2) { }

  ngOnInit() {
    this.htmlToAdd = this.data.html;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onClick() {
      const blob = this.data.data;
      var fileURL = URL.createObjectURL(blob);
      let a = document.createElement("a");
      document.body.appendChild(a);
      a.style.display = "none";
      a.href = fileURL;
      a.target = "_blank";
      a.download = "signal.xlsx";
      a.click();
      a.remove();
  }

}

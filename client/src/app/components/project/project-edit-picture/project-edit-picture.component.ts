import { Component, OnInit } from '@angular/core';
import { BasicComponent } from '../../basic.component';
import { AlertService } from '../../../services/alert.service';
import { ActivatedRoute } from '@angular/router';
import { RequestOptions } from '@angular/http';
import { Picture } from '../../../models/dtos/picture';
import { PictureService } from '../../../services/picture.service';
import { FileUploader, FileUploaderOptions } from 'ng2-file-upload';
import { AppConfig } from '../../../app.config';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'project-edit-picture',
  templateUrl: './project-edit-picture.component.html'
})
export class ProjectEditPictureComponent extends BasicComponent implements OnInit {

  private fileUploader: FileUploader;
  private pictureUrl : string;
  private projectId : string;
  private picture : Picture = new Picture();

  constructor(
    alertService : AlertService,
    private pictureService : PictureService,
    private route : ActivatedRoute,
    private appConfig : AppConfig
  ) {
    super(alertService);
  }
  
  ngOnInit() {
    this.initUploader();

    this.route.paramMap.subscribe(params => {
      this.projectId = params.get('projectId');

      if(this.projectId)
        this.loadPicture(this.projectId);
    });
  }

  loadPicture(projectId) {
    this.pictureUrl = this.appConfig.apiUrl + '/pictures/' + projectId;
    this.picture.projectId = projectId;
  }

  initUploader() {
    let currentAccount = JSON.parse(localStorage.getItem('currentAccount'));
    
    this.fileUploader = new FileUploader(
      <FileUploaderOptions>{
        url: this.appConfig.apiUrl + '/pictures',
        headers: [
          { name: "Authorization", value: 'Bearer ' + currentAccount.token },
          { name: "Accept", value: "application/json" }
        ],
        isHTML5: true,
        // allowedMimeType: ["image/jpeg", "image/png", "application/pdf", "application/msword", "application/zip"]
        allowedFileType: [
          "image"
        ],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 10 * 1024 * 1024
      }
    );

    this.fileUploader.onBuildItemForm = (fileItem, form) => {
      for (const key in this.picture) {
        if (this.picture.hasOwnProperty(key)) {
          form.append(key, this.picture[key]);
        }
      }
    };

    this.fileUploader.onCompleteAll = () => {
      this.alertService.success('success!');
    };

    this.fileUploader.onWhenAddingFileFailed = (item, filter, options) => {
      this.alertService.error('error 1');
    };

    this.fileUploader.onErrorItem = (fileItem, response, status, headers) => {
      this.alertService.error('error 2');
    };

    this.fileUploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        console.log(status);
      }
    };
  }

  getCookie(name: string): string {
    const value = "; " + document.cookie;
    const parts = value.split("; " + name + "=");
    if (parts.length === 2) {
      return decodeURIComponent(parts.pop().split(";").shift());
    }
  }

  save() {

    this.fileUploader.uploadAll();

    // NOTE: Upload multiple files in one request -> https://github.com/valor-software/ng2-file-upload/issues/671
  }
}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { HttpModule } from "@angular/http";

import { AppComponent } from './app.component';
import { AccountComponent } from './components/account/account.component';
import { AlertComponent } from './components/alert/alert.component';
import { ProjectComponent } from './components/project/project.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { MyProjectsComponent } from './components/my-projects/my-projects.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { AutofocusDirective } from './helpers/autofocus.directive';
import { routing } from './app.routing';
import { AccountService } from './services/account.service';
import { AuthenticationService } from './services/authentication.service';
import { AppConfig } from './app.config';
import { AlertService } from './services/alert.service';
import { ProjectViewService } from './services/project-view.service';
import { AuthGuard } from './guards/auth-guard';
import { ProjectEditDataComponent } from './components/project/project-edit-data/project-edit-data.component';
import { ProjectEditPictureComponent } from './components/project/project-edit-picture/project-edit-picture.component';
import { ProjectEditContentComponent } from './components/project/project-edit-content/project-edit-content.component';
import { ZippyComponent } from './components/zippy/zippy.component';
import { ProjectService } from './services/project.service';
import { ProjectEditItemsComponent } from './components/project/project-edit-items/project-edit-items.component';
import { PictureService } from './services/picture.service';
import { ContentService } from './services/content.service';
import { InfoService } from './services/info.service';
import { TinyEditorDirective } from './components/tinyeditor/tiny-editor.component';
import { FileUploadModule } from "ng2-file-upload";
import { ItemService } from './services/item.service';
import { UpDownInputComponent } from './components/up-down-input/up-down-input.component';
import { ProjectInfoComponent } from './components/my-projects/project-info/project-info.component';
import { NeededItemViewComponent } from './components/my-projects/needed-item-view/needed-item-view.component';
import { ProjectViewComponent } from './components/project-view/project-view.component';
import { ProjectThumbnailComponent } from './components/home/project-thumbnail/project-thumbnail.component';
import { ItemViewComponent } from './components/project-view/item-view/item-view.component';
import { PagedViewComponent } from './components/home/paged-view/paged-view.component';

@NgModule({
  declarations: [
    AutofocusDirective,
    AppComponent,
    AccountComponent,
    AlertComponent,
    ProjectComponent,
    HomeComponent,
    LoginComponent,
    MyProjectsComponent,
    NavBarComponent,
    ProjectEditDataComponent,
    ProjectEditPictureComponent,
    ProjectEditContentComponent,
    ZippyComponent,
    ProjectEditItemsComponent,
    TinyEditorDirective,
    UpDownInputComponent,
    ProjectInfoComponent,
    ProjectThumbnailComponent,
    NeededItemViewComponent,
    ProjectViewComponent,
    ItemViewComponent,
    PagedViewComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    FileUploadModule,
    routing
  ],
  providers: [
    AuthGuard,
    ContentService,
    PictureService,
    InfoService,
    AlertService,
    AppConfig,
    AuthenticationService,
    AccountService,
    ProjectService,
    ItemService,
    ProjectViewService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

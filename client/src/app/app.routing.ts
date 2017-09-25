import { ExploreComponent } from './components/explore/explore.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from "./guards/auth-guard";
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { AccountComponent } from './components/account/account.component';
import { ProjectComponent } from './components/project/project.component';
import { MyProjectsComponent } from './components/my-projects/my-projects.component';
import { ProjectViewComponent } from './components/project-view/project-view.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'overview/:projectId', component: ProjectViewComponent },
    { path: 'explore/:filter/:preset', component: ExploreComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: AccountComponent },
    { path: 'project/new', component: ProjectComponent, canActivate: [AuthGuard] },
    { path: 'project/:projectId', component: ProjectComponent, canActivate: [AuthGuard] },
    { path: 'myprojects', component: MyProjectsComponent, canActivate: [AuthGuard] },
    { path: 'account/:accountId', component: AccountComponent, canActivate: [AuthGuard] },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
 
export const routing = RouterModule.forRoot(appRoutes);
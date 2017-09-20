import { AlertService } from '../services/alert.service';
import { ServerError } from '../errors/server-error';

export class BasicComponent {

  protected isNewDataSet : boolean;
  protected loading = false;

  constructor(protected alertService : AlertService) { }

  handleError(error) {
    if(error instanceof ServerError)
      this.alertService.error('An internal server error occured.');
    else if(error.originalError)
      this.alertService.error(error.originalError._body);
    else
      this.alertService.error(error._body);
    this.loading = false;
  }
}

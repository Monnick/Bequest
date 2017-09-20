
import { AppError } from "./app-error";

export class BadGateway extends AppError {

    constructor(originalError? : any) {
        super(originalError);
    }
}
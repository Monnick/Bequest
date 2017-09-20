
import { AppError } from "./app-error";

export class ServerError extends AppError {

    constructor(originalError? : any) {
        super(originalError);
    }
}
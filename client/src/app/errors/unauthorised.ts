
import { AppError } from "./app-error";

export class Unauthorised extends AppError {

    constructor(originalError? : any) {
        super(originalError);
    }
}
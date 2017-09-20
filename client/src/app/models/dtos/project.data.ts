
import { NeededItem } from "./neededItem";
import { Contact } from "./contact";

export class ProjectData {

    id : string;

    title : string;

    contactData : Contact = new Contact();

    category : string;

    initiator : string;

    items: NeededItem[];

    state : number = 0;

    possibleStates : number[] = [ 0 ];

    views : number = 0;

    updatedAt : Date;
}
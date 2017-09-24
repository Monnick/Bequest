import { NeededItem } from './neededItem';
import { Contact } from './contact';

export class ProjectView {
	
	title : string;

	contactData : Contact = new Contact();

	category : string;

	neededItems: NeededItem[];

	content : string;
}
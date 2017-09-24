import { NeededItem } from './neededItem';

export class ProjectInfo {

  id : string;

  title : string;

  category : string;

  state : number;

  views : number;

  updatedAt : Date;
  
  possibleStates : number[] = [ 0 ];

  neededItems : NeededItem[];
}
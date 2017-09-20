
export class State {
  private static _data = [
    { value: 0, text: 'New'},
    { value: 1, text: 'Online'},
    { value: 2, text: 'Finished'},
    { value: 3, text: 'Closed'}
  ];

  static data() : any[] {
    return State._data;
  }

  static newState() : any[] {
    return [
      { value: 0, text: 'New'}
    ];
  }

  static toText(value : number) : string {
    let index = State._data.findIndex(s => s.value == value);

    return State._data[index].text;
  }

  static format(values : number[]) : State[] {
    return State._data.filter(state => {
        return values.findIndex(s => s == state.value) > -1;
    });
  }
}
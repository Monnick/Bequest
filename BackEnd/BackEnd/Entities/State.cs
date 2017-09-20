using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Entities
{
    public enum State
    {
		New = 0,
		Online,
		Finished,
		Closed
    }

	public static class StateMachineHelper
	{
		private static Dictionary<State, IEnumerable<State>> _stateMachine = BuildStateMachineHelper();

		private static Dictionary<State, IEnumerable<State>> BuildStateMachineHelper()
		{
			var machine = new Dictionary<State, IEnumerable<State>>();

			machine.Add(State.New, new []{ State.New, State.Online });
			machine.Add(State.Online, new[] { State.New, State.Online, State.Finished });
			machine.Add(State.Finished, new[] { State.Online, State.Finished, State.Closed });
			machine.Add(State.Closed, new[] { State.Closed });

			return machine;
		}

		public static IEnumerable<State> GetPossibleStates(this State state)
		{
			return _stateMachine[state];
		}
	}
}

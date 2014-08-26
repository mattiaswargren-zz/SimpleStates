// Copyright (c) 2014 Mattias Wargren

using System.Collections.Generic;
using System.Globalization;
using System;

namespace simplestates
{
	public class SimpleStateMachine
	{
		Dictionary<string, SimpleState> states = new Dictionary<string, SimpleState>();

		public SimpleState state {
			get;
			private set;
		}

		public SimpleState this [string pStateName] {
			get {

				if (pStateName == null)
				{
					return null;
				}
				else if (states.ContainsKey(pStateName))
				{
					return states[pStateName];
				}
				else
				{
					throw new NotImplementedException(string.Format("{0} is not defined", pStateName));
				}
			}
			set {
				states[pStateName] = value;
				states[pStateName].name = pStateName;
			}
		}

		public void Change(string pStateName)
		{
			if (state != null)
			{
				state.OnExit();
			}

			state = this[pStateName];

			if (state != null)
			{
				state.OnEnter();
			}
		}

		public void Update()
		{
			if (state != null)
			{
				state.OnUpdate();

				foreach (var transition in state.transitions)
				{
					if (transition.CheckCriteria())
					{
						Change(transition.Next());
					}
				}
			}
		}
	}
}


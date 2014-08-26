// Copyright (c) 2014 Mattias Wargren

using System;

namespace simplestates
{

	public class SimpleStateTransition
	{
		string nextState;
		Func<bool> criteria;

		public SimpleStateTransition(Func<bool> pCriteria, string pNextState)
		{
			nextState = pNextState;
			criteria = pCriteria;
		}

		public bool CheckCriteria()
		{
			return criteria.Invoke();
		}

		public string Next()
		{
			return nextState;
		}
	}


}


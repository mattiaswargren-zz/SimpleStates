using System;

namespace SimpleStates
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


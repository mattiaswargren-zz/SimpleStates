// Copyright (c) 2014 Mattias Wargren

using System;
using System.Collections.Generic;

namespace simplestates
{
	public class SimpleState
	{
		public string name;
		public List<SimpleStateTransition> transitions = new List<SimpleStateTransition>();
		public Action OnEnter = () => { };
		public Action OnUpdate = () => { };
		public Action OnExit = () => { };
	}
}


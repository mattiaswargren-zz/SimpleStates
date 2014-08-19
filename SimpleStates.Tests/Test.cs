// Test.cs
// Copyright (c) 2014 Hello There
//
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace SimpleStates.Tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCreateStateMachine()
		{
			var s = new SimpleStateMachine();
			Assert.IsNull(s.state);
		}

		[Test()]
		public void TestCreateInitialStates()
		{
			var statemachine = new SimpleStateMachine();

			statemachine["BOOT"] = new SimpleState();
			statemachine.Change("BOOT");

			Assert.AreEqual("BOOT", statemachine.state.name);
		}

		[Test()]
		public void TestCreateStatesWithTransitions()
		{
			var elevator = new SimpleStateMachine();
			var floor = 0;

			elevator["UP"] = new SimpleState()
			{
				OnEnter = () => { Console.WriteLine("Elevator is going up!"); },
				OnUpdate = () => { floor++; },
				transitions =
				{
					new SimpleStateTransition(() => floor == 25, "TOP")
				}
			};

			elevator["TOP"] = new SimpleState()
			{
				OnEnter = () => { Console.WriteLine("Elevator is at the top!"); },
			};

			elevator.Change("UP");

			while(elevator.state.name != "TOP")
			{
				elevator.Update();
			}

			Assert.AreEqual(25, floor);

		}

	}
}


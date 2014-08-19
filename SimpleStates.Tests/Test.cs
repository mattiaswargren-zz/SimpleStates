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
			var elevator = new SimpleStateMachine();

			elevator["BOOT"] = new SimpleState()
			{
				OnEnter = () => { Console.WriteLine("Booting..."); }
			};

			elevator.Change("BOOT");

			Assert.AreEqual("BOOT", elevator.state.name);
		}

		[Test()]
		public void TestCreateStatesWithTransitions()
		{
			var elevator = new SimpleStateMachine();
			var floor = 0;

			elevator["UP"] = new SimpleState()
			{
				OnEnter = () => { Console.WriteLine("Going UP"); },
				OnUpdate = () => { floor++; },
				transitions =
				{
					new SimpleStateTransition("TOP", () => floor == 25)
				}
			};

			elevator["TOP"] = new SimpleState()
			{

			};

			elevator.Change("UP");

			for(var i = 0 ; i < 100 ; i++)
			{
				elevator.Update();
			}

			Assert.AreEqual(25, floor);

		}

	}
}


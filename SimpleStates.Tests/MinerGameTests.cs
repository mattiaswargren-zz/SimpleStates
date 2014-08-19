using NUnit.Framework;
using System;

namespace SimpleStates.Tests
{
	[TestFixture()]
	public class MinerGameTests
	{
		[Test()]
		public void TestGame()
		{
			var game = new SimpleStateMachine();

			const string BOOT = "BOOT";
			const string GO_HOME_AND_REST = "GO_HOME_AND_REST";
			const string ENTER_MINE_AND_DIG_FOR_NUGGETS = "ENTER_MINE_AND_DIG_FOR_NUGGETS";
			const string VISIT_BANK_AND_DEPOSIT_GOLD = "VISIT_MINE_AND_DEPOSIT_GOLD";

			var goldInPockets = 0;
			var goldInBank = 0;
			var sleepiness = 0;

			game[BOOT] = new SimpleState()
			{
				OnEnter = () => Console.WriteLine(BOOT),
				transitions = {
					new SimpleStateTransition(ENTER_MINE_AND_DIG_FOR_NUGGETS, () => true)
				}
			};

			game[GO_HOME_AND_REST] = new SimpleState()
			{
				OnEnter = () => Console.WriteLine("Resting"),
				OnUpdate = () => { sleepiness--; },
				transitions = {
					new SimpleStateTransition(ENTER_MINE_AND_DIG_FOR_NUGGETS, () => sleepiness <= 0)
				}
			};

			// ENTER_MINE_AND_DIG_FOR_NUGGETS
			game[ENTER_MINE_AND_DIG_FOR_NUGGETS] = new SimpleState()
			{
				OnEnter = () => Console.WriteLine("Looking for nuggets"),
				OnUpdate = () => { sleepiness++; goldInPockets += new Random().Next(0, 10); },
				transitions = {
					new SimpleStateTransition(GO_HOME_AND_REST, () => sleepiness > 10),
					new SimpleStateTransition(VISIT_BANK_AND_DEPOSIT_GOLD, () => goldInPockets >= 10),
				}
			};

			// VISIT_BANK_AND_DEPOSIT_GOLD
			game[VISIT_BANK_AND_DEPOSIT_GOLD] = new SimpleState()
			{
				OnEnter = () => { Console.WriteLine(string.Format("Depositing {0} gold", goldInPockets)); goldInBank += goldInPockets; goldInPockets = 0; },
				OnUpdate = () => { sleepiness++; },
				transitions = {
					new SimpleStateTransition(ENTER_MINE_AND_DIG_FOR_NUGGETS, () => sleepiness < 10 ),
					new SimpleStateTransition(GO_HOME_AND_REST, () => sleepiness >= 10)
				}
			};

			game.Change(BOOT);

			while(goldInBank < 64)
			{
				game.Update();
			}

		}
	}
}


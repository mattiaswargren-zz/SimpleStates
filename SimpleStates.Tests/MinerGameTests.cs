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
					new SimpleStateTransition(() => true, ENTER_MINE_AND_DIG_FOR_NUGGETS)
				}
			};

			game[GO_HOME_AND_REST] = new SimpleState()
			{
				OnEnter = () => Console.WriteLine("Resting"),
				OnUpdate = () => { sleepiness--; },
				transitions = {
					new SimpleStateTransition(() => sleepiness <= 0, ENTER_MINE_AND_DIG_FOR_NUGGETS)
				}
			};

			// ENTER_MINE_AND_DIG_FOR_NUGGETS
			game[ENTER_MINE_AND_DIG_FOR_NUGGETS] = new SimpleState()
			{
				OnEnter = () => Console.WriteLine("Looking for nuggets"),
				OnUpdate = () => { sleepiness++; goldInPockets += new Random().Next(0, 10); },
				transitions = {
					new SimpleStateTransition(() => sleepiness > 10, GO_HOME_AND_REST),
					new SimpleStateTransition(() => goldInPockets >= 10, VISIT_BANK_AND_DEPOSIT_GOLD),
				}
			};

			// VISIT_BANK_AND_DEPOSIT_GOLD
			game[VISIT_BANK_AND_DEPOSIT_GOLD] = new SimpleState()
			{
				OnEnter = () => { Console.WriteLine(string.Format("Depositing {0} gold", goldInPockets)); goldInBank += goldInPockets; goldInPockets = 0; },
				OnUpdate = () => { sleepiness++; },
				transitions = {
					new SimpleStateTransition(() => sleepiness < 10, ENTER_MINE_AND_DIG_FOR_NUGGETS),
					new SimpleStateTransition(() => sleepiness >= 10, GO_HOME_AND_REST)
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


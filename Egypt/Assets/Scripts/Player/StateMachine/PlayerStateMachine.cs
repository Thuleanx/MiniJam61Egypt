
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx {
	public class PlayerStateMachine
	{
		public PlayerState CurrentState { get; private set; }

		public void Init(PlayerState defaultState) {
			CurrentState = defaultState;
			CurrentState.Enter();
		}

		public void ChangeState(PlayerState newState) {
			CurrentState.Exit();
			CurrentState = newState;
			CurrentState.Enter();
		}
	}
}
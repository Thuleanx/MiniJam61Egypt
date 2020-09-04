
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx {
	public class AnubisStateMachine
	{
		public AnubisState CurrentState { get; private set; }

		public void Init(AnubisState defaultState) {
			CurrentState = defaultState;
			CurrentState.Enter();
		}

		public void ChangeState(AnubisState newState) {
			CurrentState.Exit();
			CurrentState = newState;
			CurrentState.Enter();
		}
	}
}
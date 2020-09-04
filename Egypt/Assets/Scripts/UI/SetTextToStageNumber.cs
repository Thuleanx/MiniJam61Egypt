
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Thuleanx;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SetTextToStageNumber : MonoBehaviour
{
	TMP_Text textComponent;

	static int number = 0;
	bool afterPool;

	void Awake() {
		textComponent = GetComponent<TextMeshProUGUI>();
		number = 0; 
	}

	void OnEnable() {
		if (afterPool) {
			if (number < 5) textComponent.text = "Stage " + number;
			else textComponent.text = "Are you still sane";
			number++;
		}
		afterPool = true;
	}
}

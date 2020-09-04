
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BarFill: MonoBehaviour
{
	Slider slider;	

	public virtual void Awake() {
		slider = GetComponent<Slider>();
	}

	public void SetFill(float fraction) {
		slider.value = Mathf.Clamp01(fraction);
	}
}

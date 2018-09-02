using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayeredText : MonoBehaviour {

	[SerializeField] private string _text;
	public string Text {
		get { return _text; }
		set {
			_text = value;
			updateText();
		}
	}

	private void OnDrawGizmosSelected() {
		updateText();
	}

	private void updateText() {
		foreach(Text t in GetComponentsInChildren<Text>()) {
			t.text = _text;
		}
	}
}

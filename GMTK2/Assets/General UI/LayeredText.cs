using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayeredText : MonoBehaviour {

	public float jiggleInterval, jiggleAmount;

	[SerializeField] private string _text;
	public string Text {
		get { return _text; }
		set {
			_text = value;
			updateText();
		}
	}

	private Dictionary<Text, Vector3> initialPositions = new Dictionary<Text, Vector3>();

	private void Start() {
		foreach(Text t in GetComponentsInChildren<Text>()) {
			initialPositions.Add(t, t.transform.position);
		}
		StartCoroutine(jiggle());
	}

	private void OnDrawGizmosSelected() {
		updateText();
	}

	private void updateText() {
		foreach(Text t in GetComponentsInChildren<Text>()) {
			t.text = _text;
		}
	}

	private IEnumerator jiggle() {
		while(true) {
			yield return new WaitForSeconds(jiggleInterval);
			foreach(Text t in GetComponentsInChildren<Text>()) {
				t.transform.position = initialPositions[t] + Random.Range(-jiggleAmount, jiggleAmount) * Vector3.left;
			}
		}
	}
}

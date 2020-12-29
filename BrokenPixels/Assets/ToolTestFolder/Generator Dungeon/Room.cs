using System;
using UnityEngine;

public abstract class Room : ScriptableObject {
	[Header("Size Room")]
	public int width;
	public int lenght;
	[Header("Type of Room")]
	private Material material;

	private void OnValidate() {
		if (width <= 0) {
			Debug.LogWarning("La stanza non può assumere valore negativi o pari a zero.");
		}
		if (lenght <= 0) {
			Debug.LogWarning("La stanza non può assumere valore negativi o pari a zero.");
		}
	}
}

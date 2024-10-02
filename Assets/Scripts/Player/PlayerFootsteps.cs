using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {
	AudioSource source;
	public AudioClip[] footsteps;
	AudioClip lastClip;

	void Start(){
		source = GetComponent<AudioSource> ();
	}


	public void Step(){
		int i = Random.Range (0, footsteps.Length);

		while (footsteps [i] == lastClip) {
			i = Random.Range (0, footsteps.Length);
		}
			
		source.clip = footsteps [i];
		source.Play ();
		lastClip = footsteps [i];
	}
}

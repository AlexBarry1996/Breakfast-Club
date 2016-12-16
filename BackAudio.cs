using UnityEngine;
using System.Collections;

public class BackAudio : MonoBehaviour {

	public AudioSource source;
	public AudioClip click;

	public void OnClick()
	{
		source.PlayOneShot (click);
	}

}

using UnityEngine;
using System.Collections;

public class HowToPlayAudio : MonoBehaviour {

	public AudioSource source;
	public AudioClip click;

	public void OnClick()
	{
		source.PlayOneShot (click);
	}
}

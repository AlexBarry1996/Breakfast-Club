using UnityEngine;
using System.Collections;

public class AboutUsAudio : MonoBehaviour {

	public AudioSource source;
	public AudioClip click;

	public void OnClick()
	{
		source.PlayOneShot (click);
	}
}

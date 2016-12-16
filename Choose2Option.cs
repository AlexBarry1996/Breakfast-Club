using UnityEngine;
using System.Collections;

public class Choose2Option : MonoBehaviour {

	public AudioSource source;
	public AudioClip click;

	public void OnClick()
	{
		source.PlayOneShot (click);
	}

}

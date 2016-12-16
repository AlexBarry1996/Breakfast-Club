using UnityEngine;
using System.Collections;

public class UIControll : MonoBehaviour {

	public void VolumeControl(float volumeControl)
	{
		GetComponent<AudioSource>().volume = volumeControl;
	}
}

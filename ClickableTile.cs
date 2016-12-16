using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileZ;
	public TileMap map;

	public void OnMouseUp()
	{
		//Debug.Log ("click");

		map.GeneratePathTo (tileX, tileZ);

	}
}

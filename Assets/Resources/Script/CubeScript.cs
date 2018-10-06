using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {


	public Utils.Direction[] avaibleDirections = new Utils.Direction[6];
	public Utils.Direction dir;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setAvaibleDirection() {

		//Vector3 incomingVec = hit.normal - Vector3.up;
		System.Array.Clear (avaibleDirections,0,avaibleDirections.Length -1);

		Vector3 southV = Vector3.back;
		Vector3 northV = Vector3.forward;
		Vector3 upV = Vector3.up;
		Vector3 downV =Vector3.down;
		Vector3 westV = Vector3.left;
		Vector3 eastV = Vector3.right;
		RaycastHit outInfo;

		if (!Physics.Raycast (transform.position, southV,out outInfo)) {
			avaibleDirections[0] = Utils.Direction.South;
		}
		if (!Physics.Raycast (transform.position, northV, out outInfo)) {
			avaibleDirections[1] = Utils.Direction.North;
		}
		if (!Physics.Raycast (transform.position, upV, out outInfo)) {
			avaibleDirections[2] = Utils.Direction.Up;
		}
		if (!Physics.Raycast (transform.position, downV, out outInfo)) {
			avaibleDirections[3] = Utils.Direction.Down;
		}
		if (!Physics.Raycast (transform.position, westV, out outInfo)) {
			avaibleDirections[4] = Utils.Direction.West;
		}
		if (!Physics.Raycast (transform.position, eastV, out outInfo)) {
			avaibleDirections[5] = Utils.Direction.East;
		}
	}
}

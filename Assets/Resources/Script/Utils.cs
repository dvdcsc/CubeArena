using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

	public enum Direction {
		None,
		Up,
		Down,
		East,
		West,
		North,
		South
	}
	public enum Axis {
		X,
		Y,
		Z,
		None
	}

	public Direction getHitDirection(RaycastHit hit) {

		Vector3 incomingVec = hit.normal - Vector3.up;

		Direction[] directions = hit.transform.gameObject.GetComponent<CubeScript> ().avaibleDirections;

		if (hit.transform.gameObject.GetComponent<CubeScript>().avaibleDirections[0] == Utils.Direction.South && 
			V3Equal(incomingVec, new Vector3 (0, -1, -1))) {

			return Direction.South;
		}
		else if (hit.transform.gameObject.GetComponent<CubeScript>().avaibleDirections[1] == Utils.Direction.North && 
			V3Equal(incomingVec, new Vector3 (0, -1, 1))) {
			return Direction.North;
		}
		else if (hit.transform.gameObject.GetComponent<CubeScript>().avaibleDirections[2] == Utils.Direction.Up && 
			V3Equal(incomingVec,new Vector3 (0, 0, 0))) {
			return Direction.Up;
		}
		else if (hit.transform.gameObject.GetComponent<CubeScript>().avaibleDirections[3] == Utils.Direction.Down && 
			V3Equal(incomingVec,new Vector3 (0, -2, 0))) {
			return Direction.Down;
		}
		else if (hit.transform.gameObject.GetComponent<CubeScript>().avaibleDirections[4] == Utils.Direction.West && 
			V3Equal(incomingVec,new Vector3 (-1, -1, 0))) {
			return Direction.West;
		}
		else if (hit.transform.gameObject.GetComponent<CubeScript>().avaibleDirections[5] == Utils.Direction.East && 
			V3Equal(incomingVec,new Vector3 (1, -1, 0))) {
			return Direction.East;
		}
		return Direction.None;
	}

	public bool isFirstOrLast(int val) {

		int max = WorldCreator.size;
		return val==0 || val == max-1;
	}

	public Axis getAxisFromDir(Direction dir) {

		Debug.Log ("real Dir: "+dir);
		Axis axis = Axis.None;
		if (dir.Equals (Direction.South) || dir.Equals (Direction.North)) {
			axis = Axis.Z;
		} else if (dir.Equals (Direction.East) || dir.Equals (Direction.West)) {
			axis = Axis.X;
		} else if (dir.Equals (Direction.Up) || dir.Equals (Direction.Down) ) {
			axis = Axis.Y;
		}

		return axis;
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a - b) < 0.1;
	}
}

using UnityEngine;
using System.Collections;
using System;

public class Move : MonoBehaviour {

	public Vector3 pointA, pointB;
	public float speed = 1;
	public Utils.Direction moveDirection = Utils.Direction.None;

	void Start () {
		pointA = transform.position;
		pointB = transform.position;
		RaycastHit hitdown = new RaycastHit();
		Physics.Raycast(transform.position,Vector3.down, out hitdown);
		setMoveDirection (hitdown);

	}


	void Update () {
		if (Input.GetMouseButtonDown(0)) {

			RaycastHit hitInfo = new RaycastHit();
			Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			CubeScript cubeScript = hitInfo.transform.gameObject.GetComponent<CubeScript>();
//
//			if (cubeScript.hitDirection.Equals (moveDirection)) {
//				pointB = hitInfo.transform.position;
//				pointB.y= transform.position.y;
//				transform.parent = hitInfo.transform;
//			}
		}


		if (!pointA.Equals (pointB)) {
			transform.position = Vector3.MoveTowards (transform.position, pointB, 0.1f);
		} else {
			pointA = transform.position;
		}

	}
		
	public void setMoveDirection(RaycastHit hit) {
		Vector3 incomingVec = hit.normal - Vector3.up;

		if (incomingVec == new Vector3 (0, -1, -1)) {
			moveDirection = Utils.Direction.South;
		}
		else if (incomingVec == new Vector3 (0, -1, 1)) {
			moveDirection = Utils.Direction.North;
		}
		else if (incomingVec == new Vector3 (0, 0, 0)) {
			moveDirection = Utils.Direction.Up;
		}
		else if (incomingVec == new Vector3 (1, 1, 1)) {
			moveDirection = Utils.Direction.Down;
		}
		else if (incomingVec == new Vector3 (-1, -1, 0)) {
			moveDirection = Utils.Direction.West;
		}
		else if (incomingVec == new Vector3 (1, -1, 0)) {
			moveDirection = Utils.Direction.East;
		}
	}
}

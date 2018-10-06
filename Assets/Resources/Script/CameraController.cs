using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

	private Transform target;
	public float speed;

	void Start() {
		speed = 8;
	}

	void Update () {

		if (target) {

			if (Input.GetMouseButton (1)) {
				transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * speed);
				transform.RotateAround(target.position, transform.up, -Input.GetAxis("Mouse X") * speed);
			}
			if (Input.GetAxis("Mouse ScrollWheel") < 0  && Camera.main.orthographicSize < 22) // back
			{
				Camera.main.orthographicSize +=1;


			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 2) // forward
			{
				Camera.main.orthographicSize -=1;
			}

		} else {
			int index = (int) Mathf.Floor ( WorldCreator.size / 2);
			target = GameObject.Find ("RotateCenter_"+index+""+index+""+index).transform;
			transform.LookAt(target);
		}
	}

	public void setTarget(Transform target) {
		this.target = target;
	}
}

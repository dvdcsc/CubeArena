using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using System;

public class Rotate : MonoBehaviour {

	public bool canRotate;

	public Quaternion rot;
	public Quaternion tarRot;

	public Vector3 axis;

	void Start () {
		rot = transform.rotation;
		canRotate = false;	
	}
	
	void Update () {
		
		if(canRotate) {

			rot = transform.rotation;

			transform.rotation= Quaternion.Slerp(transform.rotation, tarRot, 3*Time.deltaTime);
			Quaternion pezza = tarRot;
			pezza.x = -tarRot.x;
			pezza.w = -tarRot.w;

			if (transform.rotation == tarRot || transform.rotation == pezza) {
				canRotate = false;
				for (var i = transform.childCount - 1; i >= 0; i--)
				{
					Transform child = transform.GetChild (i);
					((CubeScript)child.gameObject.GetComponent <CubeScript>()).setAvaibleDirection ();
					child.parent = null;
				}

			}
		}
	}
}

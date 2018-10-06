using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour {

	public CubeSelected c0;
	public CubeSelected c1;

	public Boolean isX = false;
	public Boolean isY = false;
	public Boolean isZ = false;

	Utils utils;
	public bool canSelect = true;
	ArrayList selected = new ArrayList();
	public GameObject rotateCube;
	int clockWise = 0;
	int size;

	void Start () {
		utils = GameObject.Find ("GameController").GetComponent <Utils>();
		size = WorldCreator.size;
		RaycastHit hitted = new RaycastHit ();
		Physics.Raycast (transform.position,Vector3.down, out hitted);
		transform.parent = hitted.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (canSelect && Input.GetMouseButtonDown (0)) {

			RaycastHit hitInfo = new RaycastHit ();

			Boolean hitted = false;
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
				hitted = true;
			}
			if (hitted && hitInfo.transform.gameObject.tag.Equals("Cube")) {

				CubeSelected cubSel = new CubeSelected ();
				cubSel.cube = hitInfo.transform.gameObject;
				cubSel.hitDirection = utils.getHitDirection (hitInfo);

				//**********FoundCenter***********//
				if (selected.Count == 0) {
					
					isX = true;
					isY = true;
					isZ = true;
					selected.Add (cubSel);

					Utils.Direction dir = cubSel.hitDirection;

					switch (utils.getAxisFromDir (dir)) {

						case Utils.Axis.X:
							isX = false;
							break;
						case Utils.Axis.Y:
							isY = false;
							break;
						case Utils.Axis.Z:
							isZ = false;
							break;
					}


					c0 = (CubeSelected)selected [0];
						
				} else if (selected.Count == 1) {

					selected.Add (cubSel);

					if (cubSel.cube.Equals (((CubeSelected)selected [0]).cube) || !cubSel.hitDirection.Equals (((CubeSelected)selected [0]).hitDirection) ) {
						selected = new ArrayList ();
						return;//BUG ripetere selzione 0
					}
					c1 = cubSel;

					float c1xPos = cubSel.cube.transform.position.x;
					float c1yPos = cubSel.cube.transform.position.y;
					float c1zPos = cubSel.cube.transform.position.z;

					float c0xPos = ((CubeSelected)selected [0]).cube.transform.position.x;
					float c0yPos = ((CubeSelected)selected [0]).cube.transform.position.y;
					float c0zPos = ((CubeSelected)selected [0]).cube.transform.position.z;

					int xCor = 100;
					int yCor = 100;
					int zCor = 100;

					if( ( !isZ && Math.Round(c1xPos,0) != Math.Round(c0xPos,0) && Math.Round(c1yPos,0) != Math.Round(c0yPos,0) ) ||
						( !isY && Math.Round(c1xPos,0) != Math.Round(c0xPos,0) && Math.Round(c1zPos,0) != Math.Round(c0zPos,0) ) ||
						( !isX && Math.Round(c1zPos,0) != Math.Round(c0zPos,0) && Math.Round(c1yPos,0) != Math.Round(c0yPos,0) ) 					

					) {
						selected = new ArrayList ();
						return;//BUG ripetere selzione 0
					}

					Debug.Log ("c0xPos: "+Math.Round(c0xPos,0));
					Debug.Log ("c0yPos: "+Math.Round(c0yPos,0));
					Debug.Log ("c0zPos: "+Math.Round(c0zPos,0));

					Debug.Log ("c1xPos: "+Math.Round(c1xPos,0));
					Debug.Log ("c1yPos: "+Math.Round(c1yPos,0));
					Debug.Log ("c1zPos: "+Math.Round(c1zPos,0));

					if (Math.Round(c1xPos,0) != Math.Round(c0xPos,0)) {
						isX = false;
					}  
					if (Math.Round(c1yPos,0) != Math.Round(c0yPos,0)) {
						isY = false;
					}  
					if (Math.Round(c1zPos,0) != Math.Round(c0zPos,0)) {
						isZ = false;
					}

					xCor =(int)Mathf.Floor ((cubSel.cube.transform.position.x / 2.2f)+0.1f);
					yCor = (int)Math.Floor ((cubSel.cube.transform.position.y / 2.2f)+0.1f);
					zCor =(int)Mathf.Floor ((cubSel.cube.transform.position.z / 2.2f)+0.1f);
					GameObject[] allcube = GameObject.FindGameObjectsWithTag ("Cube");
					Vector3 axis = new Vector3 ();

					if (isX) {
						yCor = (int) Mathf.Floor (size/2);
						zCor = (int) Mathf.Floor (size/2);
						rotateCube = GameObject.Find ("RotateCenter_"+xCor + yCor + zCor);
						foreach (GameObject children in allcube) {
							if (Math.Round(children.transform.position.x,0) == Math.Round(rotateCube.transform.position.x,0)) {
								children.transform.parent = rotateCube.transform;		
							}
						}
						if (c1.hitDirection.Equals (Utils.Direction.North) || c1.hitDirection.Equals (Utils.Direction.Up)) {
							if ((Math.Round(c1yPos,0) > Math.Round(c0yPos,0) || Math.Round(c1zPos,0) < Math.Round(c0zPos,0))) {
								clockWise = -1;
								Debug.Log ("-X");

							} else {
								clockWise = 1;
								Debug.Log ("+X");

							}
						} else {
							if ((Math.Round(c1yPos,0) > Math.Round(c0yPos,0) || Math.Round(c1zPos,0) < Math.Round(c0zPos,0))) {
								clockWise = 1;
								Debug.Log ("+X");

							} else {
								Debug.Log ("-X");

								clockWise = -1;
							}
						}

						if (rotateCube != null) {
							
							Quaternion tarRot =  Quaternion.Euler (90 * clockWise, 0, 0) * rotateCube.transform.rotation;
							rotateCube.GetComponent <Rotate> ().tarRot = tarRot;

							rotateCube.GetComponent <Rotate> ().axis = axis;
							canSelect = false;
							rotateCube.GetComponent <Rotate> ().canRotate = true;

						}
					}

					if (isY) {

						xCor = (int) Mathf.Floor (size/2);
						zCor = (int) Mathf.Floor (size/2);
						rotateCube = GameObject.Find ("RotateCenter_"+xCor + yCor + zCor);
						foreach (GameObject children in allcube) {
							if (Math.Round(children.transform.position.y) == Math.Round(rotateCube.transform.position.y)) {
								children.transform.parent = rotateCube.transform;		
							}
						}

						Debug.Log ("hitDirection: "+c1.hitDirection);

						if (c1.hitDirection.Equals (Utils.Direction.South) || c1.hitDirection.Equals (Utils.Direction.East)) {
							if (Math.Round(c1xPos,0) > Math.Round(c0xPos,0) || Math.Round(c1zPos,0) > Math.Round(c0zPos,0)) {
								axis = Vector3.down;
								clockWise = -1;
								Debug.Log ("-Y");
							} else {
								axis = Vector3.up;
								Debug.Log ("+Y");
								clockWise = 1;
							}
						} else {

							if (Math.Round(c1xPos,0) > Math.Round(c0xPos,0) || Math.Round(c1zPos,0) > Math.Round(c0zPos,0)) {
								clockWise = 1;
								axis = Vector3.up;
								Debug.Log ("+Y");

							} else {
								Debug.Log ("-Y");

								axis = Vector3.down;
								clockWise = -1;

							}
						}

						if (rotateCube != null) {
					
							Quaternion tarRot = Quaternion.Euler (0, 90 * clockWise, 0) * rotateCube.transform.rotation;
							rotateCube.GetComponent <Rotate> ().tarRot = tarRot;
							rotateCube.GetComponent <Rotate> ().axis = axis;
							rotateCube.GetComponent <Rotate> ().canRotate = true;
							canSelect = false;
						}
					}

					if (isZ) {

						xCor = (int) Mathf.Floor (size/2);
						yCor = (int) Mathf.Floor (size/2);
						rotateCube = GameObject.Find ("RotateCenter_"+xCor + yCor + zCor);
						foreach (GameObject children in allcube) {
							if (Math.Round(children.transform.position.z) == Math.Round(rotateCube.transform.position.z)) {
								children.transform.parent = rotateCube.transform;		
							}
						}

						if (c1.hitDirection.Equals (Utils.Direction.Down) || c1.hitDirection.Equals (Utils.Direction.East)) {
							if (Math.Round(c1xPos,0) > Math.Round(c0xPos,0) || Math.Round(c1yPos,0) > Math.Round(c0yPos,0)) {
								Debug.Log ("+Z");
								clockWise = 1;
							} else {
								Debug.Log ("-Z");
								clockWise = -1;
							}
						} else {
							if (Math.Round(c1xPos,0) > Math.Round(c0xPos,0) || Math.Round(c1yPos,0) > Math.Round(c0yPos,0)) {
								clockWise = -1;
								Debug.Log ("-Z");
							} else {
								clockWise = 1;
								Debug.Log ("+Z");

							}
						}

						if (rotateCube != null) {

							Quaternion tarRot = Quaternion.Euler (0, 0, 90 * clockWise) * rotateCube.transform.rotation;
							rotateCube.GetComponent <Rotate> ().tarRot = tarRot;
							rotateCube.GetComponent <Rotate> ().axis = axis;
							rotateCube.GetComponent <Rotate> ().canRotate = true;
							canSelect = false;
						}
					}

					selected = new ArrayList ();
					c0 = null;
					c1 = null;

				}

			}

		} else if (!canSelect && rotateCube != null) {
			if (!rotateCube.GetComponent <Rotate> ().canRotate) {
				canSelect = true;
				rotateCube = null;
			}

		}

	}


	[Serializable]
	public class CubeSelected {

		public GameObject cube;
		public Utils.Direction hitDirection;
	}
}
using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System;

public class WorldCreator : MonoBehaviour {

	public static int size;
	public int dim;
	public float dis;
	public float lightDist;
	public float lightIntensity;
	public float lightRange;

	Utils utils;
	void Start () {

		size = 5;
		dim = 2;
		dis = 0.2f;
		lightDist = 9f;
		lightIntensity = 5.5f;
		lightRange = 18f;
		utils = GameObject.Find ("GameController").GetComponent <Utils> ();
		for(int i=0; i<size; i++) {
			for(int j=0; j<size; j++) {
				for(int k=0; k<size; k++) {

					//Guscio
					if (isCube(i,j,k)) {
						GameObject go = (GameObject)Instantiate(Resources.Load("Prefab/Cube"));

						go.transform.position = new Vector3 (i*(dim+dis),j*(dim+dis),k*(dim+dis));
						go.name = "Cube_"+i+j+k;
						go.tag = "Cube";
						go.GetComponent<Renderer>().material.color = new Color(0.3f,0.6f,0.1f);


						//CENTRI
						if (isCenter(i,j,k)) {

							GameObject light = new GameObject ();
							light.name = "Light_"+i+j+k;
							light.AddComponent<Light> ();
							light.GetComponent <Light>().intensity=lightIntensity;
							light.GetComponent <Light>().range=lightRange;

							light.transform.position = go.transform.position;

							if (i == 0) {
								light.transform.position = new Vector3 (light.transform.position.x-lightDist,light.transform.position.y,light.transform.position.z);
							} else if (i == size -1) {
								light.transform.position = new Vector3 (light.transform.position.x+lightDist,light.transform.position.y,light.transform.position.z);
							} else if (j == 0) {
								light.transform.position = new Vector3 (light.transform.position.x,light.transform.position.y-lightDist,light.transform.position.z);
							} else if (j == size -1) {
								light.transform.position = new Vector3 (light.transform.position.x,light.transform.position.y+lightDist,light.transform.position.z);
							} else if (k == 0) {
								light.transform.position = new Vector3 (light.transform.position.x,light.transform.position.y,light.transform.position.z-lightDist);
							} else if (k == size -1) {
								light.transform.position = new Vector3 (light.transform.position.x,light.transform.position.y,light.transform.position.z+lightDist);
							}
							go.name = "Center_"+i+j+k;;
							go.GetComponent<Renderer>().material.color = new Color(1,0.5f,0.1f);
						} 

						//VERTICI
						else if(isVertex(i,j,k)) {
							go.name = "Vertex_"+i+j+k;
							go.GetComponent<Renderer>().material.color = new Color(0.6f,0.1f,0.3f);
						}

						//SPIGOLI
						else if(isCorner(i,j,k)) {
							go.name = "Corner_"+i+j+k;;
							go.GetComponent<Renderer>().material.color = new Color(1.5f,0,0);

						}

						if (go.transform.name.StartsWith ("Cube") || go.transform.name.StartsWith ("Center")) {

							switch (getCubeInitialDirection (i,j,k)) {

							case Utils.Direction.North:
								
								//Up Wall
								createWall (new Vector3(0,1.1f,2f), new Vector3(2f,  0.2f, 2f), go);

								//Down Wall
								createWall (new Vector3(0,-1.1f,2f), new Vector3(2f,  0.2f, 2f), go);

								//West Wall
								createWall (new Vector3(1.1f,0,2f), new Vector3(0.2f, 2f, 2f), go);

								//East Wall
								createWall (new Vector3(-1.1f,0,2f), new Vector3(0.2f, 2f, 2f), go);
								break;

							case Utils.Direction.South:

								//Up Wall
								createWall (new Vector3(0,1.1f,-2f), new Vector3(2f,  0.2f, 2f), go);

								//Down Wall
								createWall (new Vector3(0,-1.1f,-2f), new Vector3(2f,  0.2f, 2f), go);

								//West Wall
								createWall (new Vector3(1.1f,0,-2f), new Vector3(0.2f, 2f, 2f), go);

								//East Wall
								createWall (new Vector3(-1.1f,0,-2f), new Vector3(0.2f, 2f, 2f), go);
								break;

							case Utils.Direction.West:
								
								//North Wall
								createWall (new Vector3(-2f,0,1.1f),new Vector3(2,2,0.2f), go);

								//South Wall
								createWall (new Vector3(-2f,0,-1.1f),new Vector3( 2,2,0.2f), go);

								//Up Wall
								createWall (new Vector3(-2f,1.1f,0), new Vector3(2,0.2f,2), go);

								//Down Wall
								createWall (new Vector3(-2f,-1.1f,0), new Vector3(2,0.2f,2), go);

								break;

							case Utils.Direction.East:

								//North Wall
								createWall (new Vector3(2f,0,1.1f),new Vector3(2,2,0.2f), go);

								//South Wall
								createWall (new Vector3(2f,0,-1.1f),new Vector3( 2,2,0.2f), go);

								//Up Wall
								createWall (new Vector3(2f,1.1f,0), new Vector3(2,0.2f,2), go);

								//Down Wall
								createWall (new Vector3(2f,-1.1f,0), new Vector3(2,0.2f,2), go);

								break;

							case Utils.Direction.Up:

								//North Wall
								createWall (new Vector3(0,2f,1.1f), new Vector3(2f, 2f, 0.2f), go);

								//South Wall
								createWall (new Vector3(0,2f,-1.1f), new Vector3(2f, 2f, 0.2f), go);

								//West Wall
								createWall (new Vector3(1.1f,2f,0), new Vector3(0.2f, 2f, 2f), go);

								//East Wall
								createWall (new Vector3(-1.1f,2f,0), new Vector3(0.2f, 2f, 2f), go);

								break;
							case Utils.Direction.Down:

								//North Wall
								createWall (new Vector3(0,-2f,1.1f), new Vector3(2f, 2f, 0.2f), go);

								//South Wall
								createWall (new Vector3(0,-2f,-1.1f), new Vector3(2f, 2f, 0.2f), go);

								//West Wall
								createWall (new Vector3(1.1f,-2f,0), new Vector3(0.2f, 2f, 2f), go);

								//East Wall
								createWall (new Vector3(-1.1f,-2f,0), new Vector3(0.2f, 2f, 2f), go);
								break;
							}
						

						}
					}

					//Scheletro
					if (isRotateCenter(i,j,k)) {
						GameObject go = (GameObject)Instantiate(Resources.Load("Prefab/RotateCenter"));
						go.transform.position = new Vector3 (i*(dim+dis),j*(dim+dis),k*(dim+dis));
						go.name = "RotateCenter_"+i+j+k;
						go.tag = "Center";

					}
				}
			}
		}

		GameObject[] cubes = GameObject.FindGameObjectsWithTag ("Cube");
		foreach(GameObject cube in cubes) {
			cube.GetComponent <CubeScript> ().setAvaibleDirection ();

		}

	}

	private void createWall(Vector3 pos,Vector3 scale,GameObject parent) {

		int diceRoll = UnityEngine.Random.Range (0, 10);
		if (diceRoll > 0) {
			return;
		}

		Collider[] colliders =  Physics.OverlapSphere(new Vector3(parent.transform.position.x +pos.x,parent.transform.position.y+pos.y, parent.transform.position.z +pos.z), 0.1f);
		if(colliders.Length > 0) {
			return;
		}

		GameObject wall = (GameObject)Instantiate (Resources.Load ("Prefab/Wall"));
		wall.GetComponent<Renderer>().material.color = new Color(0.5f,0.2f,0);
		wall.tag = "Wall";
		wall.transform.localScale = new Vector3 (scale.x, scale.y, scale.z);
		wall.transform.position = new Vector3 (parent.transform.position.x +pos.x, parent.transform.position.y+pos.y, parent.transform.position.z +pos.z);
		wall.transform.parent = parent.transform; //TODO move to Rotate script
	}

	public Utils.Direction getCubeInitialDirection(int i,int j,int k) {

		if (i == 0) {
			return Utils.Direction.West;
		} else if (i == size -1) {
			return Utils.Direction.East;
		} else if (j == 0) {
			return Utils.Direction.Down;
		} else if (j == size -1) {
			return Utils.Direction.Up;
		} else if (k == 0) {
			return Utils.Direction.South;
		} else if (k == size -1) {
			return Utils.Direction.North;
		}

		return Utils.Direction.None;
	}


	public bool isCube(int i,int j,int k) {
		if(utils.isFirstOrLast(i) || utils.isFirstOrLast(j) || utils.isFirstOrLast(k)) {
			return true;
		}
		return false;
	}

	public bool isCenter(int i,int j,int k) {
		if(utils.isFirstOrLast(i) && j== Mathf.Floor (size/2) && k== Mathf.Floor (size/2) ||
			utils.isFirstOrLast(j) && i== Mathf.Floor (size/2) && k== Mathf.Floor (size/2) ||
			utils.isFirstOrLast(k) && j== Mathf.Floor (size/2) && i== Mathf.Floor (size/2)) {
			return true;
		}
		return false;
	}

	public bool isVertex(int i,int j,int k) {
		if(utils.isFirstOrLast (i) && utils.isFirstOrLast(j) && utils.isFirstOrLast(k)) {
			return true;
		}
		return false;
	}

	public bool isCorner(int i,int j,int k) {
		if((utils.isFirstOrLast(i) && utils.isFirstOrLast(j)) ||
			(utils.isFirstOrLast(k) && utils.isFirstOrLast(j)) ||
			(utils.isFirstOrLast(i) && utils.isFirstOrLast(k))) {

			return true;
		}
		return false;
	}

	public bool isRotateCenter(int i,int j,int k) {
		if(j== Mathf.Floor (size/2) && k== Mathf.Floor (size/2) ||
			i== Mathf.Floor (size/2) && k== Mathf.Floor (size/2) ||
			j== Mathf.Floor (size/2) && i== Mathf.Floor (size/2)) {

			return true;
		}
		return false;
	}



}
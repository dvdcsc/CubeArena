using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Spawn : MonoBehaviour {

	private Utils utils;
	private Select selectScript;
	public bool spawned;
	public bool placed;
	private GameObject player;

	void Start () {
		utils = GameObject.Find ("GameController").GetComponent <Utils>();
		selectScript = GameObject.Find ("GameController").GetComponent <Select>();
		spawned = false;
		placed = false;

	}
	
	void Update () {
	
		if (Input.GetMouseButtonDown (0) && !spawned) {

			RaycastHit hitInfo = new RaycastHit ();
			bool hitted = false;

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
				hitted = true;
			}
			if (hitted && hitInfo.transform.gameObject.tag.Equals ("Cube")) {


				player = (GameObject)Instantiate (Resources.Load ("Robot Kyle/Model/RobotKyle"));

				CubeSelected cubSel = new CubeSelected ();
				cubSel.cube = hitInfo.transform.gameObject;
				cubSel.hitDirection = utils.getHitDirection (hitInfo);

				Utils.Direction dir = cubSel.hitDirection;
				player.transform.position = cubSel.cube.transform.position;

				switch (dir) {

				case Utils.Direction.North:

					player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 1.1f);
					player.transform.rotation = Quaternion.Euler (90, 0, 0) * player.transform.rotation;

					break;

				case Utils.Direction.South:

					player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 1.1f);
					player.transform.rotation = Quaternion.Euler (-90, 0, 0) * player.transform.rotation;
					break;

				case Utils.Direction.West:

					player.transform.position = new Vector3 (player.transform.position.x - 1.1f, player.transform.position.y, player.transform.position.z);
					player.transform.rotation = Quaternion.Euler (0, 0, 90) * player.transform.rotation;

					break;

				case Utils.Direction.East:

					player.transform.position = new Vector3 (player.transform.position.x + 1.1f, player.transform.position.y, player.transform.position.z);
					player.transform.rotation = Quaternion.Euler (0, 0, -90) * player.transform.rotation;

					break;

				case Utils.Direction.Up:


					player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 1.1f, player.transform.position.z);
					break;
				case Utils.Direction.Down:

					player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - 1.1f, player.transform.position.z);
					player.transform.rotation = Quaternion.Euler (180, 0, 0) * player.transform.rotation;

					break;
				}

				player.transform.parent = cubSel.cube.transform;
				spawned = true;

			}

		} else if (Input.GetKeyDown (KeyCode.R) && spawned) {

			player.transform.rotation = player.transform.rotation*Quaternion.Euler (0, 90, 0);

		} else if (Input.GetKeyDown (KeyCode.V) && spawned) {

			placed = true;

		} else if (placed) {
			selectScript.enabled = true;
			this.enabled = false;
		}
	}


}

[Serializable]
public class CubeSelected {

	public GameObject cube;
	public Utils.Direction hitDirection;
}
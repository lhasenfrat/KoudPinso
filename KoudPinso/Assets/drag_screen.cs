using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_screen : MonoBehaviour {

	public int minX = 0; //The minimum 'x' position of camera (up-down scrolling)
	public int maxX = 10; //The maximum 'x' position of camera
	public int minY = 0; //The minimum 'y' position of camera (right-left scrolling)
	public int maxY = 10; //The maximum 'y' position of camera 
	bool canScroll; //Check if it can scroll
	Vector3 firstMousePosition; //The position of the mouse at the begining

	void Update () {

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); //Mouse position every frame
		float x = transform.position.x; //Camera x position
		float y = transform.position.y; //Camera y position
		float z = transform.position.z; //Camera z position

		if (Input.GetMouseButtonDown (0)) {
			firstMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			canScroll = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			canScroll = false;
		}

		if (canScroll) {
			transform.position = new Vector3 (x, y + firstMousePosition.y - mousePos.y, z); //Make the scroll movement
		}

		if (transform.position.y > minX) { //Check for the minimum scroll limit
			transform.position = new Vector3 (x, minX, z);
		}
		if (transform.position.y < -maxX) { //Check for the maximum scroll limit
			transform.position = new Vector3 (x, -maxX, z);
		}

	}
}

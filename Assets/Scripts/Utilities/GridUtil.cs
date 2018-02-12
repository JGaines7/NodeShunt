using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtil {

	const int SPACING = 1;

	public static Vector3 posToGridPos(Vector3 pos)
	{
		pos.x = SPACING * Mathf.Round (pos.x / SPACING);
		pos.y = SPACING * Mathf.Round (pos.y / SPACING);
		pos.z = SPACING * Mathf.Round (pos.z / SPACING);

		return pos;

	}
		
	public static GameObject getGameObjectAtCursor (Camera camera)
	{
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, 1000)) {
			return hit.transform.gameObject;
		} else
			return null;
	}

}

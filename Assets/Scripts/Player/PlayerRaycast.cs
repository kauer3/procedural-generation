using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {

	Transform rayStart;
	public float maxTargetDistance = 20f;

	[HideInInspector]
	public float targetDist;

	public bool showDebug;
	public Color hitColour_Debug = Color.green;

	RaycastHit hit;
	Transform rayCastHitPoint;

	[HideInInspector]
	public bool hitSomething;
	[HideInInspector]
	public string targetName, targetTag;

	// Use this for initialization
	void Start () {
		rayStart = GameObject.Find ("RayStart").transform;
		rayCastHitPoint = GameObject.Find ("RayHitPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
		FindTarget ();
	}

	void FindTarget(){
		if (Physics.Raycast (rayStart.position, rayStart.forward, out hit, maxTargetDistance)) {
			hitSomething = true;
			rayCastHitPoint.position = hit.point;
			targetName = hit.transform.name;
			targetTag = hit.transform.tag;
			//Calculate distance to target (from rayStart)
			targetDist = Vector3.Distance (rayStart.position, hit.point);
		} else {
			hitSomething = false;
			rayCastHitPoint.position = rayStart.position + rayStart.forward * maxTargetDistance;
			targetName = null;
			targetTag = null;
			targetDist = maxTargetDistance;
		}
	}

	void OnDrawGizmos() {
		if (showDebug && rayStart != null) {
			if (hitSomething) {
				Gizmos.color = hitColour_Debug;
				Gizmos.DrawLine (rayStart.position, hit.point);
			} else {
				Gizmos.color = Color.white;
				Gizmos.DrawRay (rayStart.position, rayStart.forward * maxTargetDistance);
			}
		}
	}
}

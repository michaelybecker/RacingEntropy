using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public TileManager tiles;
	public float yHeight;
	
	public LayerMask castMask = 256;
	
	public float minZoom = 2f;
	public float maxZoom = 50f;
	public float initialZoom = 5f;
	
	private float currentZoom;
	private float targetZoom;
	
	public float zoomSnap = 0.5f;
	public float zoomScale = 0.6f;
	public float zoomCurving = 2f;

	public bool Orthographic = true;

	public void Awake () {

	}
	
	public void Init()
	{
		yHeight = 10f;
		int centerX = (int)(tiles.getTile.GetLength (0) / 2);
		int centerY = (int)(tiles.getTile.GetLength (1) / 2);
		
		Vector3 LookHere = tiles.objectFromTile [tiles.getTile[centerX,centerY]].transform.position;
		//LookHere = Camera.main.ScreenToWorldPoint(LookHere); 
		
		transform.position = new Vector3 (centerX, yHeight, tiles.boardSize.y);
		transform.LookAt (LookHere);
		
		currentZoom = initialZoom;
		targetZoom = ((float)tiles.getTile.GetLength (0))/2f;
	}
	
	public void OnGUI () 
	{
		// If we're dragging the right mouse button,
		if (Event.current.type == EventType.MouseDrag && Event.current.button == 1) 
		{
			// ... then check how far the mouse has been dragged.
			Vector2 origin = Event.current.mousePosition;
			origin.x -= Event.current.delta.x;
			origin.y += Event.current.delta.y;
			RaycastHit origin3D;
			RaycastHit current3D;
			
			if (!Physics.Raycast (Camera.main.ScreenPointToRay(origin), out origin3D, Mathf.Infinity, castMask))
				return; // If we missed the background plane somehow, then quit trying to process the impossible.
			
			if (!Physics.Raycast (Camera.main.ScreenPointToRay(Event.current.mousePosition), out current3D, Mathf.Infinity, castMask))
				return; // If we missed the background plane somehow, then quit trying to process the impossible.
			
			Vector3 movement = origin3D.point - current3D.point;
			
			transform.Translate (movement, Space.World);
		}
		
		if (Event.current.type == EventType.ScrollWheel) {
			targetZoom += Event.current.delta.y;

			// Zoom more if you are zoomed out and less if you are zoomed in.  Beware: magic number here!
			targetZoom += Event.current.delta.y*(zoomCurving*((currentZoom-minZoom) / (maxZoom-minZoom))-0.75f);

			if (targetZoom > maxZoom)
				targetZoom = maxZoom;
			if (targetZoom < minZoom)
				targetZoom = minZoom;
		}
	}
	
	public void Update () 
	{
		float oldZoom = currentZoom; // Need to track this.
		float newZoom = Mathf.Lerp(currentZoom, targetZoom, zoomSnap);
		float shift = currentZoom - newZoom;
		currentZoom = newZoom;

		if (Camera.main.orthographic) {
			// Basic orthographic zooming.
			Camera.main.orthographicSize = currentZoom;

			// Displace camera position towards or away from mouse position.
			float ecks = Input.mousePosition.x/Screen.width;
			ecks -= 1f-ecks;
			ecks *= Camera.main.aspect;
			float why = Input.mousePosition.y/Screen.height;
			why -= 1f-why;
			Vector3 movement = new Vector3(ecks*shift, why*shift, 0);
			Camera.main.transform.Translate(movement, Space.Self);
		} else {
			// Old perspective-based movement
			transform.Translate (new Vector3(0, 0, shift*zoomScale), Space.Self);
		}
	}
}

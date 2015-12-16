using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public TileManager tiles;
	public float yHeight;
	
	public LayerMask castMask = 256;
	
	public float minZoom = -10f;
	public float maxZoom = 50f;
	public float initialZoom = 5f;
	
	private float currentZoom;
	private float targetZoom;
	
	public float zoomSnap = 0.5f;
	public float zoomScale = 0.6f;
	
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
		//Android zoom
		if (Input.touchCount >= 2)
		{
			Vector2 touch0, touch1;
			float distance;
			touch0 = Input.GetTouch(0).position;
			touch1 = Input.GetTouch(1).position;
			distance = Vector2.Distance(touch0, touch1);
			targetZoom += distance;
			if (targetZoom > maxZoom)
				targetZoom = maxZoom;
			if (targetZoom < minZoom)
				targetZoom = minZoom;
		}
		else if(Input.touchCount > 0)
		{
			// ... then check how far the mouse has been dragged.
			Debug.Log("Dragging");
			Vector2 origin = Input.GetTouch(0).position;
			origin.x -= Input.GetTouch(0).deltaPosition.x;
			origin.y += Input.GetTouch(0).deltaPosition.y;
			RaycastHit origin3D;
			RaycastHit current3D;
			
			if (!Physics.Raycast (Camera.main.ScreenPointToRay(origin), out origin3D, Mathf.Infinity, castMask))
				return; // If we missed the background plane somehow, then quit trying to process the impossible.
			
			if (!Physics.Raycast (Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out current3D, Mathf.Infinity, castMask))
				return; // If we missed the background plane somehow, then quit trying to process the impossible.
			
			Vector3 movement = origin3D.point - current3D.point;
			
			transform.Translate (movement, Space.World);
		}
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

		//PC scroll
		if (Event.current.type == EventType.ScrollWheel) 
		{
			targetZoom += Event.current.delta.y;
			if (targetZoom > maxZoom)
				targetZoom = maxZoom;
			if (targetZoom < minZoom)
				targetZoom = minZoom;
		}
	}
	
	public void Update () 
	{
		float newZoom = Mathf.Lerp(currentZoom, targetZoom, zoomSnap);
		float shift = currentZoom - newZoom;
		currentZoom = newZoom;
		transform.Translate (new Vector3(0, 0, shift*zoomScale), Space.Self);
	}
}
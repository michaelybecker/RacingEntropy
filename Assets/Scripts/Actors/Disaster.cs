using UnityEngine;
using System.Collections;

public class Disaster : MonoBehaviour {

	//type of object
	public int type = -1;
	//current tile the disaster is on
	public Tile currentTile;
	//tile manager
	public TileManager manager;

	//actions to take when a turn is processed
	public void Turn () 
	{
		if (currentTile == null || type == -1)
			return;
		switch (type) 
		{
			case 0:
				manager.cascade.OnThunder(currentTile);
				break;
			case 1:
				manager.cascade.OnEruption(currentTile);
				break;
			case 2:
				manager.cascade.OnFlood(currentTile);
				break;
			case 3:
				manager.cascade.OnQuake(currentTile);
				break;
		}
	}

	//Creates a new disaster with a random type
	public void StartDisaster (Tile t) 
	{
		if(t == null) return;
		type = Random.Range(0, 4);
		StartDisaster (t, type);
	}

	//Start a new disaster with a set type
	public void StartDisaster (Tile t, int newType) 
	{
		if(t == null) return;
		type = newType;
		currentTile = t;

		//Create the mesh and material and add them to the object
		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

		filter.mesh = Resource.disasterMesh [type];
		renderer.material = Resource.disasterMaterial [type];

		//add some particels for effect
		GameObject particles = (GameObject)Instantiate (Resource.disasterParticles [type]);
		particles.transform.parent = transform;

		//position it just above the tile
		transform.position = new Vector3 (
				manager.objectFromTile[currentTile].transform.position.x,
				manager.objectFromTile[currentTile].transform.position.y+manager.worldScale.y, 
				manager.objectFromTile[currentTile].transform.position.z);

		particles.transform.position = new Vector3(
			transform.position.x + Resource.disasterParticleOffset[type].x,
			transform.position.y + Resource.disasterParticleOffset[type].y,
			transform.position.z + Resource.disasterParticleOffset[type].z
			);

		transform.parent = manager.transform;
	}

	//Destroy this object and any trace that had once existed
	public void Kill()
	{
		manager.disasters.Remove (this);
		DestroyImmediate (gameObject);
	}
}

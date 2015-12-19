using UnityEngine;
using System.Collections;

public class Disaster : MonoBehaviour {

	public int type = -1;
	public Tile currentTile;
	public TileManager manager;

	public void Turn () {
		if (currentTile == null || type == -1)
			return;
		switch (type) {
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

	public void StartDisaster (Tile t) 
	{
		type = Random.Range(0, 4);
		StartDisaster (t, type);
	}

	public void StartDisaster (Tile t, int newType) 
	{
		type = newType;
		currentTile = t;

		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

		filter.mesh = Resource.disasterMesh [type];
		renderer.material = Resource.disasterMaterial [type];

		GameObject particles = (GameObject)Instantiate (Resource.disasterParticles [type]);
		particles.transform.parent = transform;

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
	public void Kill()
	{
		manager.disasters.Remove (this);
		DestroyImmediate (gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class Disaster : MonoBehaviour {

	public int type = -1;
	public int x = -1;
	public int y = -1;
	public TileManager manager;

	public void Turn () {
		if (x == -1 || y == -1 || type == -1)
			return;
		switch (type) {
			case 0:
				manager.cascade.OnThunder(manager.getTile[x,y]);
				break;
			case 1:
				manager.cascade.OnEruption(manager.getTile[x,y]);
				break;
			case 2:
				manager.cascade.OnFlood(manager.getTile[x,y]);
				break;
			case 3:
				manager.cascade.OnQuake(manager.getTile[x,y]);
				break;
		}
	}

}

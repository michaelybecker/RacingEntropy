using UnityEngine;
using System.Collections;

public static class Resource
{
	//Meshes for the different Tiles
	public static Mesh[] tileMesh = new Mesh[]{
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh ))
		/*(Mesh)Resources.Load(("Tiles/Marsh"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Forest"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Lake"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Mountain"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Plain"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Crags"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Goal"), typeof( Mesh ))*/
	};

	public static Material[] tileMaterial = new Material[]{
		(Material)Resources.Load (("Tiles/Materials/Desert"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Marsh"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Forest"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Lake"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Mountain"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Plain"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Crags"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Goal"), typeof(Material))
	};
}


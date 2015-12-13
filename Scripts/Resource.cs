using UnityEngine;
using System.Collections;

public static class Resource
{
	public static Mesh plantMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));
	public static Mesh fireMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));

	public static Mesh baseMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));

	//Meshes for the different Tiles
	public static Mesh[] tileMesh = new Mesh[]{
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Forest"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Water"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Mountain"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Crags"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Goal"), typeof( Mesh ))
		/*(Mesh)Resources.Load(("Tiles/Marsh"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Forest"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Lake"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Mountain"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Plain"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Crags"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Goal"), typeof( Mesh ))*/
	};

	public static Material plantMaterial = (Material)Resources.Load (("Tiles/Materials/Plant"), typeof(Material));
	public static Material fireMaterial = (Material)Resources.Load (("Tiles/Materials/Fire"), typeof(Material));

	public static Material[] tileMaterial = new Material[]{
		(Material)Resources.Load (("Tiles/Materials/Desert"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Marsh"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Forest"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Water"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Mountain"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Plain"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Crags"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Goal"), typeof(Material))
	};
}


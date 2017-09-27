using UnityEngine;

public enum MapLayers
{
	BottomLayer = 0,
	DecorationLayer,
	Count,
}

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TileMap : MonoBehaviour
{
    public int NumberOfTilesX = 100;
    public int NumberOfTilesY = 100;
    public float TileSize = 1.0f;

    public Texture2D [] TilesTextureLayers;

    public int TileResolution = 16;

    public TileMapData MapData;

	public Transform RockPrefab;
	public Transform TreePrefab;

	public int RockAbundance = 10;

	private TileColliders _colliders;


	// Use this for initialization
	void Start () {
        
        MapData = new TileMapData(NumberOfTilesX, NumberOfTilesY, RockAbundance);
		
		BuildMesh();

		PopulateWithResources();

		_colliders = new TileColliders();
	}

	// Update is called once per frame
	void Update () {

	}

    Color[][] GetTiles(int mapLayer)
    {
        int numTilesPerRow = TilesTextureLayers[mapLayer].width / TileResolution;
        int numRows = TilesTextureLayers[mapLayer].height / TileResolution;

        Color[][] tiles = new Color[numTilesPerRow * numRows][];

        for(int y = 0; y < numRows; ++y)
        {
            for(int x = 0; x < numTilesPerRow; ++x)
            {
                tiles[y * numTilesPerRow + x] = TilesTextureLayers[mapLayer].GetPixels(x * TileResolution, y * TileResolution, TileResolution, TileResolution);
            }
        }

        return tiles;
    }

	public void RegenerateMesh()
	{
		if (MapData == null)
		{
			MapData = new TileMapData(NumberOfTilesX, NumberOfTilesY, RockAbundance);
		}

		if (_colliders != null)
		{ 
			_colliders.Clean();
			_colliders = new TileColliders();
		}

		BuildMesh();

		PopulateWithResources();

	}

	public void BuildMesh()
    {
        int numTiles = NumberOfTilesX * NumberOfTilesY;
        int numTriangles = numTiles * 2;

        int numberOfVerticesX = NumberOfTilesX + 1;
        int numberOfVerticesY = NumberOfTilesY + 1;
        int numVerts = numberOfVerticesX * numberOfVerticesY;

        Vector3[] verticesCoordinates = new Vector3[numVerts];

        Vector3[] verticesNormals = new Vector3[numVerts];
        Vector2[] uvs = new Vector2[numVerts];
        
        int[] triangles = new int[numTriangles * 3];


        for(int y = 0; y < numberOfVerticesY; ++y)
        {
            for(int x = 0; x < numberOfVerticesX; ++x)
            {
                verticesCoordinates[y * numberOfVerticesX + x] = new Vector3(x * TileSize, y * TileSize, 0.0f);
                verticesNormals[y * numberOfVerticesX + x] = Vector3.up;
                uvs[y * numberOfVerticesX + x] = new Vector2((float)x / NumberOfTilesX, (float)y / NumberOfTilesY);
            }
        }

        int squareIndex = 0;
        int triangleOffset = 0;
        for (int y = 0; y < NumberOfTilesY; ++y)
        {
            for (int x = 0; x < NumberOfTilesX; ++x)
            {
                squareIndex = y * NumberOfTilesX + x;
                triangleOffset = squareIndex * 6;
                triangles[triangleOffset + 0] = y * numberOfVerticesX + x;
                triangles[triangleOffset + 1] = y * numberOfVerticesX + x + numberOfVerticesX;
                triangles[triangleOffset + 2] = y * numberOfVerticesX + x + numberOfVerticesX + 1;

                triangles[triangleOffset + 3] = y * numberOfVerticesX + x;
                triangles[triangleOffset + 4] = y * numberOfVerticesX + x + numberOfVerticesX + 1;
                triangles[triangleOffset + 5] = y * numberOfVerticesX + x + 1;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verticesCoordinates;
        mesh.triangles = triangles;
        mesh.normals = verticesNormals;
        mesh.uv = uvs;


        MeshFilter meshFilter = GetComponent<MeshFilter>();

        meshFilter.mesh = mesh;

        BuildTexture();
    }

    void BuildTexture()
    {
        int textureWidth = NumberOfTilesX * TileResolution;
        int textureHeight = NumberOfTilesY * TileResolution;

        Texture2D texture = new Texture2D(textureWidth, textureHeight);

		for (int layerIndex = 0; layerIndex < (int)MapLayers.Count; layerIndex++)
		{
			Color[][] tiles = GetTiles(layerIndex);

			for (int y = 0; y < NumberOfTilesY; ++y)
			{
				for (int x = 0; x < NumberOfTilesX; ++x)
				{
					int tilePixelIndex = MapData.GetTileType(x, y, layerIndex);
					if (tilePixelIndex != -1)
					{
						texture.SetPixels(x * TileResolution, y * TileResolution, TileResolution, TileResolution, tiles[tilePixelIndex]);
					}
				}
			}

		}

        texture.filterMode = FilterMode.Point;
        texture.Apply();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterials[0].mainTexture = texture;

    }

	private void PopulateWithResources()
	{
		for (int resourceIndex = 0; resourceIndex < MapData.NumberOfResources; ++resourceIndex)
		{
			Vector2 rockPosition = MapData.GetResourceOfTypePositionByIndex(ResourcesTileType.Rock, resourceIndex);
			Vector2 treePosition = MapData.GetResourceOfTypePositionByIndex(ResourcesTileType.Tree, resourceIndex);
			Instantiate(RockPrefab, ConvertMapCoordintesToWorld( new Vector3(rockPosition.x, rockPosition.y, -5)), Quaternion.identity, GameObject.Find("Resources").transform);
			Instantiate(TreePrefab, ConvertMapCoordintesToWorld(new Vector3(treePosition.x, treePosition.y, -5)), Quaternion.identity, GameObject.Find("Resources").transform);
		}
	}

	private Vector3 ConvertMapCoordintesToWorld(Vector3 worldPosition)
	{
		float tileCenter = 0.5f;
		return new Vector3(worldPosition.x + tileCenter, worldPosition.y + tileCenter, worldPosition.z);
	}
}

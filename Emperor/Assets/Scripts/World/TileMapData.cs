using UnityEngine;
using System.Collections;

public enum BottomLayerTileType
{
	Invalid = -1,
    Grass = 0,
    Water,
    Mountain,
    Sand,    
    Count,
}

public enum DecorationLayerTileType
{
	Invalid = -1,
	Rock = 0,
	Tree,
	Count
}

public enum ResourcesTileType
{
	Invalid = -1,
	Rock = 0,
	Tree,
	Count
}

public class TileMapData {

    private int[,,] _mapData;

	private Vector2[,] _resources;	//Each row is a different resource

    private int _sizeX;
    private int _sizeY;

	public int NumberOfResources;

    public TileMapData(int sizeX, int sizeY, int numberOfResources)
    {
        _sizeX = sizeX;
        _sizeY = sizeY;

		NumberOfResources = numberOfResources;

        _mapData = new int [sizeX, sizeY, (int) MapLayers.Count];
		_resources = new Vector2 [(int)ResourcesTileType.Count, NumberOfResources];

		GenerateMapData();
    }

    public void GenerateMapData()
    {
		for (int layerIndex = 0; layerIndex < (int) MapLayers.Count; layerIndex++)
		{
			switch((MapLayers) layerIndex)
			{
				case MapLayers.BottomLayer:
					GenerateBottomLayer();
					break;
				case MapLayers.DecorationLayer:
					GenerateDecorationLayer();
					break;
			}
		}

		PopulateWithResources();
    }

	private void GenerateBottomLayer()
	{
		for (int y = 0; y < _sizeY; ++y)
		{
			for (int x = 0; x < _sizeX; ++x)
			{
				_mapData[x, y, (int) MapLayers.BottomLayer] = (int) BottomLayerTileType.Grass;
			}
		}

		for (int x = 1; x < _sizeX; ++x)
		{
			_mapData[x, 2, (int) MapLayers.BottomLayer] = (int) BottomLayerTileType.Water;
		}
	}

	private void GenerateDecorationLayer()
	{
		for (int y = 0; y < _sizeY; ++y)
		{
			for (int x = 0; x < _sizeX; ++x)
			{
				_mapData[x, y, (int)MapLayers.DecorationLayer] = -1;
			}
		}
	}

	private void PopulateWithResources()
	{
		GenerateRocks();
		GenerateTrees();
	}
	
	private void GenerateRocks()
	{
		for (int resourceIndex = 0; resourceIndex < NumberOfResources; ++resourceIndex)
		{
			int x = 1;
			int y = resourceIndex;

			if(x < 0)
			{
				x = 0;
			}
			else if( x > _sizeX)
			{
				x = x % _sizeX;
			}

			if (y < 0)
			{
				y = 0;
			}
			else if (y > _sizeY)
			{
				y = y % _sizeY;
			}

			_resources[(int) ResourcesTileType.Rock, resourceIndex] = new Vector2(x, y);
		}
	}

	private void GenerateTrees()
	{
		for (int resourceIndex = 0; resourceIndex < NumberOfResources; ++resourceIndex)
		{
			int x = resourceIndex + 2;
			int y = 0;

			if (x < 0)
			{
				x = 0;
			}
			else if (x > _sizeX)
			{
				x = x % _sizeX;
			}

			if (y < 0)
			{
				y = 0;
			}
			else if (y > _sizeY)
			{
				y = y % _sizeY;
			}

			_resources[(int)ResourcesTileType.Tree, resourceIndex] = new Vector2(x, y);
		}
	}

	public int GetTileType(int x, int y, int z)
    {
        return _mapData[x, y, z];
    }

	public Vector2 GetResourceOfTypePositionByIndex(ResourcesTileType resourceType, int resourceIndex)
	{
		return _resources[(int) resourceType, resourceIndex];
	}
}

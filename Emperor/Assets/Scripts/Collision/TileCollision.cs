using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class TileCollision : MonoBehaviour {

    Vector2 currentPosition;
    Vector2 oldPosition;
    TileMapData mapData;
    float tileSize = 1f;
    InputMovement entityMovement;
    Rigidbody2D entityBody;

	// Use this for initialization
	void Start () {

        oldPosition = currentPosition;
        tileSize = GameObject.Find("World").GetComponent<TileMap>().TileSize;
        mapData = GameObject.Find("World").GetComponent<TileMap>().MapData;
        if(mapData == null)
        {
            Debug.Log("Couldn´t find map data!");
        }

        entityMovement = GetComponent<InputMovement>();
        entityBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        oldPosition = currentPosition;
        currentPosition = transform.position;

        CheckSurroundingTiles();
	}

    void CheckSurroundingTiles()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if(currentPosition.x + i < 0)
                {
                    continue;
                }

                if(currentPosition.y + j < 0)
                {
                    continue;
                }

                BottomLayerTileType terrain = (BottomLayerTileType) mapData.GetTileType((int) ((currentPosition.x + i) / tileSize), (int) ((currentPosition.y + j) / tileSize), (int) MapLayers.BottomLayer);

                if(terrain == BottomLayerTileType.Water 
                    || terrain == BottomLayerTileType.Mountain)
                {
                    //Debug.Log("Dot product: " + Vector2.Dot(entityBody.velocity, new Vector2(1, 0)));
                    //Debug.Log("Velocity: " + entityBody.velocity);
                    if (Vector2.Dot(entityBody.velocity, new Vector2(i, j)) > 0)
                    {
                        //Debug.Log("Stop X mate");
                        transform.position.Set(oldPosition.x, oldPosition.y + entityBody.velocity.y, 0f);
                        
                    }
                    else if (Vector2.Dot(entityBody.velocity, new Vector2(i, j)) > 0)
                    {
                        //Debug.Log("Stop Y mate");
                        transform.position.Set(oldPosition.x + entityBody.velocity.x, oldPosition.y, 0f);
                        
                    }

                    entityBody.velocity = new Vector2(0f, 0f);
                    entityMovement.Movement = new Vector2(0f, 0f);
                }
            }
        }
    }
}

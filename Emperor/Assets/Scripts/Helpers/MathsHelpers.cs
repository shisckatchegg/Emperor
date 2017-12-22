using UnityEngine;

static public class MathsHelpers
{
	static public Vector3 ConvertMapCoordintesToWorld(Vector3 worldPosition)
	{
		float tileCenter = 0.5f;
		return new Vector3(worldPosition.x + tileCenter, worldPosition.y + tileCenter, worldPosition.z);
	}
}

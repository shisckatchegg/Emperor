
public enum ResourceType
{
	Invalid = -1,
	Stone = 0,
	Wood,
	Gold,
	Count
}

public class Resource {

	public int ResourceAmount;

	public ResourceType Type;

	public float ResourceCollectionRate;
}

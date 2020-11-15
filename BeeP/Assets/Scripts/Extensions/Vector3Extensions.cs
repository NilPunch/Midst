using UnityEngine;

public static class Vector3Extensions
{ 
    //вопросы дают полям эти интересные свойства (наверное)
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        float newX = x.HasValue ? x.Value : original.x;
        float newY = y.HasValue ? y.Value : original.y;
        float newZ = z.HasValue ? z.Value : original.z;
        return new Vector3(newX, newY, newZ);
    }

	public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
	{
		float newX = x.HasValue ? x.Value : original.x;
		float newY = y.HasValue ? y.Value : original.y;
		return new Vector2(newX, newY);
	}
}
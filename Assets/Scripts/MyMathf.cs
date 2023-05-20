using UnityEngine;

public static class MyMathf
{
    public static int MoveTowards(int value, int target, int delta)
    {
        if (value > target)
        {
            int difference = value - delta;
            return difference > target ? difference : target;
        }
        else if (value < target)
        {
            int sum = value + delta;
            return sum < target ? sum : target;
        }
        else
        {
            return target;
        }
    }

    public static Vector2 GetMapPosition(Vector3 worldPosition)
    {
        return new Vector2(worldPosition.x, worldPosition.z);
    }
}

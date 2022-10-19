using System;

public static class ArrayUtility
{
    public static T[] AddRow<T>(T[] oldArray, T[] newRow)
    {
        if (oldArray.Length % newRow.Length != 0)
        {
            throw new InvalidOperationException($"{nameof(oldArray)} cannot convert to two dimensional array with size {newRow.Length}xN ");
        }

        T[] newArray = new T[oldArray.Length + newRow.Length];

        for (int i = 0; i < oldArray.Length; i++)
        {
            newArray[i] = oldArray[i];
        }

        int j = 0;

        for (int i = oldArray.Length; i < newArray.Length; i++, j++)
        {
            newArray[i] = newRow[j];
        }

        return newArray;
    }

    public static T[] AddColoumn<T>(T[] oldArray, T[] newColoumn)
    {
        if (oldArray.Length % newColoumn.Length != 0)
        {
            throw new InvalidOperationException($"{nameof(oldArray)} cannot convert to two dimensional array with size Nx{newColoumn.Length}");
        }

        int oldWidth = oldArray.Length / newColoumn.Length + 1;

        T[] newArray = new T[oldArray.Length + newColoumn.Length];

        int newColoumnIndex = 0;
        int oldArrayIndex = 0;

        for (int i = 0; i < oldArray.Length; i++)
        {
            if (i % oldWidth == 0)
            {
                newArray[i] = newColoumn[newColoumnIndex];
                newColoumnIndex++;
            }
            else
            {
                newArray[i] = oldArray[oldArrayIndex];
                oldArrayIndex++;
            }
        }

        return newArray;
    }

    public static T[] SplitRow<T>(ref T[] oldArray, int width)
    {
        if (oldArray.Length % width != 0)
        {
            throw new InvalidOperationException($"{nameof(oldArray)} cannot convert to two dimensional array with size {width}xN ");
        }

        T[] newArray = new T[oldArray.Length - width];
        T[] row = new T[width];

        for (int i = newArray.Length; i < oldArray.Length; i++)
        {
            row[i] = oldArray[i];
        }

        for (int i = 0; i < newArray.Length; i++)
        {
            newArray[i] = oldArray[i];
        }

        return newArray;
    }
}

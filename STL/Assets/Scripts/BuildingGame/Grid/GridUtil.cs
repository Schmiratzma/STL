using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtil
{
    /// <summary>
    /// transforms a Grid coordinate into worldposition with given dimensions of tile size
    /// </summary>
    /// <param name="coord">Gird Coordinate</param>
    /// <param name="tileDimensions">World Size of each tile</param>
    /// <returns>The World Position</returns>
    public static Vector3 GridCoordToWorldPos(Vector2Int coord, Vector2 tileDimensions)
    {
        return new Vector3(coord.x * tileDimensions.x, 0, coord.y * tileDimensions.y);
    }

    public static Vector2Int WorlPosToGridCoord(Vector3 worldPos, Vector2 tileSize)
    {
        return new Vector2Int((int)Mathf.Round(worldPos.x / tileSize.x), (int)Mathf.Round(worldPos.z / tileSize.y));
    }
}

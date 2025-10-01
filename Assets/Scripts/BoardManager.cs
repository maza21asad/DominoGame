using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform boardCenter;   // where the first tile goes
    public float tileSpacing = 1.6f;

    private List<DominoTile> boardTiles = new List<DominoTile>();

    // open values on left and right ends
    private int leftValue = -1;
    private int rightValue = -1;

    public void PlaceFirstTile(DominoTile tile)
    {
        if (tile == null) return;

        tile.transform.SetParent(boardCenter, false);
        tile.transform.localPosition = Vector3.zero;
        tile.transform.rotation = Quaternion.identity;

        boardTiles.Add(tile);

        leftValue = tile.leftValue;
        rightValue = tile.rightValue;

        Debug.Log($"First tile placed: {leftValue}|{rightValue}");
    }

    public bool TryPlaceTile(DominoTile tile)
    {
        if (tile == null) return false;

        // check left
        if (tile.leftValue == leftValue || tile.rightValue == leftValue)
        {
            PlaceTileAtEnd(tile, isLeft: true);
            return true;
        }

        // check right
        if (tile.leftValue == rightValue || tile.rightValue == rightValue)
        {
            PlaceTileAtEnd(tile, isLeft: false);
            return true;
        }

        // cannot play this tile
        return false;
    }

    private void PlaceTileAtEnd(DominoTile tile, bool isLeft)
    {
        DominoTile endTile = isLeft ? boardTiles[0] : boardTiles[boardTiles.Count - 1];
        Vector3 offsetDir = isLeft ? Vector3.left : Vector3.right;

        // position new tile relative to end
        tile.transform.SetParent(boardCenter, false);
        tile.transform.localPosition = endTile.transform.localPosition + offsetDir * tileSpacing;

        // rotate doubles perpendicular (optional visual tweak)
        if (tile.leftValue == tile.rightValue)
            tile.transform.rotation = Quaternion.Euler(0, 0, 90f);
        else
            tile.transform.rotation = Quaternion.identity;

        if (isLeft)
        {
            boardTiles.Insert(0, tile);
            leftValue = tile.OtherValue(leftValue);
        }
        else
        {
            boardTiles.Add(tile);
            rightValue = tile.OtherValue(rightValue);
        }

        Debug.Log($"Placed tile. New ends: {leftValue}|{rightValue}");
    }

    // Expose ends for rule checks
    public int GetLeftEnd() => leftValue;
    public int GetRightEnd() => rightValue;
}

//using System.Collections.Generic;

//[System.Serializable]
//public class Player
//{
//    public string playerName;
//    public List<DominoTile> hand = new List<DominoTile>();
//    public int score;

//    public Player(string name)
//    {
//        playerName = name;
//        score = 0;
//    }

//    public bool HasValidMove(int leftEnd, int rightEnd)
//    {
//        foreach (DominoTile tile in hand)
//        {
//            if (tile.Matches(leftEnd) || tile.Matches(rightEnd))
//                return true;
//        }
//        return false;
//    }
//}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[System.Serializable]
public class Player
{
    public string playerName;
    public List<DominoTile> hand = new List<DominoTile>();
    public int score;

    public Transform handHolder; // 👈 UI/scene holder for this player's tiles

    public Player(string name, Transform holder)
    {
        playerName = name;
        handHolder = holder;
        score = 0;
        hand = new List<DominoTile>();
    }

    // Called when a tile is dealt or drawn
    public void AddTile(DominoTile tile)
    {
        if (tile == null) return;
        hand.Add(tile);
        // optionally set tile to a holding area or UI parent later

        // Place under holder in scene
        tile.transform.SetParent(handHolder, false);

        // Position it (simple horizontal layout)
        float offset = hand.Count * 1.5f; // spacing
        tile.transform.localPosition = new Vector3(offset, 0, 0);
    }

    //// Remove a tile from hand (returns tile if removed, null otherwise)
    //public DominoTile RemoveTile(DominoTile tile)
    //{
    //    if (tile == null) return null;
    //    if (hand.Remove(tile)) return tile;
    //    return null;
    //}

    //// Checks if any tile in hand matches either open end
    //public bool HasValidMove(int leftEnd, int rightEnd)
    //{
    //    foreach (DominoTile tile in hand)
    //    {
    //        if (tile == null) continue;
    //        if (tile.Matches(leftEnd) || tile.Matches(rightEnd))
    //            return true;
    //    }
    //    return false;
    //}

    //// Helper: sum of pips (for scoring)
    //public int TotalPips()
    //{
    //    int sum = 0;
    //    foreach (var t in hand)
    //        sum += t.leftValue + t.rightValue;
    //    return sum;
    //}
}

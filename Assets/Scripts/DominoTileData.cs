using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DominoTileData", menuName = "Domino/DominoTileData")]

public class DominoTileData : ScriptableObject
{
    public int leftValue;
    public int rightValue;
    public Sprite dominoSprite;
}

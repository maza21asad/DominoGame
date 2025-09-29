using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoTile : MonoBehaviour
{
    public int leftValue;
    public int rightValue;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(DominoTileData data)
    {
        leftValue = data.leftValue;
        rightValue = data.rightValue;
        spriteRenderer.sprite = data.dominoSprite;
    }

    public bool Matches(int value)
    {
        return leftValue == value || rightValue == value;
    }

    public void Flip()
    {
        // swap values if needed
        int temp = leftValue;
        leftValue = rightValue;
        rightValue = temp;

        // visually rotate 180
        transform.Rotate(0, 180, 0);
    }
}


using System.Collections.Generic;

[System.Serializable]
public class Player
{
    public string playerName;
    public List<DominoTile> hand = new List<DominoTile>();
    public int score;

    public Player(string name)
    {
        playerName = name;
        score = 0;
    }

    public bool HasValidMove(int leftEnd, int rightEnd)
    {
        foreach (DominoTile tile in hand)
        {
            if (tile.Matches(leftEnd) || tile.Matches(rightEnd))
                return true;
        }
        return false;
    }
}

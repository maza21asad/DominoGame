//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    public DominoManager dominoManager;

//    private List<Player> players = new List<Player>();
//    private int currentPlayerIndex = 0;

//    private void Start()
//    {
//        SetupGame();
//    }

//    private void SetupGame()
//    {
//        // Create 2 players
//        players.Add(new Player("Player 1"));
//        players.Add(new Player("Player 2"));

//        // Generate the full set of dominoes
//        dominoManager.GenerateDominoSet();

//        // Deal 7 tiles each
//        foreach (Player player in players)
//        {
//            for (int i = 0; i < 7; i++)
//            {
//                var tile = dominoManager.DrawTile();
//                player.AddTile(tile);
//            }
//        }

//        Debug.Log("Players have been dealt their starting tiles.");
//    }

//    private void NextTurn()
//    {
//        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
//        Debug.Log(players[currentPlayerIndex].playerName + " turn.");
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DominoManager dominoManager;
    public int tilesPerPlayer = 7;

    private List<Player> players = new List<Player>();

    private void Start()
    {
        SetupGame();
    }

    private void SetupGame()
    {
        // create players
        players.Clear();
        players.Add(new Player("Player 1"));
        players.Add(new Player("Player 2"));

        // generate domino set and boneyard
        dominoManager.GenerateDominoSet();

        // deal tiles
        for (int p = 0; p < players.Count; p++)
        {
            for (int t = 0; t < tilesPerPlayer; t++)
            {
                DominoTile drawn = dominoManager.DrawTile();
                if (drawn == null)
                {
                    Debug.LogWarning("Boneyard empty while dealing.");
                    break;
                }

                players[p].AddTile(drawn);
                // Optional: position the tile somewhere offscreen or parent to a holder
                drawn.transform.SetParent(this.transform); // temporary parent
            }
            Debug.Log(players[p].playerName + " has " + players[p].hand.Count + " tiles.");
        }
    }
}

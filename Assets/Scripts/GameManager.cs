//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    public DominoManager dominoManager;
//    public int tilesPerPlayer = 7;

//    private List<Player> players = new List<Player>();

//    private void Start()
//    {
//        SetupGame();
//    }

//    private void SetupGame()
//    {
//        // create players
//        players.Clear();
//        players.Add(new Player("Player 1"));
//        players.Add(new Player("Player 2"));

//        // generate domino set and boneyard
//        dominoManager.GenerateDominoSet();

//        // deal tiles
//        for (int p = 0; p < players.Count; p++)
//        {
//            for (int t = 0; t < tilesPerPlayer; t++)
//            {
//                DominoTile drawn = dominoManager.DrawTile();
//                if (drawn == null)
//                {
//                    Debug.LogWarning("Boneyard empty while dealing.");
//                    break;
//                }

//                players[p].AddTile(drawn);
//                // Optional: position the tile somewhere offscreen or parent to a holder
//                drawn.transform.SetParent(this.transform); // temporary parent
//            }
//            Debug.Log(players[p].playerName + " has " + players[p].hand.Count + " tiles.");
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DominoManager dominoManager;
    public int tilesPerPlayer = 7;

    public BoardManager boardManager;

    [Header("Hand Holders")]
    public Transform player1HandHolder;
    public Transform player2HandHolder;

    private List<Player> players = new List<Player>();

    private void Start()
    {
        SetupGame();
    }

    private void SetupGame()
    {
        players.Clear();
        players.Add(new Player("Player 1", player1HandHolder));
        players.Add(new Player("Player 2", player2HandHolder));

        dominoManager.GenerateDominoSet();

        for (int p = 0; p < players.Count; p++)
        {
            for (int t = 0; t < tilesPerPlayer; t++)
            {
                DominoTile drawn = dominoManager.DrawTile();
                if (drawn == null) break;

                players[p].AddTile(drawn);
            }
            Debug.Log(players[p].playerName + " has " + players[p].hand.Count + " tiles.");
        }

        // Example: auto-place first tile (highest double rule will come later)
        DominoTile first = players[0].hand[0];
        players[0].RemoveTile(first);
        boardManager.PlaceFirstTile(first);
    }
}


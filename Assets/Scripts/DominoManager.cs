//using System.Collections.Generic;
//using UnityEngine;

//public class DominoManager : MonoBehaviour
//{
//    public DominoTileData[] allDominoData; // assign 28 SOs in Inspector
//    public GameObject dominoPrefab;        // prefab with DominoTile script

//    private List<DominoTile> dominoSet = new List<DominoTile>();
//    private Queue<DominoTile> boneyard = new Queue<DominoTile>();

//    public void GenerateDominoSet()
//    {
//        dominoSet.Clear();

//        foreach (DominoTileData data in allDominoData)
//        {
//            GameObject tileObj = Instantiate(dominoPrefab);
//            DominoTile tile = tileObj.GetComponent<DominoTile>();
//            tile.Initialize(data);
//            dominoSet.Add(tile);
//            tile.gameObject.SetActive(false);
//        }

//        Shuffle(dominoSet);

//        // fill boneyard
//        boneyard = new Queue<DominoTile>(dominoSet);
//        Debug.Log("Generated " + dominoSet.Count + " tiles.");
//    }

//    public DominoTile DrawFromBoneyard()
//    {
//        if (boneyard.Count > 0)
//            return boneyard.Dequeue();
//        return null;
//    }

//    private void Shuffle(List<DominoTile> list)
//    {
//        for (int i = 0; i < list.Count; i++)
//        {
//            int rand = Random.Range(i, list.Count);
//            DominoTile temp = list[i];
//            list[i] = list[rand];
//            list[rand] = temp;
//        }
//    }
//}


using System.Collections.Generic;
using UnityEngine;

public class DominoManager : MonoBehaviour
{
    [Tooltip("Assign the 28 DominoTileData ScriptableObjects here (0_0 ... 6_6)")]
    public DominoTileData[] allDominoData; // assign 28 SOs in Inspector

    [Tooltip("Prefab that has a SpriteRenderer + DominoTile component")]
    public GameObject dominoPrefab;        // assign the prefab in Inspector

    private List<DominoTile> dominoSet = new List<DominoTile>();
    private Queue<DominoTile> boneyard = new Queue<DominoTile>();

    public void GenerateDominoSet()
    {
        // cleanup old tiles if any (safe to re-run)
        if (dominoSet.Count > 0)
        {
            foreach (var t in dominoSet)
                if (t != null) Destroy(t.gameObject);
            dominoSet.Clear();
        }

        // create tiles from ScriptableObjects
        foreach (DominoTileData data in allDominoData)
        {
            GameObject tileObj = Instantiate(dominoPrefab);
            DominoTile tile = tileObj.GetComponent<DominoTile>();
            tile.Initialize(data);
            tileObj.SetActive(false); // hide until drawn
            dominoSet.Add(tile);
        }

        // shuffle
        Shuffle(dominoSet);

        // put into boneyard queue
        boneyard = new Queue<DominoTile>(dominoSet);

        Debug.Log("Generated " + dominoSet.Count + " tiles. Boneyard size: " + boneyard.Count);
    }

    // Call this to draw one tile from the boneyard. Returns null if empty.
    public DominoTile DrawTile()
    {
        if (boneyard == null || boneyard.Count == 0)
            return null;

        DominoTile tile = boneyard.Dequeue();
        if (tile != null)
            tile.gameObject.SetActive(true);

        return tile;
    }

    public int BoneyardCount() => boneyard?.Count ?? 0;

    private void Shuffle(List<DominoTile> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            DominoTile temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}

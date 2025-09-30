using System.Collections.Generic;
using UnityEngine;

public class DominoManager : MonoBehaviour
{
    public DominoTileData[] allDominoData; // assign 28 SOs in Inspector
    public GameObject dominoPrefab;        // prefab with DominoTile script

    private List<DominoTile> dominoSet = new List<DominoTile>();
    private Queue<DominoTile> boneyard = new Queue<DominoTile>();

    public void GenerateDominoSet()
    {
        dominoSet.Clear();

        foreach (DominoTileData data in allDominoData)
        {
            GameObject tileObj = Instantiate(dominoPrefab);
            DominoTile tile = tileObj.GetComponent<DominoTile>();
            tile.Initialize(data);
            dominoSet.Add(tile);
            tile.gameObject.SetActive(false);
        }

        Shuffle(dominoSet);

        // fill boneyard
        boneyard = new Queue<DominoTile>(dominoSet);
        Debug.Log("Generated " + dominoSet.Count + " tiles.");
    }

    public DominoTile DrawFromBoneyard()
    {
        if (boneyard.Count > 0)
            return boneyard.Dequeue();
        return null;
    }

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

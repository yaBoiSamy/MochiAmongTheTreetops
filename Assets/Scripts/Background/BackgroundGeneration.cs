using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGeneration : MonoBehaviour
{
    public int[] treeDistanceRange;
    public int backgroundSize;

    private List<int> tree1Randomisation = new List<int>();
    private int tree1totalOccupiedSpace;
    public GameObject tree1Prefab;

    void Start()
    {
        while (tree1totalOccupiedSpace < backgroundSize) {
            int addedDistance = Random.Range(treeDistanceRange[0], treeDistanceRange[1]);
            tree1Randomisation.Add(addedDistance + tree1totalOccupiedSpace);
            foreach (int distance in tree1Randomisation)
            {
                tree1totalOccupiedSpace += addedDistance;
                Debug.Log(distance);
            }
        }
        foreach (int distance in tree1Randomisation)
        {
            Instantiate(tree1Prefab, new Vector3(distance, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

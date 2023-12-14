using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class DungeonCreator : MonoBehaviour
{
    public GameObject Lobby;
    public List<GameObject> levelList;
    public List<GameObject> restLevelList;
    public List<GameObject> bossLevelList;
    public List<GameObject> levelPositionsList;
    public List<GameObject> restLevelPositionsList;
    public List<GameObject> bossLevelPositionsList;

    private List<GameObject> levelInstanceList = new List<GameObject>();
    private GameObject thisLevel;

    // Start is called before the first frame update
    void Start()
    {
        levelInstanceList.Add(Lobby);
        List<GameObject> randomLevels = GetRandomItemsFromList<GameObject>(levelList, levelPositionsList.Count);
        CreateLevels(randomLevels, levelPositionsList);
        CreateLevels(restLevelList, restLevelPositionsList);
        CreateLevels(bossLevelList, bossLevelPositionsList);
        ConnectLevels();
    }

    public static List<T> GetRandomItemsFromList<T>(List<T> list, int number)
    {
        // this is the list we're going to remove picked items from
        List<T> tmpList = new List<T>(list);
        // this is the list we're going to move items to
        List<T> newList = new List<T>();

        // make sure tmpList isn't already empty
        while (newList.Count < number && tmpList.Count > 0)
        {
            int index = Random.Range(0, tmpList.Count);
            newList.Add(tmpList[index]);
            tmpList.RemoveAt(index);
        }

        return newList;
    }

    void CreateLevels(List<GameObject> levels, List<GameObject> positions)
    {
        for (int i = 0; i < positions.Count; i++) {
            thisLevel = Instantiate(levels[i], positions[i].transform.position, Quaternion.identity) as GameObject;
            LevelInfo thisLevelInfo = new LevelInfo();
            thisLevelInfo = thisLevel.GetComponent<LevelInfo>();
            thisLevelInfo.levelOrder = i+1;
            Debug.Log("created level " + i);
            levelInstanceList.Add(thisLevel);
        }
    }



    void ConnectLevels()
    {
        LevelInfo thisLevelInfo = new LevelInfo();
        LadderEndGame thisLevelLadderInfo = new LadderEndGame();
        HatchStart thisLevelHatchInfo = new HatchStart();

        LevelInfo nextLevelInfo = new LevelInfo();
        GameObject nextLevelHatch = new GameObject();

        LevelInfo prevLevelInfo = new LevelInfo();
        GameObject prevLevelLadder = new GameObject();

        for (int i = 0; i < levelInstanceList.Count; i++)
        {
            if (i < levelInstanceList.Count - 1)
            {
                thisLevelInfo = levelInstanceList[i].GetComponent<LevelInfo>();
                thisLevelLadderInfo = thisLevelInfo.Ladder.GetComponent<LadderEndGame>();

                nextLevelInfo = levelInstanceList[i + 1].GetComponent<LevelInfo>();
                nextLevelHatch = nextLevelInfo.Hatch;

                thisLevelLadderInfo.Hatch = nextLevelHatch;
            }

            if (i > 0) 
            {
                thisLevelInfo = levelInstanceList[i].GetComponent<LevelInfo>();
                thisLevelHatchInfo = thisLevelInfo.Hatch.GetComponent<HatchStart>();

                prevLevelInfo = levelInstanceList[i-1].GetComponent<LevelInfo>();
                prevLevelLadder = prevLevelInfo.Ladder;
                
                thisLevelHatchInfo.Ladder = prevLevelLadder;
            }
        }
    }
}

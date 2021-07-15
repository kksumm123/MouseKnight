using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageInfo
{
    public int stageID;
    public string titleString;
    public int rewardXP;

}
public class GameData : MonoBehaviour
{
    public static GameData instance;
    public List<StageInfo> stageInfos;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

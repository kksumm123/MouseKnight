using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] List<StageInfo> stageInfos;
    public static Dictionary<int, StageInfo> stageInfoMap = new Dictionary<int, StageInfo>();
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        stageInfoMap = stageInfos.ToDictionary(x => x.stageID);
    }
}

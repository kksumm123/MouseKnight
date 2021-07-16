using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class StageInfo
{
    public int stageID;
    public string titleString;
    public int rewardXP;

}
public class GameData : SingletonMonoBehavior<GameData>
{
    [SerializeField] List<StageInfo> stageInfos;
    public static Dictionary<int, StageInfo> stageInfoMap = new Dictionary<int, StageInfo>();
    new void Awake()
    {
        base.Awake();

        stageInfoMap = stageInfos.ToDictionary(x => x.stageID);
    }
}

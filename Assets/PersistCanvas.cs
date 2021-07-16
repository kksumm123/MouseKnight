using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistCanvas : SingletonMonoBehavior<PersistCanvas>
{
    public override string HierarchyPath => "PersistCanvas";
    public CanvasGroup blackScreen;
    new void Awake()
    {
        base.Awake();
        blackScreen = transform.Find("BlackScreen").GetComponent<CanvasGroup>();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using FrancoisSauce.changeName.Scripts.Player;
using FrancoisSauce.Scripts.FSEvents.SO;
using Sirenix.OdinInspector;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControllerPatrol : MonoBehaviour
{
    [SerializeField] private List<Transform> positions = new List<Transform>();
    [SerializeField] private float timeToPath = 1f;

    [SerializeField] private LoopType loopType = LoopType.Yoyo;
    
    [SerializeField] private FSIntEventSO onPlayerHitByEnemy = null;
    
#if UNITY_EDITOR
#if ODIN_INSPECTOR
    [Button("Find Positions")]
    [ContextMenu("Find Positions")]
#endif
    private void FindPositions()
    {
        positions.Clear();

        foreach (var componentsInChild in GetComponentsInChildren<PathPosition>())
        {
            positions.Add(componentsInChild.transform);
        }
    }
#endif

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.layer)
        {
            //Player layer
            case 8:
                if (other.gameObject.GetComponent<PlayerController>().isInvincible) break;
                onPlayerHitByEnemy.Invoke(1);
                break;
        }
    }
    
    public void OnClickedStartButton()
    {
        transform.DOPath(positions.Select(position => position.position).ToArray(), timeToPath, PathType.CatmullRom)
            .SetLoops(-1, loopType)
            .SetEase(Ease.Linear)
            .SetLookAt(0f)
            .SetOptions(AxisConstraint.None, AxisConstraint.X | AxisConstraint.W | AxisConstraint.Z);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}

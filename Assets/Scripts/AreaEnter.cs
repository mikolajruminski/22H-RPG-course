using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnter : MonoBehaviour
{
    [SerializeField] private PlayerAreaEnterPossibilitesScript.AreaEntrances areaEntrance;
    void Start()
    {
        if (areaEntrance == Player.Instance.GetNextAreaEntrancePoint())
        {
            Player.Instance.transform.position = transform.position;
        }
    }

}

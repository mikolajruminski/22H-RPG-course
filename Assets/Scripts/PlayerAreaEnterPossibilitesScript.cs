using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAreaEnterPossibilitesScript
{

    public enum AreaEntrances
    {
        Mountains_Right,
        Mountains_Left,
        Village_Top,
        Village_Left,
        Village_Right,
        Village_Bottom,
    }

    public static void SetAreaEntrance(AreaEntrances areaEntrance)
    {
        Player.Instance.SetNextAreaEntrancePoint(areaEntrance);
    }
}

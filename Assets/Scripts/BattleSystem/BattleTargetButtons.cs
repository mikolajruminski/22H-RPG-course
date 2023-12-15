using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTargetButtons : MonoBehaviour
{
    public string moveName;
    public int activeBattleTarget;
    public TextMeshProUGUI enemyName;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Press() 
    {
        BattleManager.Instance.PlayerAttack(moveName, activeBattleTarget);
    }
}

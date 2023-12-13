using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private float effectTime;
    [SerializeField] private int sfxNumberToPlay;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySFX(sfxNumberToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, effectTime);
    }
}

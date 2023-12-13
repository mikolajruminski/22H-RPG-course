using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDamageGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float lifeTime = 1f, moveSpeed, textVibration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime);
    }

    public void SetDamage(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        float jitterAmount = Random.Range(-textVibration, textVibration);
        transform.position += new Vector3(jitterAmount, jitterAmount, 0f);
    }
}

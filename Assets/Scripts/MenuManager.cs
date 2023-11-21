using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance {get; private set;}
    [SerializeField] private Image imageToFade;
    // Start is called before the first frame update

    private void Awake() 
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFade");
    }
}

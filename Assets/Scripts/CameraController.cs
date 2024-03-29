using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player playerTarget;
    CinemachineVirtualCamera virtualCamera;

    [SerializeField] private int musicToPlay;
    private bool musicAlreadyPlaying;
    // Start is called before the first frame update
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        playerTarget = FindObjectOfType<Player>();
        virtualCamera.Follow = playerTarget.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (!musicAlreadyPlaying)
        {
            musicAlreadyPlaying = true;
            AudioManager.Instance.PlayBackgroundMusic(musicToPlay);
        }
    }
}

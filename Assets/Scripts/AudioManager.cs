using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource[] SFX, backgroundMusic;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayBackgroundMusic(2);
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < SFX.Length)
        {
            SFX[soundToPlay].Play();
        }
    }

    public void PlayBackgroundMusic(int musicToPlay)
    {
        StopMusic();

        if (musicToPlay < backgroundMusic.Length)
        {
            backgroundMusic[musicToPlay].Play();
        }
    }

    private void StopMusic()
    {
        foreach (AudioSource song in backgroundMusic)
        {
            song.Stop();
        }
    }
}

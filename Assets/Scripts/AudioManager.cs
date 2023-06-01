using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource backgroundMusic, levelEndMusic, bossMusic, victoryBoss;

    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect(int soundEffectIndex) 
    {
        soundEffects[soundEffectIndex].Stop();

        soundEffects[soundEffectIndex].pitch = Random.Range(0.9f, 1.1f);

        soundEffects[soundEffectIndex].Play();
    }

    public void PlayLevelVictory() 
    {
        backgroundMusic.Stop();
        levelEndMusic.Play();
    }

    public void PlayBossMusic() 
    {
        backgroundMusic.Stop();

        bossMusic.Play();
    }

    public void StopBossMusic() 
    {
       victoryBoss.Play();

        bossMusic.Stop();
    }
}

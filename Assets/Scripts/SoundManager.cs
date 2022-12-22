using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource aus;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip bulletAdvClip;
    private bool bulletAdvPlayed;
    [SerializeField] private AudioClip killSound;

    private void Awake()
    {
        if (instance == null)       { instance = this; }
        else                        { Destroy(gameObject);}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayStepSound()
    {
        aus.PlayOneShot(moveClip);
    } 
    public void PlayShootSound()
    {
        aus.PlayOneShot(shootClip);
    }
    public void PlaySpecialSound(AudioClip clip)
    {
        aus.PlayOneShot(clip);
    }
    public void PlayBulletsAdvSound()
    {
        if (!bulletAdvPlayed) {aus.PlayOneShot(bulletAdvClip); }        
    }
    public void RefreshBulletAdvSound()
    {
        bulletAdvPlayed = false;
    }
    public void PlayDeathSound()
    {
        aus.PlayOneShot(killSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

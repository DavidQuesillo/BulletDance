using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource aus;
    [SerializeField] private List<AudioClip> battleSongs;
    [SerializeField] private AudioClip winSong;

    private void Awake()
    {
        if (instance == null) { instance = this;}
        else { Destroy(gameObject);}
    }

    public void PlayBattleMusic()
    {
        aus.volume = 0.6f;
        aus.clip = battleSongs[Random.Range(0, battleSongs.Count)];
        aus.Play();
    }
    public void PlayBattleMusic(int songIndex)
    {
        aus.volume = 0.6f;
        aus.clip = battleSongs[songIndex];
        aus.Play();
    }
    public void StopBattleMusic()
    {
        aus.DOFade(0f, 0.5f).OnComplete(() => aus.Stop());

    }
    public void PlayVictoryMusic()
    {
        aus.volume = 0.2f;
        aus.PlayOneShot(winSong);
    }
    public void StopPlaying()
    {
        aus.DOFade(0f, 0.5f).OnComplete(() => aus.Stop());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

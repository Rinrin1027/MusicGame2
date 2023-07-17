using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audio;
    AudioClip Music;
    string songName;
    bool played;

    void Start()
    {
        GManager.instance.Start = false;
        songName = "Blossom";
        audio = GetComponent<AudioSource>();
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        played = false;

        StartCoroutine(StartMusicDelay());
    }

    IEnumerator StartMusicDelay()
    {
        yield return new WaitForSeconds(5f); // 5秒待機
        GManager.instance.Start = true;
        GManager.instance.StartTime = Time.time;
        played = true;
        audio.PlayOneShot(Music);
    }

    void Update()
    {
        // Updateメソッドは不要なので削除しても構いません
    }
}

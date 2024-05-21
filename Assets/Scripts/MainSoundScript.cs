using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundScript : MonoBehaviour
{
    public AudioSource Audio; //오디오

    public bool Is_Sound = true;
    // Start is called before the first frame update
    void Awake()
    {
        //DONTDESTROY로 인해 원래 씬으로 돌아왔을 때 같은 오브젝트가 있을 때 중복방지코드
        var obj = FindObjectsOfType<MainSoundScript>();

        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        playSound(); //오디오 플레이
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void playSound()
    {
        if(Is_Sound) Audio.Play();
    }
}

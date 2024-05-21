using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundScript : MonoBehaviour
{
    public AudioSource Audio; //�����

    public bool Is_Sound = true;
    // Start is called before the first frame update
    void Awake()
    {
        //DONTDESTROY�� ���� ���� ������ ���ƿ��� �� ���� ������Ʈ�� ���� �� �ߺ������ڵ�
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
        playSound(); //����� �÷���
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

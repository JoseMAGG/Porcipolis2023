using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamagotchiAudioController : MonoBehaviour
{
    [System.Serializable]
    public class AudioClipProp {
        public string name;
        [HideInInspector] public bool active = false;
        public AudioClip audioClip;

        public AudioClipProp(string name, AudioClip clip,bool active)
        {
            this.name = name;
            audioClip = clip;
            this.active = active;
        }
    }

    public List<AudioClipProp> audios;

    AudioSource audioSource;
    
        
    void Start()
    {
        TamagotchiEvent.instance.OnCerdoCritico += ChangeClip;

        audioSource = this.GetComponent<AudioSource>();
        audios.Add(new AudioClipProp("principal", audioSource.clip, true));
    }

    public void ChangeClip(string name)
    {
        AudioClipProp nextAudio = audios.Find(x => x.name.Equals(name) && !x.active);
        if (nextAudio is null) return;
        
        #region Disable actual
        AudioClipProp actualAudio = audios.Find(x => x.active);
        actualAudio.active = false;
        #endregion

        #region Enable next
        nextAudio.active = true;
        audioSource.clip = nextAudio.audioClip ;
        audioSource.Play();
        #endregion 
    }
}

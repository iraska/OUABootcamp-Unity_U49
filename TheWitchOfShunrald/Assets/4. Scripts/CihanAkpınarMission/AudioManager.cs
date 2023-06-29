using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace CihanAkpınar
{
    public class AudioManager : MonoBehaviour
    {
        #region Static Instance
        private static AudioManager instance;

        public static AudioManager Instance
        {
            get
            {
                if (instance==null)
                instance=FindObjectOfType<AudioManager>();
                {
                    if (instance==null)
                    {
                    instance=new GameObject("SpawnedAudioManager",typeof(AudioManager)).GetComponent<AudioManager>();
                    }   
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
           
        }
        #endregion

        #region Field

        private AudioSource musicSource;
        private AudioSource musicSource2;
        private AudioSource sfxSource;

        private bool firstMusicSourcePlayin;
        
        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            
            musicSource = this.gameObject.AddComponent<AudioSource>();
            musicSource2 = this.gameObject.AddComponent<AudioSource>();
            sfxSource = this.gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;
            musicSource2.loop = true;
        }

        public void PlayMusic(AudioClip musicClip)
        {
            AudioSource activeSource = (firstMusicSourcePlayin) ? musicSource : musicSource2;
            
            activeSource.clip = musicClip;
            activeSource.volume = 1;
            activeSource.Play();
        }
        public void PlayMusicWithFade(AudioClip newClip, float tranitionTime = 1.0f)
        {
            AudioSource activeSource = (firstMusicSourcePlayin) ? musicSource : musicSource2;

            StartCoroutine(UpdateMusicWithFade(activeSource, newClip, tranitionTime));
        } 
        
        IEnumerator UpdateMusicWithFade(AudioSource activeSource,AudioClip newClip,float transationTime)
        {
            if (!activeSource.isPlaying)
                activeSource.Play();
            
            float t = 0.0f;

            for (t = 0;  t< transationTime; t+=Time.deltaTime)
            {
                activeSource.volume =  (t / transationTime);
             yield return null;
            }
            
            activeSource.Stop();
            activeSource.clip = newClip;
            activeSource.Play();
        }

        public void PlaySfx(AudioClip clip)
        {
          sfxSource.PlayOneShot(clip);    
        }
        public void PlaySfx(AudioClip clip, float volume)
        {
          sfxSource.PlayOneShot(clip,volume);  
        }
    }  
}

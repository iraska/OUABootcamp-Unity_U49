using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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
        //Sliders
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        //Mixers
        [SerializeField] private AudioMixer audioMixer;
        private AudioMixerGroup[] musicGroups;
        private AudioMixerGroup[] sfxGroups;
        //Sources
        private AudioSource musicSource;
        private AudioSource musicSource2;
        private AudioSource sfxSource;
        //Transation
        private bool firstMusicSourcePlayin;
        //AtmosphereAudios
        public AudioClip dungeonAtmosphereAudios;
        public AudioClip cemeteryAtmosphereAudios;
        //UIMainMenu
        public AudioClip mainMenuAudio;
        public AudioClip buttonClickAudio;
        //BasicSpawnedEnemy
        public AudioClip basicSpawnedEnemyHitAudio;
        public AudioClip basicSpawnedEnemyDieAudio;
        public AudioClip basicSpawnedEnemyTakeDamageAudio;
        //WitchIrem
        public AudioClip witchNormalAudio;
        public AudioClip witchDieAudio;
        public AudioClip witchTakeDamageAudio;
        public AudioClip witchAttackScreamAudio;
        //Staff
        public AudioClip staffProjectileExplosionAudio;
        //NecroMancerSidar
        //---------------
        //RangedEnemyYunus
        public AudioClip rangedEnemyRangeAttackAudio;
        public AudioClip rangedEnemyDieAudio;
        public AudioClip rangedEnemyTakeDamageAudio;
        //Object
        public AudioClip moveableObjectDestructionAudio;

        



        #endregion

        private void Start()
        {
          SetMusicVolume();  
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            
            musicGroups = audioMixer.FindMatchingGroups("MusicVolume");
            sfxGroups = audioMixer.FindMatchingGroups("SfxVolume");
            
            musicSource = this.gameObject.AddComponent<AudioSource>();
            musicSource2 = this.gameObject.AddComponent<AudioSource>();
            sfxSource = this.gameObject.AddComponent<AudioSource>();
            
            
            musicSource.outputAudioMixerGroup = musicGroups[0];
            musicSource2.outputAudioMixerGroup = musicGroups[0];
            sfxSource.outputAudioMixerGroup = sfxGroups[0];

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

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            audioMixer.SetFloat("musicVolumeSet", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume",volume);
            Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
        }

        private void LoadVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SetMusicVolume();
        }
        public void SetSfxVolume()
        {
            float volume = musicSlider.value;
            audioMixer.SetFloat("sfxVolumeSet", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("sfxVolume",volume);
            Debug.Log(PlayerPrefs.GetFloat("sfxVolume"));
        }

        private void LoadSfxVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            SetMusicVolume();
        }
    }  
}

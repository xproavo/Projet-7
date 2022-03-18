using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider _EffectSoundSlider;
    public Slider _MusicSoundSlider;
    public Slider _MasterSoundSlider;
    
    [SerializeField]
    private AudioMixer _audioMixer;


    public void UpdateMasterVolume(float vol)
    {
        _audioMixer.SetFloat("Master", vol);
    }    
    
    public void UpdateMusicVolume(float vol)
    {
        _audioMixer.SetFloat("Music", vol);
    }    
    
    public void UpdateEffectVolume(float vol)
    {
        _audioMixer.SetFloat("Effect", vol);
    }
    
}

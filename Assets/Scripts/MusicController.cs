using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{

    public AudioMixerSnapshot ice;
    public AudioMixerSnapshot steampunk;
    public AudioMixerSnapshot apocalypse;
    

    public float bpm = 100;

    private float _transitionIn;
    private float _quarterNote;
    private Dictionary<int, AudioMixerSnapshot> _layerToMusicMap;
    // Use this for initialization
    void Start()
    {
        _quarterNote = 60 / bpm;
        _transitionIn = _quarterNote;
        _layerToMusicMap = new Dictionary<int, AudioMixerSnapshot>();
        _layerToMusicMap.Add(0, apocalypse);
        _layerToMusicMap.Add(1, steampunk);
        _layerToMusicMap.Add(2, ice);
    }

    public void LayerChange(int layer)
    {
        _layerToMusicMap[layer].TransitionTo(_transitionIn);       
    }

}


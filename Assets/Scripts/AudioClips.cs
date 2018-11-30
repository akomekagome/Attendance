using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : Singleton<AudioClips>
{
    [SerializeField] private AudioClip bgm = null;
    [SerializeField] private AudioClip jumpSE = null;
    [SerializeField] private AudioClip hitSE = null;
    [SerializeField] private AudioClip chime = null;

    public AudioClip Bgm { get { return bgm; } }
    public AudioClip JumpSE { get { return jumpSE; } }
    public AudioClip HitSE { get { return hitSE; } }
    public AudioClip Chime { get { return chime; } }
}

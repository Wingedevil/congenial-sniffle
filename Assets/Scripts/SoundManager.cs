using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    public AudioSource Tick;
    public AudioSource Scrap;
    public AudioSource Repair;
    void Start()
    {

    }

    // Update is called once per frame
   public void PlayTick()
   {
       Tick.time = 0.12f;
       Tick.Play();
   }

   public void PlayScrap()
   {
       Scrap.Play();
   }

   public void PlayRepair()
   {
       Repair.Play();
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Manager<ParticleManager>
{
    public ParticleSystem WoodSys;
    public ParticleSystem SteelSys;
    public ParticleSystem GoldSys;
    // Start is called before the first frame update


    public void Burst(int wood, int steel, int gold, Vector3 pos)
    {
        transform.position = pos;
        ParticleSystem.Burst burst = WoodSys.emission.GetBurst(0);
        burst.count = wood;
        WoodSys.emission.SetBurst(0, burst);
        WoodSys.time = 0;
        WoodSys.Play();

        burst = new ParticleSystem.Burst();
        burst.count = steel;
        SteelSys.time = 0;
        SteelSys.emission.SetBurst(0, burst);
        SteelSys.Play();

        burst = new ParticleSystem.Burst();
        burst.count = gold;
        GoldSys.emission.SetBurst(0, burst);
        GoldSys.time = 0;
        GoldSys.Play();
    }
}

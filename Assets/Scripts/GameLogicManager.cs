using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : Manager<GameLogicManager>
{
    public Resources PlayerResources;
    public Upgrades PlayerUpgrades;

    public void IncreaseWood(int amt)
    {
        PlayerResources.Wood += amt;
    }

    public void IncreaseSteel(int amt)
    {
        PlayerResources.Steel += amt;
    }

    public void IncreaseGold(int amt)
    {
        PlayerResources.Gold += amt;
    }

    public void IncreaseHappiness(int amt)
    {
        PlayerResources.Happiness += amt;
    }

    public void IncreasePopulation(int amt)
    {
        PlayerResources.Population += amt;
    }

    public void IncreaseRiverSize(int amt)
    {
        PlayerResources.RiverSize += amt;
    }

    public void IncreaseStorage(int amt)
    {
        PlayerResources.Storage += amt;
    }

    public void IncreaseWoodCostReduction(int amt)
    {
        PlayerUpgrades.WoodCostReduction += amt;
    }

    public void IncreaseSteelCostReduction(int amt)
    {
        PlayerUpgrades.SteelCostReduction += amt;
    }

    public void IncreaseGoldCostReduction(int amt)
    {
        PlayerUpgrades.GoldCostReduction += amt;
    }
}

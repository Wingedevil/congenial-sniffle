using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : Manager<GameLogicManager>
{
    public Resources PlayerResources;
    public Upgrades PlayerUpgrades;
    public List<Card> PlayerStorage;
    public List<Card> PlayerRiver;

    public void MoveRiver()
    {
        // Remove last card
        if (PlayerRiver.Count > 0)
        {
            PlayerRiver.RemoveAt(PlayerRiver.Count - 1);
        }

        // draw from deck
    }

    public void ResetActions()
    {
        PlayerResources.Actions = 1 + PlayerResources.Population / 10;
    }

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

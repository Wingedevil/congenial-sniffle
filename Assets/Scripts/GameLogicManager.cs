using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameLogicManager : Manager<GameLogicManager>
{
    public Resources PlayerResources;
    public Upgrades PlayerUpgrades;
    public List<Card> PlayerAssets;
    public List<Card> PlayerRiver;

    public void MoveRiver()
    {
        // Remove last card
        if (PlayerRiver.Count >= PlayerResources.RiverSize)
        {
            DeckManager.Instance.DownStreamed(PlayerRiver[PlayerRiver.Count - 1]);
            PlayerRiver.RemoveAt(PlayerRiver.Count - 1);
        }

        // draw from deck
        Card drawnCard = DeckManager.Instance.Draw();
        PlayerRiver.Insert(0, drawnCard);
    }

    public void ResetActions()
    {
        PlayerResources.Actions = 1 + PlayerResources.Population / 10;
    }

    public void DiscardResources()
    {
        // we could display UI here or just do some automatic deduction
    }

    public void TapCard(Card card)
    {
        if (!card.IsTapped && CanUseEffects(card.OnAction))
        {
            UseEffects(card.OnAction);
            card.IsTapped = true;
        }

        if (PlayerResources.Actions == 0)
        {
            GameLoopManager.Instance.NoMoreActions();
        }
    }

    public void UntapCards()
    {
        foreach (Card card in PlayerAssets)
        {
            card.IsTapped = false;
        }
    }

    bool CanUseEffects(Effect[] effects)
    {
        int actionCost = 0;
        int woodCost = 0;
        int steelCost = 0;
        int goldCost = 0;
        foreach (Effect effect in effects)
        {
            actionCost += effect.ActionCost;
            woodCost += effect.WoodCost;
            steelCost += effect.SteelCost;
            goldCost += effect.GoldCost;
        }

        return (PlayerResources.Actions >= actionCost &&
            PlayerResources.Wood >= woodCost &&
            PlayerResources.Steel >= steelCost &&
            PlayerResources.Gold >= goldCost);
    }

    void UseEffects(Effect[] effects)
    {
        foreach (Effect effect in effects)
        {
            PlayerResources.Actions -= effect.ActionCost;
            PlayerResources.Wood -= effect.WoodCost;
            PlayerResources.Steel -= effect.SteelCost;
            PlayerResources.Gold -= effect.GoldCost;
            effect.ApplyEffect();
        }
    }

    public void TriggerEndOfTurnEffects()
    {
        foreach (Card card in PlayerAssets)
        {
            if (CanUseEffects(card.OnEndTurn))
            {
                UseEffects(card.OnEndTurn);
            }
        }
    }

    public void TryRepair(Card card)
    {
        Assert.IsTrue(PlayerRiver.Contains(card));
        // TODO: Ask about design for repair
        // Theres a chance we cant repair
        if (CanUseEffects(card.OnRepair))
        {
            UseEffects(card.OnRepair);
            int index = PlayerRiver.IndexOf(card);
            PlayerRiver[index] = null;
            PlayerAssets.Add(card);
            DeckManager.Instance.Repaired(card);
        }
    }

    public void Scrap(Card card)
    {
        Assert.IsTrue(PlayerRiver.Contains(card));
        int index = PlayerRiver.IndexOf(card);
        PlayerRiver[index] = null;
        DeckManager.Instance.Scrapped(card);
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

    public void IncreaseWoodScrapBonus(int amt)
    {
        PlayerUpgrades.WoodScrapBonus += amt;
    }

    public void IncreaseSteelScrapBonus(int amt)
    {
        PlayerUpgrades.SteelScrapBonus += amt;
    }

    public void IncreaseGoldScrapBonus(int amt)
    {
        PlayerUpgrades.GoldScrapBonus += amt;
    }
}

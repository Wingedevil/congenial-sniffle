using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameLogicManager : Manager<GameLogicManager> {
    public const float HAPPINESS_PER_POPULATION = 0.5f; // actually resources
    public const int NUMBER_OF_OBJECTIVES = 4;

    public int HutsBuilt = 0;
    public Resources PlayerResources = new Resources();
    public Upgrades PlayerUpgrades;
    public List<Card> PlayerAssets;
    public List<Card> PlayerRiver = new List<Card>();
    public List<Card> PlayerObjectives;

    public void PopulateObjectives() {
        for (int i = 0; i < NUMBER_OF_OBJECTIVES; ++i) {
            Objective oj = DeckManager.Instance.DrawObjective();
            if (!oj) {
                break;
            }
            PlayerObjectives.Add(oj);
        }
    }

    public void MoveRiver() {
        // draw from deck
        Card drawnCard = DeckManager.Instance.Draw();

        // Remove last card
        if (PlayerRiver.Count >= PlayerResources.RiverSize) {
            DeckManager.Instance.DownStreamed(PlayerRiver[PlayerRiver.Count - 1]);
            PlayerRiver.RemoveAt(PlayerRiver.Count - 1);
        }

        PlayerRiver.Insert(0, drawnCard);
        UIManager.Instance.DrawRiver();
    }

    public void ResetActions() {
        PlayerResources.Actions = 1;
    }

    public void DiscardResources() {
        // we could display UI here or just do some automatic deduction

        // todo refine?
        IncreaseWood(-1 * Mathf.Max(PlayerResources.Wood - PlayerResources.Storage, 0));
        IncreaseSteel(-1 * Mathf.Max(PlayerResources.Steel - PlayerResources.Storage, 0));
        IncreaseGold(-1 * Mathf.Max(PlayerResources.Gold - PlayerResources.Storage, 0));
    }

    public bool TapCard(Card card)
    {
        if (!card.IsTapped && CanUseEffects(card.OnAction) && PlayerResources.Actions > 0)
        {
            UseEffects(card.OnAction);
            card.IsTapped = true;
            PlayerResources.Actions--;
            UIManager.Instance.UpdateResources();

            if (PlayerResources.Actions == 0) {
                GameLoopManager.Instance.NoMoreActions();
            }
            return true;
        }
        return false;
    }

    public void UntapCards() {
        foreach (Card card in PlayerAssets) {
            card.IsTapped = false;
        }
    }

    bool CanUseEffects(Effect[] effects) {
        int actionCost = 0;
        int woodCost = 0;
        int steelCost = 0;
        int goldCost = 0;
        foreach (Effect effect in effects) {
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

    void UseEffects(Effect[] effects) {
        foreach (Effect effect in effects) {
            PlayerResources.Actions -= effect.ActionCost;
            PlayerResources.Wood -= effect.WoodCost;
            PlayerResources.Steel -= effect.SteelCost;
            PlayerResources.Gold -= effect.GoldCost;
            effect.ApplyEffect();
        }
        UIManager.Instance.UpdateResources();
    }

    public void TriggerEndOfTurnEffects() {
        foreach (Card card in PlayerAssets) {
            if (CanUseEffects(card.OnEndTurn)) {
                UseEffects(card.OnEndTurn);
            }
        }
    }

    public void HappinessUpkeep() {
        IncreaseHappiness(Mathf.FloorToInt(HAPPINESS_PER_POPULATION * PlayerResources.Population * -1));
        PlayerResources.Happiness = Mathf.Min(PlayerResources.Happiness, 100);
    }

    public void CheckLoseCond() {
        if (PlayerResources.Happiness <= 0) {
            LoseTheGame();
        }
    }

    public void LoseTheGame() {
        // todo
        Debug.Log("You Lose");
    }

    public void TryRepair(Card card) {
        Assert.IsTrue(PlayerRiver.Contains(card) || card is Objective || card is Hut);
        // TODO: Ask about design for repair
        // Theres a chance we cant repair
        if (PlayerResources.Actions > 0 && CanUseEffects(card.OnRepair) && CanRepair(card)) {
            PlayerResources.Wood -= card.RepairWoodCost;
            PlayerResources.Steel -= card.RepairSteelCost;
            PlayerResources.Gold -= card.RepairGoldCost;
            UseEffects(card.OnRepair);

            if (card is Hut) {
                ++HutsBuilt;
                PlayerAssets.Add(card);
            } else if (card is Objective) {
                int index2 = PlayerObjectives.IndexOf(card);
                PlayerObjectives[index2] = null;
                PlayerAssets.Add(card);
            } else {
                DeckManager.Instance.Repaired(card);
                int index = PlayerRiver.IndexOf(card);
                PlayerRiver[index] = null;
                PlayerAssets.Add(card);
            }

            UIManager.Instance.DrawRiver();
            UIManager.Instance.DrawAssets();
            PlayerResources.Actions--;
            UIManager.Instance.UpdateResources();
        }

        if (PlayerResources.Actions == 0)
        {
            GameLoopManager.Instance.NoMoreActions();
        }
    }

    public bool CanRepair(Card card) {
        return (PlayerResources.Wood >= card.RepairWoodCost &&
            PlayerResources.Steel >= card.RepairSteelCost &&
            PlayerResources.Gold >= card.RepairGoldCost);
    }
    public bool Scrap(Card card)
    {
        // Assert.IsTrue(PlayerRiver.Contains(card));
        if (card.Scrappable && PlayerResources.Actions > 0)
        {
            int index = PlayerRiver.IndexOf(card);
            PlayerRiver[index] = null;
            DeckManager.Instance.Scrapped(card);
            UIManager.Instance.DrawRiver();
            PlayerResources.Actions--;
            PlayerResources.Wood += card.ScrapWoodCost != 0 ? card.ScrapWoodCost + PlayerUpgrades.WoodScrapBonus : 0;
            PlayerResources.Steel += card.ScrapSteelCost != 0 ? card.ScrapSteelCost + PlayerUpgrades.SteelScrapBonus : 0;
            PlayerResources.Gold += card.ScrapGoldCost != 0 ? card.ScrapGoldCost + PlayerUpgrades.GoldScrapBonus : 0;
            UIManager.Instance.UpdateResources();

            if (PlayerResources.Actions == 0)
            {
                GameLoopManager.Instance.NoMoreActions();
            }
            return true;
        }
        return false;
    }

    public void IncreaseWood(int amt) {
        PlayerResources.Wood += amt;
    }

    public void IncreaseSteel(int amt) {
        PlayerResources.Steel += amt;
    }

    public void IncreaseGold(int amt) {
        PlayerResources.Gold += amt;
    }

    public void IncreaseHappiness(int amt) {
        PlayerResources.Happiness += amt;
    }

    public void IncreasePopulation(int amt) {
        PlayerResources.Population += amt;
    }

    public void IncreaseRiverSize(int amt) {
        PlayerResources.RiverSize += amt;
    }

    public void IncreaseStorage(int amt) {
        PlayerResources.Storage += amt;
    }

    public void IncreaseWoodCostReduction(int amt) {
        PlayerUpgrades.WoodCostReduction += amt;
    }

    public void IncreaseSteelCostReduction(int amt) {
        PlayerUpgrades.SteelCostReduction += amt;
    }

    public void IncreaseGoldCostReduction(int amt) {
        PlayerUpgrades.GoldCostReduction += amt;
    }

    public void IncreaseWoodScrapBonus(int amt) {
        PlayerUpgrades.WoodScrapBonus += amt;
    }

    public void IncreaseSteelScrapBonus(int amt) {
        PlayerUpgrades.SteelScrapBonus += amt;
    }

    public void IncreaseGoldScrapBonus(int amt) {
        PlayerUpgrades.GoldScrapBonus += amt;
    }
}

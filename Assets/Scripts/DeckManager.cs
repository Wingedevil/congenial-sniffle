using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Manager<DeckManager>
{
    public const int TURNS_BEFORE_REAPPEAR = 3;
    public int[] TIER_REQUIREMENTS = new []{0, 10, 20};
    public Card[] JunkCards;

    public Card[] DeckOfCards;

    public List<Objective> ObjectiveCards;

    // from downstreamed or scrapped
    private List<Card> deadCards = new List<Card>();
    private Queue<List<Card>> forbiddenCards = new Queue<List<Card>>();

    public override void Awake() {
        base.Awake();
        forbiddenCards.Enqueue(new List<Card>());
        forbiddenCards.Enqueue(new List<Card>());
        forbiddenCards.Enqueue(new List<Card>());
    }

    public Objective DrawObjective() {
        if (ObjectiveCards.Count <= 0) {
            return null;
        }

        Objective drawn = ObjectiveCards[Random.Range(0, ObjectiveCards.Count)];
        ObjectiveCards.Remove(drawn);
        return drawn;
    }

    public Card Draw() {
        List<Card> river = GameLogicManager.Instance.PlayerRiver;
        int builtStuffs = GameLogicManager.Instance.PlayerAssets.Count;
        List<Card> finalDeck = new List<Card>();
        foreach(Card c in DeckOfCards) {
            // no duplicates
            if (river.Contains(c)) {
                continue;
            }

            // tier requirement
            if (builtStuffs < TIER_REQUIREMENTS[c.Tier]) {
                continue;
            }
            
            // 3 turn cooldown
            foreach (List<Card> list in forbiddenCards) {
                if (list.Contains(c)) {
                    continue;
                }
            }

            // no more than 2
            if (deadCards.Contains(c)) {
                continue;
            }

            finalDeck.Add(c);
        }
        forbiddenCards.Dequeue();
        forbiddenCards.Enqueue(new List<Card>());

        if (finalDeck.Count > 0) {
            return finalDeck[Random.Range(0, finalDeck.Count)];
        } else {
            return JunkCards[Random.Range(0, JunkCards.Length)];
        }
    }

    public void DownStreamed(Card cd) {
        List<Card> list = null;
        foreach (List<Card> inlist in forbiddenCards) {
            list = inlist;
        }

        list.Add(cd);
    }

    public void Scrapped(Card cd) {
        List<Card> list = null;
        foreach (List<Card> inlist in forbiddenCards) {
            list = inlist;
        }

        list.Add(cd);
    }

    public void Repaired(Card cd) {
        if (GameLogicManager.Instance.PlayerAssets.Contains(cd)) {
            deadCards.Add(cd);
        }
    }
}

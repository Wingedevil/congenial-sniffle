using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Manager<DeckManager>
{
    public const int TURNS_BEFORE_REAPPEAR = 3;
    public int[] TIER_REQUIREMENTS;
    public Card[] JunkCards;

    public Card[] DeckOfCards;

    public List<Objective> ObjectiveCards;

    // owned cards
    private Hashtable deadCards = new Hashtable();

    // from downstreamed or scrapped
    private Queue<List<Card>> forbiddenCards = new Queue<List<Card>>();

    public override void Awake() {
        base.Awake();
        for (int i = 0; i < TURNS_BEFORE_REAPPEAR; ++i) {
            forbiddenCards.Enqueue(new List<Card>());
        }
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
        foreach (Card c in DeckOfCards) {
            // no duplicates
            if (river.Contains(c)) {
                continue;
            }

            // tier requirement
            if (builtStuffs < TIER_REQUIREMENTS[c.Tier]) {
                continue;
            }

            // turn cooldown
            foreach (List<Card> list in forbiddenCards) {
                if (list.Contains(c)) {
                    goto end;
                }
            }

            // no more than n cards
            if ((int)(deadCards.ContainsKey(c.ID) ? deadCards[c.ID] : 0) >= c.Copies) {
                continue;
            }

            finalDeck.Add(c);
        end:
            continue;
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
        foreach (List<Card> inlist in forbiddenCards) {
            inlist.Add(cd);
        }
    }

    public void Scrapped(Card cd) {
        foreach (List<Card> inlist in forbiddenCards) {
            inlist.Add(cd);
        }
    }

    public void Repaired(Card cd) {
        List<Card> list = null;
        foreach (List<Card> inlist in forbiddenCards) {
            list = inlist;
        }
        list.Add(cd);
        deadCards[cd.ID] = (int)(deadCards.ContainsKey(cd.ID) ? deadCards[cd.ID] : 0) + 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class UICard : MonoBehaviour
{
    public Card CardData;
    public Sprite DefaultSprite;
    public SpriteRenderer CardImage;
    public TextMeshProUGUI Name;

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        CardImage.sprite = CardData.Image == null ? DefaultSprite : CardData.Image;
        Name.text = CardData.Name;
    }
}

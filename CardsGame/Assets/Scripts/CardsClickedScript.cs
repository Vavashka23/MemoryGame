using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsClickedScript : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneControllerScript controller;

    private int _id;

    public int id
    {
        get { return _id; }
    }

    public void SetBackCardActive()
    {
        cardBack.SetActive(true);
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.SecondCardIsOpened)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }
}

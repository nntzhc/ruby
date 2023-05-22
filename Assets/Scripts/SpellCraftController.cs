using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCraftController : MonoBehaviour
{
    private CanvasGroup runeCanvasGroup;
    public GameObject player;


    void Start()
    {
        gameObject.SetActive(false);
        runeCanvasGroup = gameObject.GetComponent<CanvasGroup>();
        runeCanvasGroup.alpha = 0;
        runeCanvasGroup.interactable = false;
        runeCanvasGroup.blocksRaycasts = false;
    }

    void Update()
    {

    }
}

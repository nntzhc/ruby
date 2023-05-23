using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{

    public GameObject SpellCraft;
    private bool isRuneUIOpen = false;
    private CanvasGroup runeCanvasGroup;
    // Start is called before the first frame update
    public GameObject player;
    public GameObject runePanel;
    private Vector3 panelInitialPosition;
    void Start()
    {
        runeCanvasGroup = SpellCraft.GetComponent<CanvasGroup>();
        panelInitialPosition = runePanel.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isRuneUIOpen)
            {
                // Debug.Log("SpellCraft set active");
                SpellCraft.SetActive(true);
                isRuneUIOpen = true;
                Time.timeScale = 0;
                runeCanvasGroup.alpha = 1;
                runeCanvasGroup.interactable = true;
                runeCanvasGroup.blocksRaycasts = true;
                Vector3 playerPosition = player.transform.position;
                runeCanvasGroup.transform.position = playerPosition + new Vector3(0, 1, 0);
                // runeCanvasGroup.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                // Debug.Log(runeCanvasGroup.transform.position);
                // Debug.Log(player.transform.position);
                // 设置符文面板在屏幕中央
                Vector3 panelPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 10f));
                runePanel.transform.position = panelPosition;
                ResetRunePanelPosition();

            }
            else
            {
                // Debug.Log("SpellCraft set deactive");
                SpellCraft.SetActive(false);
                isRuneUIOpen = false;
                Time.timeScale = 1;
                runeCanvasGroup.alpha = 0;
                runeCanvasGroup.interactable = false;
                runeCanvasGroup.blocksRaycasts = false;
            }
        }
    }

    public void ResetRunePanelPosition()
    {
        // 重置符文面板位置
        runePanel.transform.localPosition = panelInitialPosition;
    }
}

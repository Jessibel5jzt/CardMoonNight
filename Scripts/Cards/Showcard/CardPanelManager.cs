using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanelManager : MonoBehaviour
{
    public GameObject CardPanel;


    void Start()
    {
        AchieveUIManager.Instance.AddEventListener(10100, ShowCardPanel);
    }

    public void ShowCardPanel(string s)
    {
        GameObject C_CardPanel = Instantiate(CardPanel, this.transform);
        C_CardPanel.name = s;
        C_CardPanel.AddComponent<ShowBigCard>();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI turnNumberText;

    [SerializeField]
    private Button endTurnButton;

    [SerializeField]
    private GameObject enemyTurnVisualGamObject;

    void Start()
    {
        endTurnButton
            .onClick
            .AddListener(() =>
            {
                TurnSystem.Instance.NextTurn();
            });

        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;

        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEnndTurnButtonVisibility();
    }

    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEnndTurnButtonVisibility();
    }

    private void UpdateTurnText()
    {
        turnNumberText.text = "Turn " + TurnSystem.Instance.GetTurnNumber();
    }

    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualGamObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEnndTurnButtonVisibility()
    {
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}

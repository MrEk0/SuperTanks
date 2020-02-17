using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] GameObject levelButtonPrefab;
    [SerializeField] GameObject buttonPanelPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] int numberOfLevels;

    Rect panelRect;
    int numberPerPage;
    int levelCount = 0;

    private void Start()
    {
        CreateLevelPanel();
    }

    private void CreateLevelPanel()
    {
        panelRect = buttonPanelPrefab.GetComponent<RectTransform>().rect;
        Rect levelRect = levelButtonPrefab.GetComponent<RectTransform>().rect;
        Vector2 spacing = buttonPanelPrefab.GetComponent<GridLayoutGroup>().spacing;

        int maxInARow = Mathf.FloorToInt((panelRect.width+spacing.x) / (levelRect.width+spacing.x));
        int maxInAColumn = Mathf.FloorToInt((panelRect.height+spacing.y) / (levelRect.height+spacing.y));
        numberPerPage = maxInARow * maxInAColumn;
        int numberOfPages = Mathf.CeilToInt((float)numberOfLevels / numberPerPage);

        LoadPanels(numberOfPages);
    }

    private void LoadPanels(int numberOfPages)
    {
        for (int i = 0; i <= numberOfPages-1; i++)
        {
            GameObject panel = Instantiate(buttonPanelPrefab) as GameObject;
            panel.transform.SetParent(canvas.transform, false);
            panel.transform.SetParent(transform);
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelRect.width * (i), 0);
            LoadButtons(numberPerPage, panel);
        }
    }

    private void LoadButtons(int numberOfButtons, GameObject parent)
    {
        for(int i=0; i<=numberOfButtons-1; i++)
        {
            levelCount++;
            GameObject button = Instantiate(levelButtonPrefab);
            button.transform.SetParent(canvas.transform, false);
            button.transform.SetParent(parent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(levelCount.ToString());
        }
    }
}

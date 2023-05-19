using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private Button upgradeButton;
    private GameObject towerToUpgrade;

    private void OnEnable()
    {
        towerToUpgrade = selectionManager.selectedObject.GetGameObject();
        SpriteRenderer sr = towerToUpgrade.GetComponentInChildren<SpriteRenderer>();
        Sprite sprite = sr.sprite;
        towerImage.sprite = sprite;
        upgradeText.text = towerToUpgrade.GetComponent<BaseTower>().towerStatData.costToUpgrade.ToString();
        if (towerToUpgrade.GetComponent<BaseTower>().canUpgrade)
        {
            upgradeText.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(true);
        }
        else
        {
            upgradeText.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(false);
        }
    }
}

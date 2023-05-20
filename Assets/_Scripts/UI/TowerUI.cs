using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private Button upgradeButton;
    private GameObject towerToUpgrade;
    private BaseTower tower;
    private PlayerStats playerStats;

    private void Awake()
    {
        upgradeButton.onClick.AddListener(OnUpgradeClick);
        playerStats = GameObject.FindWithTag("PlayerStat").GetComponent<PlayerStats>();
    }
    private void OnEnable()
    {
        towerToUpgrade = selectionManager.selectedObject.GetGameObject();
        tower = towerToUpgrade.GetComponent<BaseTower>();
        SpriteRenderer sr = tower.upgradeTowers[tower.upgradeLevel].GetComponent<SpriteRenderer>();
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;
        Sprite sprite = sr.sprite;
        towerImage.sprite = sprite;
        towerImage.rectTransform.sizeDelta = new Vector2(spriteWidth * 100, spriteHeight * 100);
        upgradeText.text = tower.towerStatData.costToUpgrade.ToString();
        towerName.text = tower.towerStatData.towerName;
        if (tower.canUpgrade)
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

    private void Update()
    {
        if (playerStats.CurrentCoins < tower.towerStatData.costToUpgrade)
        {
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButton.interactable = true;
        }
    }

    private void OnUpgradeClick()
    {
        if (tower.canUpgrade)
        {
            tower.UpgradeTower(tower.upgradeLevel + 1);
        }
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}

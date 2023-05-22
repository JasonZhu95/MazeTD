using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour, ISelectable
{
    public TowerStatData towerStatData;
    [SerializeField] private GameObject rangeIndicator;
    public GameObject TowerUpgradeCanvas { get; set; }

    protected float timeSinceLastFire = 0f;
    private Vector3 transformOffset = new Vector3(.5f, .5f, 0);
    private GameObject finalEnemyDestination;
    public bool canUpgrade;
    public int upgradeLevel = 0;
    [SerializeField] public GameObject[] upgradeTowers;
    [SerializeField] private TowerStatData[] upgradeTowerData;
    private PlayerStats playerStats;
    public int currentValue = 0;

    public bool isActive { get; set; } = false;

    private void Awake()
    {
        playerStats = GameObject.FindWithTag("PlayerStat").GetComponent<PlayerStats>();
        finalEnemyDestination = GameObject.FindWithTag("EnemyTarget");
        rangeIndicator.transform.localScale = new Vector3(towerStatData.range * 2, towerStatData.range * 2, 1f);
        currentValue = towerStatData.cost;
    }

    protected Enemy FindTarget()
    {
        if (isActive)
        {
            float shortestDistance = Mathf.Infinity;
            int closestEnemyIndex = 0;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + transformOffset, towerStatData.range);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Enemy enemy = hitColliders[i].GetComponent<Enemy>();
                if (enemy != null)
                {
                    float tempDistance = Vector3.Distance(finalEnemyDestination.transform.position, enemy.gameObject.transform.position);
                    if (tempDistance < shortestDistance)
                    {
                        shortestDistance = tempDistance;
                        closestEnemyIndex = i;
                    }
                }
            }
            return hitColliders[closestEnemyIndex].GetComponent<Enemy>();
        }
        else
        {
            return null;
        }
    }

    public void OnSelect()
    {
        rangeIndicator.SetActive(true);
        TowerUpgradeCanvas.SetActive(true);
    }

    public void OnDeselect()
    {
        rangeIndicator.SetActive(false);
        TowerUpgradeCanvas.SetActive(false);
    }

    public void DisableRangeIndicator()
    {
        rangeIndicator.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void UpgradeTower(int Level)
    {
        FindObjectOfType<SoundManager>().Play("upgradeBuilding");
        currentValue += towerStatData.costToUpgrade;
        playerStats.DeductCoins(towerStatData.costToUpgrade);
        foreach (GameObject upgradeTower in upgradeTowers)
        {
            upgradeTower.SetActive(false);
        }
        upgradeTowers[Level].SetActive(true);
        towerStatData = upgradeTowerData[Level];
        rangeIndicator.transform.localScale = new Vector3(towerStatData.range * 2, towerStatData.range * 2, 1f);
        upgradeLevel = Level;
        if (upgradeLevel == 2)
        {
            canUpgrade = false;
        }
    }
}

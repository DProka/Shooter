using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform enemyParent;

    private PlayerController player;
    private List<EnemyGeneral> enemyList;

    public void Init(PlayerController _player)
    {
        player = _player;

        enemyList = new List<EnemyGeneral>();

        for (int i = 0; i < enemyParent.childCount; i++)
        {
            enemyList.Add(enemyParent.GetChild(i).GetComponent<EnemyGeneral>());
            enemyList[i].Init(player);
        }
    }

    public void UpdateEnemies()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].GetAliveStatus())
                enemyList[i].UpdateEnemy();
            else
                RemoveEnemyFromList(enemyList[i]);
        }
    }

    public void RemoveEnemyFromList(EnemyGeneral enemy) { enemyList.Remove(enemy); }

    public List<EnemyGeneral> GetEnemyList() { return enemyList; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private EnemyController enemyController;

    [SerializeField] float damageDistance;

    private List<PlayerBulletScript> bulletList;

    public void Init(EnemyController enemies)
    {
        bulletList = new List<PlayerBulletScript>();

        enemyController = enemies;
    }

    public void UpdateController()
    {
        if(bulletList.Count > 0)
        {
            foreach (PlayerBulletScript bullet in bulletList)
            {
                bullet.UpdateBullet();
                CheckDistanceToEnemy(bullet);
            }
        }
    }

    public void AddBulletToList(PlayerBulletScript bullet)
    {
        bulletList.Add(bullet);
    }

    public void RemoveBulletFromList(PlayerBulletScript bullet)
    {
        bulletList.Remove(bullet);
    }

    private void CheckDistanceToEnemy(PlayerBulletScript bullet)
    {
        List<EnemyGeneral> enemies = enemyController.GetEnemyList();

        if (enemies.Count > 0)
        {
            Vector2 bulletPos = new Vector2(bullet.transform.position.x, bullet.transform.position.z);
         
            for (int i = 0; i < enemies.Count; i++)
            {
                Vector2 enemyPos = new Vector2(enemies[i].transform.position.x, enemies[i].transform.position.z);
                float currentDist = Vector2.Distance(bulletPos, enemyPos);
                //float currentDist = Vector2.Distance(bullet.transform.position, enemies[i].transform.position);

                if (currentDist < bullet.GetDistance())
                {
                    enemies[i].GetDamage(bullet.GetDamage());
                    bullet.DestroyBullet();
                }
            }
        }
    }

    public List<PlayerBulletScript> GetBulletList() { return bulletList; }
}

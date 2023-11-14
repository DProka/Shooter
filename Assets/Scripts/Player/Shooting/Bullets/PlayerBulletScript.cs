using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] float damage = 3f;
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 3f;
    [SerializeField] float distanceToDamage = 0.3f;
    [SerializeField] LayerMask groundLayer;

    private PlayerBulletController bulletController;

    public void Init(PlayerBulletController controller, Transform firepoint)
    {
        bulletController = controller;
        bulletController.AddBulletToList(this);

        Vector3 forward = firepoint.forward;
        transform.forward = forward;

        MoveBullet();
    }
    
    public void UpdateBullet()
    {
        MoveBullet();

        Destroy(gameObject, lifetime);
    }

    public float GetDamage() { return damage; }

    public float GetDistance() { return distanceToDamage; }

    public void DestroyBullet() { Destroy(gameObject); }

    private void MoveBullet()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        bulletController.RemoveBulletFromList(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулась ли пуля со слоем "Ground"
        if (LayerMaskMatchesObject(groundLayer, other.gameObject))
        {
            // Уничтожаем пулю при столкновении со слоем "Ground"
            Destroy(gameObject);
        }
    }

    private bool LayerMaskMatchesObject(LayerMask layerMask, GameObject obj)
    {
        // Проверяем, принадлежит ли объект указанному слою
        return (layerMask.value & (1 << obj.layer)) != 0;
    }
}

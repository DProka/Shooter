using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] Material originalMaterial;
    [SerializeField] Material transparentMaterial;
    [SerializeField] float boxCastDistance = 10f;
    [SerializeField] LayerMask obstacleLayer;

    private Vector3 playerPosition;
    private bool isTransparent = false;

    public void Init(Vector3 _playerPosition)
    {
        playerPosition = _playerPosition;
        // Сохраняем оригинальный материал объекта
        //originalMaterial = GetComponent<Renderer>().material;
    }

    public void UpdateController()
    {
        // Получаем позицию основной камеры и персонажа
        Vector3 cameraPosition = Camera.main.transform.position;
        playerPosition.y = 0.5f;

        // Выполняем box cast для определения преград между камерой и персонажем
        RaycastHit hit;
        bool isHit = Physics.BoxCast(playerPosition, Vector3.one * 0.5f, cameraPosition - playerPosition, out hit, Quaternion.identity, boxCastDistance, obstacleLayer);

        // Отображаем линию в окне сцены
        Debug.DrawLine(playerPosition, playerPosition + (cameraPosition - playerPosition).normalized * boxCastDistance, isHit ? Color.red : Color.green);

        Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

        if (isHit)
        {
            hitRenderer.material = transparentMaterial;
            // Если есть преграда, изменяем материал на полупрозрачный
            if (!isTransparent)
            {
                SetTransparentMaterial();
            }
        }
        else
        {
            // Если преграды нет, возвращаем оригинальный материал
            if (isTransparent)
            {
                SetOriginalMaterial();
            }
        }
    }

    private void SetTransparentMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = transparentMaterial;
        isTransparent = true;
    }

    private void SetOriginalMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = originalMaterial;
        isTransparent = false;
    }
}

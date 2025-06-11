using System;
using System.Collections;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField]
    private float hatchTime = 2f;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartHatch()
    {
        StartCoroutine(HatchRoutine());
    }

    private IEnumerator HatchRoutine()
    {
        print("HatchRoutine");
        yield return new WaitForSeconds(hatchTime);
        transform
            .DOShakeRotation(0.5f, strength: new Vector3(0, 0, 30), vibrato: 10)
            .SetEase(Ease.OutQuad);

        transform
            .DOScale(1.1f, 0.125f)
            .SetLoops(4, LoopType.Yoyo);
        yield return new WaitForSeconds(0.5f);

        Digimon digimon = GameManager.Data.GetRandomDigimon();

        _spriteRenderer.DOFade(0f, 0.1f).OnComplete(() =>
        {
            // GameObject digimon = Instantiate( /* Your Digimon prefab */, tile.transform.position, Quaternion.identity);
            _spriteRenderer.sprite = GameManager.Resources.GetResources<Sprite>("Images/Digimon/koromon");
            _spriteRenderer.DOFade(1f, 0.2f);
        });


        // GameManager.AllyUnit.SpawnAlly(digimon, transform.position);

        // Destroy(gameObject); // 알 제거
    }
}
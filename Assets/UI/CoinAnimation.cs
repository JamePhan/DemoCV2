using DG.Tweening;
using UnityEngine;

public class CoinAnimation : Singleton<CoinAnimation>
{
    public RectTransform canvasTransform;   
    public GameObject coinPrefab;          
    public RectTransform targetPosition;    

    public void AnimateCoin(Transform enemyTransform)
    {
        GameObject coin = Instantiate(coinPrefab, canvasTransform);
        RectTransform coinRect = coin.GetComponent<RectTransform>();

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(enemyTransform.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, screenPosition, Camera.main, out Vector2 canvasPosition);

        coinRect.anchoredPosition = canvasPosition;

        coinRect.DOAnchorPos(targetPosition.anchoredPosition, 1f).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                Destroy(coin); 
            });
    }


}

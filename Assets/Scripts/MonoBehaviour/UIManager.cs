using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI gameTitle;
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI reviewsText;
    public TextMeshProUGUI ratingText;
    public Image backgroundImage;
    public Image totalTimeImage;
    public Image priceImage;
    public Image reviewsImage;
    public Image ratingImage;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 0.5f;
    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindFirstObjectByType<UIManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Fresh()
    {
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
        }
        if (fadeOutCoroutine == null)
        {
            fadeOutCoroutine = StartCoroutine(fadeOut());
        }
        fadeOutCoroutine = StartCoroutine(fadeOut());
    }
    public IEnumerator fadeIn()
    {
        float elapsedTime = 0;
        Color gameTitleStartColor = gameTitle.color;
        Color gameTitleEndColor = gameTitleStartColor;
        gameTitleEndColor.a = 1;
        RectTransform totalTimeTextRectTransform = totalTimeText.GetComponent<RectTransform>();
        float totalTimeFactor = Mathf.Clamp(GameManager.Instance.currentData.totalTime / 100f, 0f, 1f);
        Vector2 targetHight = new Vector2(0f, totalTimeTextRectTransform.parent.GetComponent<RectTransform>().rect.height * totalTimeFactor);
        Color totalTimeStartColor = Color.green;
        Color totalTimeEndColor = (Color)(((Vector4)Color.yellow - (Vector4)Color.green) * totalTimeFactor + (Vector4)Color.green);
        RectTransform priceTextRectTransform = priceText.GetComponent<RectTransform>();
        float priceFactor = Mathf.Clamp(GameManager.Instance.currentData.price / 70f, 0f, 1f);
        Vector2 targetPriceHight = new Vector2(0f, priceTextRectTransform.parent.GetComponent<RectTransform>().rect.height * priceFactor);
        Color priceStartColor = Color.green;
        Color priceEndColor = (Color)((Vector4)Color.red * priceFactor + (Vector4)Color.green * (1f - priceFactor));
        RectTransform reviewsTextRectTransform = reviewsText.GetComponent<RectTransform>();
        float reviewsFactor = Mathf.Clamp((float)GameManager.Instance.currentData.reviews / 10000f, 0f, 1f);
        Vector2 targetReviewsHight = new Vector2(0f, reviewsTextRectTransform.parent.GetComponent<RectTransform>().rect.height * reviewsFactor);
        Color reviewsStartColor = Color.gray;
        Color reviewsEndColor = (Color)(((Vector4)Color.cyan - (Vector4)Color.gray) * reviewsFactor + (Vector4)Color.gray);
        float ratingFactor = Mathf.Clamp(GameManager.Instance.currentData.rating / 100f, 0f, 1f);
        Color ratingStartColor = Color.red;
        Color ratingEndColor = new Color(0f, 0.86f, 1f, 1f);
        ratingEndColor = (Color)(((Vector4)ratingEndColor - (Vector4)ratingStartColor) * ratingFactor + (Vector4)ratingStartColor);
        Color backgroundStartColor = backgroundImage.color;
        Color backgroundEndColor = backgroundStartColor;
        backgroundEndColor.a = 1;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            gameTitle.color = Color.Lerp(gameTitleStartColor, gameTitleEndColor, elapsedTime / fadeInDuration);
            totalTimeTextRectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, targetHight, elapsedTime / fadeInDuration);
            totalTimeText.text = $"{Mathf.Lerp(0f, GameManager.Instance.currentData.totalTime, elapsedTime / fadeInDuration):F1}h";
            totalTimeText.color = Color.Lerp(totalTimeStartColor, totalTimeEndColor, elapsedTime / fadeInDuration);
            totalTimeImage.fillAmount = Mathf.Lerp(0f, totalTimeFactor, elapsedTime / fadeInDuration);
            totalTimeImage.color = Color.Lerp(totalTimeStartColor, totalTimeEndColor, elapsedTime / fadeInDuration);
            priceTextRectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, targetPriceHight, elapsedTime / fadeInDuration);
            priceText.text = $"${Mathf.Lerp(0f, GameManager.Instance.currentData.price, elapsedTime / fadeInDuration):F2}";
            priceText.color = Color.Lerp(priceStartColor, priceEndColor, elapsedTime / fadeInDuration);
            priceImage.fillAmount = Mathf.Lerp(0f, priceFactor, elapsedTime / fadeInDuration);
            priceImage.color = Color.Lerp(priceStartColor, priceEndColor, elapsedTime / fadeInDuration);
            reviewsTextRectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, targetReviewsHight, elapsedTime / fadeInDuration);
            reviewsText.text = $"{Mathf.Lerp(0, GameManager.Instance.currentData.reviews, elapsedTime / fadeInDuration):F0}";
            reviewsText.color = Color.Lerp(reviewsStartColor, reviewsEndColor, elapsedTime / fadeInDuration);
            reviewsImage.fillAmount = Mathf.Lerp(0f, reviewsFactor, elapsedTime / fadeInDuration);
            reviewsImage.color = Color.Lerp(reviewsStartColor, reviewsEndColor, elapsedTime / fadeInDuration);
            ratingText.text = $"{Mathf.Lerp(0f, GameManager.Instance.currentData.rating, elapsedTime / fadeInDuration):F1}%";
            ratingText.color = Color.Lerp(ratingStartColor, ratingEndColor, elapsedTime / fadeInDuration);
            ratingImage.fillAmount = Mathf.Lerp(0f, GameManager.Instance.currentData.rating / 100f, elapsedTime / fadeInDuration);
            ratingImage.color = Color.Lerp(ratingStartColor, ratingEndColor, elapsedTime / fadeInDuration);
            backgroundImage.color = Color.Lerp(backgroundStartColor, backgroundEndColor, elapsedTime / fadeInDuration);
            yield return null;

        }
    }
    public IEnumerator fadeOut()
    {
        float elapsedTime = 0;
        Color gameTitleStartColor = gameTitle.color;
        Color gameTitleEndColor = gameTitleStartColor;
        gameTitleEndColor.a = 0;
        Color totalTimeStartColor = totalTimeText.color;
        Color totalTimeEndColor = totalTimeStartColor;
        totalTimeEndColor.a = 0;
        Color priceStartColor = priceText.color;
        Color priceEndColor = priceStartColor;
        priceEndColor.a = 0;
        Color reviewsStartColor = reviewsText.color;
        Color reviewsEndColor = reviewsStartColor;
        reviewsEndColor.a = 0;
        Color ratingStartColor = ratingText.color;
        Color ratingEndColor = ratingStartColor;
        ratingEndColor.a = 0;
        Color backgroundStartColor = backgroundImage.color;
        Color backgroundEndColor = backgroundStartColor;
        backgroundEndColor.a = 0;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            gameTitle.color = Color.Lerp(gameTitleStartColor, gameTitleEndColor, elapsedTime / fadeOutDuration);
            totalTimeText.color = Color.Lerp(totalTimeStartColor, totalTimeEndColor, elapsedTime / fadeOutDuration);
            totalTimeImage.color = Color.Lerp(totalTimeStartColor, totalTimeEndColor, elapsedTime / fadeOutDuration);
            priceText.color = Color.Lerp(priceStartColor, priceEndColor, elapsedTime / fadeOutDuration);
            priceImage.color = Color.Lerp(priceStartColor, priceEndColor, elapsedTime / fadeOutDuration);
            reviewsText.color = Color.Lerp(reviewsStartColor, reviewsEndColor, elapsedTime / fadeOutDuration);
            reviewsImage.color = Color.Lerp(reviewsStartColor, reviewsEndColor, elapsedTime / fadeOutDuration);
            ratingText.color = Color.Lerp(ratingStartColor, ratingEndColor, elapsedTime / fadeOutDuration);
            ratingImage.color = Color.Lerp(ratingStartColor, ratingEndColor, elapsedTime / fadeOutDuration);
            backgroundImage.color = Color.Lerp(backgroundStartColor, backgroundEndColor, elapsedTime / fadeOutDuration);
            yield return null;

        }
        yield return new WaitForSeconds(0.5f);
        UpdateFixedDisplayInfo();
        fadeInCoroutine = StartCoroutine(fadeIn());
    }

    public void UpdateFixedDisplayInfo()
    {
        gameTitle.text = GameManager.Instance.currentData.gameName;
        backgroundImage.sprite = GameManager.Instance.currentData.sprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KamishibaiController : MonoBehaviour
{
    // Unityエディタから画像を割り当てるための配列
    [SerializeField] private Sprite[] kamishibaiImages;

    // UIのImageコンポーネント
    private Image displayImage;

    // 現在表示中の画像のインデックス
    private int currentImageIndex = 0;

    // プレイヤーの得点
    private int score = 0;

    // 各画像で得点が入ったかを判定するフラグ
    private bool hasScoredOnCurrentImage = false;

    [SerializeField] private AudioClip switchSound; // 効果音をInspectorでセット
    [SerializeField] private AudioClip buttonSound; // ボタン用効果音をInspectorでセット
    private AudioSource audioSource; // AudioSource参照

    [SerializeField] private TMP_Text scoreText; // スコア表示用TextMeshPro

    private Coroutine scoreChangeCoroutine; // スコア表示用コルーチン参照

    private void Start()
    {
        // Imageコンポーネントを取得
        displayImage = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        UpdateScoreText(""); // 初期スコア表示

        // Coroutineを開始して、画像の切り替え処理を始める
        StartCoroutine(PlayKamishibai());
    }

    // ボタンクリック時に呼ばれるメソッド
    public void OnScoreButtonClick()
    {
        // ボタン効果音を再生
        if (buttonSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
        // まだこの画像で得点が変わっていない場合のみ処理
        if (!hasScoredOnCurrentImage)
        {
            if (currentImageIndex == 3 || currentImageIndex == 6 || currentImageIndex == 8 || currentImageIndex == 22)
            {
                // 加点
                score += 100; // 例として100点加算
                Debug.Log("得点！現在のスコア: " + score);
                UpdateScoreText("+100");
            }
            else
            {
                // 減点
                score -= 100; // 例として100点減点
                Debug.Log("お手付き！現在のスコア: " + score);
                UpdateScoreText("-100");
            }
            hasScoredOnCurrentImage = true; // この画像ではもう得点・減点できないようにフラグを立てる
        }
    }

    private void UpdateScoreText(string change)
    {
        if (string.IsNullOrEmpty(change))
        {
            scoreText.text = $"スコア: {score}";
        }
        else
        {
            scoreText.text = $"スコア: {score}  ({change})";
            if (scoreChangeCoroutine != null)
            {
                StopCoroutine(scoreChangeCoroutine);
            }
            scoreChangeCoroutine = StartCoroutine(ShowScoreChange());
        }
    }

    private IEnumerator ShowScoreChange()
    {
        yield return new WaitForSeconds(1.0f);
        scoreText.text = $"スコア: {score}";
        scoreChangeCoroutine = null;
    }

    private IEnumerator PlayKamishibai()
    {
        // 登録されている画像が40枚であることを確認
        if (kamishibaiImages.Length != 40)
        {
            Debug.LogError("画像は40枚登録してください。");
            yield break;
        }

        // 40枚の画像を順番に表示
        while (currentImageIndex < kamishibaiImages.Length)
        {
            // 現在のインデックスの画像を表示
            displayImage.sprite = kamishibaiImages[currentImageIndex];

            // 1つの画像が表示されるたびに、得点フラグをリセットする
            hasScoredOnCurrentImage = false;

            // 効果音を再生
            if (switchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(switchSound);
            }

            // 1秒待つ
            yield return new WaitForSeconds(1.0f);

            // 次の画像へ
            currentImageIndex++;
        }

        Debug.Log("紙芝居が終了しました。最終スコア: " + score);
        scoreText.text = $"最終スコア: {score}";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        // Imageコンポーネントを取得
        displayImage = GetComponent<Image>();

        // Coroutineを開始して、画像の切り替え処理を始める
        StartCoroutine(PlayKamishibai());
    }

    // ボタンクリック時に呼ばれるメソッド
    public void OnScoreButtonClick()
    {
        // 現在の画像が2枚目(インデックス1)か4枚目(インデックス3)であるかを確認
        // かつ、今回の画像でまだ得点が入っていないかを確認
        if ((currentImageIndex == 1 || currentImageIndex == 3) && !hasScoredOnCurrentImage)
        {
            // 得点を加算
            score += 100; // 例として100点加算
            hasScoredOnCurrentImage = true; // この画像ではもう得点できないようにフラグを立てる
            Debug.Log("得点！現在のスコア: " + score);
        }
    }

    private IEnumerator PlayKamishibai()
    {
        // 登録されている画像が10枚であることを確認
        if (kamishibaiImages.Length != 10)
        {
            Debug.LogError("画像は10枚登録してください。");
            yield break;
        }

        // 10枚の画像を順番に表示
        while (currentImageIndex < kamishibaiImages.Length)
        {
            // 現在のインデックスの画像を表示
            displayImage.sprite = kamishibaiImages[currentImageIndex];

            // 1つの画像が表示されるたびに、得点フラグをリセットする
            hasScoredOnCurrentImage = false;

            // 5秒待つ
            yield return new WaitForSeconds(1.0f); // 元のプログラムに合わせて5.0fに修正しました。

            // 次の画像へ
            currentImageIndex++;
        }

        Debug.Log("紙芝居が終了しました。最終スコア: " + score);
    }
}
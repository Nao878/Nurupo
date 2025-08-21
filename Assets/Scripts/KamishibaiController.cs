using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamishibaiController : MonoBehaviour
{
    // Unity�G�f�B�^����摜�����蓖�Ă邽�߂̔z��
    [SerializeField] private Sprite[] kamishibaiImages;

    // UI��Image�R���|�[�l���g
    private Image displayImage;

    // ���ݕ\�����̉摜�̃C���f�b�N�X
    private int currentImageIndex = 0;

    // �v���C���[�̓��_
    private int score = 0;

    // �e�摜�œ��_�����������𔻒肷��t���O
    private bool hasScoredOnCurrentImage = false;

    private void Start()
    {
        // Image�R���|�[�l���g���擾
        displayImage = GetComponent<Image>();

        // Coroutine���J�n���āA�摜�̐؂�ւ��������n�߂�
        StartCoroutine(PlayKamishibai());
    }

    // �{�^���N���b�N���ɌĂ΂�郁�\�b�h
    public void OnScoreButtonClick()
    {
        // ���݂̉摜��2����(�C���f�b�N�X1)��4����(�C���f�b�N�X3)�ł��邩���m�F
        // ���A����̉摜�ł܂����_�������Ă��Ȃ������m�F
        if ((currentImageIndex == 1 || currentImageIndex == 3) && !hasScoredOnCurrentImage)
        {
            // ���_�����Z
            score += 100; // ��Ƃ���100�_���Z
            hasScoredOnCurrentImage = true; // ���̉摜�ł͂������_�ł��Ȃ��悤�Ƀt���O�𗧂Ă�
            Debug.Log("���_�I���݂̃X�R�A: " + score);
        }
    }

    private IEnumerator PlayKamishibai()
    {
        // �o�^����Ă���摜��10���ł��邱�Ƃ��m�F
        if (kamishibaiImages.Length != 10)
        {
            Debug.LogError("�摜��10���o�^���Ă��������B");
            yield break;
        }

        // 10���̉摜�����Ԃɕ\��
        while (currentImageIndex < kamishibaiImages.Length)
        {
            // ���݂̃C���f�b�N�X�̉摜��\��
            displayImage.sprite = kamishibaiImages[currentImageIndex];

            // 1�̉摜���\������邽�тɁA���_�t���O�����Z�b�g����
            hasScoredOnCurrentImage = false;

            // 5�b�҂�
            yield return new WaitForSeconds(1.0f); // ���̃v���O�����ɍ��킹��5.0f�ɏC�����܂����B

            // ���̉摜��
            currentImageIndex++;
        }

        Debug.Log("���ŋ����I�����܂����B�ŏI�X�R�A: " + score);
    }
}
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

    [SerializeField] private AudioClip switchSound; // ���ʉ���Inspector�ŃZ�b�g
    [SerializeField] private AudioClip buttonSound; // �{�^���p���ʉ���Inspector�ŃZ�b�g
    private AudioSource audioSource; // AudioSource�Q��

    private void Start()
    {
        // Image�R���|�[�l���g���擾
        displayImage = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>(); // AudioSource�擾

        // Coroutine���J�n���āA�摜�̐؂�ւ��������n�߂�
        StartCoroutine(PlayKamishibai());
    }

    // �{�^���N���b�N���ɌĂ΂�郁�\�b�h
    public void OnScoreButtonClick()
    {
        // �{�^�����ʉ����Đ�
        if (buttonSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
        // �܂����̉摜�œ��_���ς���Ă��Ȃ��ꍇ�̂ݏ���
        if (!hasScoredOnCurrentImage)
        {
            if (currentImageIndex == 1 || currentImageIndex == 3)
            {
                // ���_
                score += 100; // ��Ƃ���100�_���Z
                Debug.Log("���_�I���݂̃X�R�A: " + score);
            }
            else
            {
                // ���_
                score -= 100; // ��Ƃ���100�_���_
                Debug.Log("����t���I���݂̃X�R�A: " + score);
            }
            hasScoredOnCurrentImage = true; // ���̉摜�ł͂������_�E���_�ł��Ȃ��悤�Ƀt���O�𗧂Ă�
        }
    }

    private IEnumerator PlayKamishibai()
    {
        // �o�^����Ă���摜��20���ł��邱�Ƃ��m�F
        if (kamishibaiImages.Length != 20)
        {
            Debug.LogError("�摜��20���o�^���Ă��������B");
            yield break;
        }

        // 20���̉摜�����Ԃɕ\��
        while (currentImageIndex < kamishibaiImages.Length)
        {
            // ���݂̃C���f�b�N�X�̉摜��\��
            displayImage.sprite = kamishibaiImages[currentImageIndex];

            // 1�̉摜���\������邽�тɁA���_�t���O�����Z�b�g����
            hasScoredOnCurrentImage = false;

            // ���ʉ����Đ�
            if (switchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(switchSound);
            }

            // 1�b�҂�
            yield return new WaitForSeconds(1.0f);

            // ���̉摜��
            currentImageIndex++;
        }

        Debug.Log("���ŋ����I�����܂����B�ŏI�X�R�A: " + score);
    }
}
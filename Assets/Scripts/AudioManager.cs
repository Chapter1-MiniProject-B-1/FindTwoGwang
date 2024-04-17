using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip clip;
    private AudioSource audioSource;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            // ���� �Ѿ�� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���� AudioManager�� �̹� �ִٸ� ó�� ������� �̱����� �����ϰ� �ı�
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // AudioSource ������Ʈ ����
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.Play();
    }
}

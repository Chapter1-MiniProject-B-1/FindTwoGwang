using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip clip;
    private AudioSource audioSource;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            // 씬이 넘어가도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 씬에 AudioManager가 이미 있다면 처음 만들어진 싱글톤을 제외하고 파괴
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // AudioSource 컴포넌트 참조
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = this.clip;
        audioSource.Play();
    }
}

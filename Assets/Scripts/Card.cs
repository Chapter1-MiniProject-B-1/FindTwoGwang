using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    //앞, 뒤면
    public GameObject front;
    public GameObject back;

    //앞면 이미지
    public SpriteRenderer frontImage;

    //애니메이터
    public Animator anim;

    //오디오 관련
    AudioSource audioSource; //오디오 소스
    public AudioClip audioClip; //오디오 클립

    public int idx = 0;
    public int teamIdx = 0;

    void Start()
    {
        //오디오 소스를 얻어옴
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenCard()
    {
        // 팀 카드가 설정되지 않았다면 리턴
        if (GameManager.Instance.teamCard == null)
        {
            return;
        }

        //카드 애니메이션에 있는 IsOpen변수 true로 변경
        anim.SetBool("IsOpen", true);

        //오디오 클립을 재생
        audioSource.PlayOneShot(audioClip);

        //카드가 열어질 때, 뒤집어지는 함수
        StartCoroutine(CoroutineOpenCard());

        // 선택된 카드가 없으면
        if (GameManager.Instance.selectedCard == null) 
        {
            // 현재 카드를 선택된 카드로 설정하고
            GameManager.Instance.selectedCard = this;

            // 선택된 카드와 팀 카드의 일치 여부 검사
            GameManager.Instance.CheckMatch(); 
        }
    }

    public void CloseCard()
    {
        //카드가 닫아질 때, 뒤집어지는 함수
        StartCoroutine(CoroutineCloseCard()); 
    }

    public void DestroyCard()
    {
        // 카드 오브젝트 파괴
        Destroy(gameObject, 0.5f);
    }

    IEnumerator CoroutineOpenCard()
    {
        //0.1초 후에 밑에 있는 내용들이 구동됨 (이유는 뒤집어 지는 시간 고려)
        yield return new WaitForSeconds(0.1f);

        //앞면 활성화
        front.SetActive(true);

        //뒷면 비활성화
        back.SetActive(false);
    }

    IEnumerator CoroutineCloseCard()
    {
        //1초 딜레이 후, 카드가 닫힘
        yield return new WaitForSeconds(1.0f);

        //카드 애니메이션에 있는 IsOpen변수 false 변경
        anim.SetBool("IsOpen", false);

        //앞면 비활성화
        front.SetActive(false);

        //뒷면 활성화
        back.SetActive(true); 
    }

    // 인덱스에 따른 이미지 설정
    public void Setting(int number, int teamCardIndex, int imageIndex)
    {
        idx = number;
        teamIdx = teamCardIndex;

        // 이미지 설정에 사용되는 파일 이름
        string imageName = $"team{teamIdx}_img{imageIndex}";

        // 카드 앞면 스프라이트에 이미지 할당
        frontImage.sprite = Resources.Load<Sprite>(imageName);
    }
}

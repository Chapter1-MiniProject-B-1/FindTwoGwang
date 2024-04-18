using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;

    public int teamCardCount = 5;

    void Start()
    {
        // 이미지 배열의 길이를 팀 카드 수에 맞게 초기화
        int[] imgArr = new int[teamCardCount];

        // 카드 배열을 생성 후 랜덤하게 섞기
        int[] cardArr = Enumerable.Range(0, 20).ToArray();
        cardArr = cardArr.OrderBy(x => Random.Range(0, 20)).ToArray();

        // 팀 카드 배열을 생성 후 랜덤하게 섞기
        int[] teamCardArr = Enumerable.Repeat(Enumerable.Range(0, teamCardCount), 4).SelectMany(x => x).ToArray();
        teamCardArr = teamCardArr.OrderBy(x => Random.Range(0, teamCardArr.Length)).ToArray();

        for (int i = 0; i < 20; i++)
        {
            // Board 오브젝트 아래에 card 오브젝트 생성
            GameObject go = Instantiate(card, this.transform);

            // card 오브젝트의 위치 설정
            float x = (i % 4) * 1.3f - 1.95f;
            float y = (i / 4) * 1.3f - 3.8f;
            go.transform.position = new Vector2(x, y);

            // 팀 카드 배열에서 현재 카드의 팀 인덱스를 가져옴
            int teamIdx = teamCardArr[i];
            // 해당 팀 인덱스를 사용하여 이미지 배열에서 이미지 인덱스를 가져옴
            int imgIdx = imgArr[teamIdx];
            // 이미지 인덱스를 업데이트하여 같은 팀에서 다음 카드에 다른 이미지를 할당
            imgArr[teamIdx] = (imgIdx + 1) % 4;

            // Setting 메소드에 카드 인덱스, 팀 인덱스, 이미지 인덱스를 넘겨줌
            go.GetComponent<Card>().Setting(teamIdx, imgIdx);
        }

        // GameManager에 생성된 카드 수를 업데이트
        GameManager.Instance.cardCount = cardArr.Length;
    }
}

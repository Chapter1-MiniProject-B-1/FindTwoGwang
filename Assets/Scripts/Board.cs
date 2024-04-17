using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;

    public int teamCardsCount = 5;
    private int[] imageIndexes;

    void Start()
    {
        // 이미지 인덱스 배열 초기화
        imageIndexes = new int[teamCardsCount];

        // 카드 인덱스 배열을 생성 후 랜덤하게 섞기
        int[] cardIndexes = Enumerable.Range(0, 20).ToArray();
        cardIndexes = cardIndexes.OrderBy(x => Random.Range(0, 20)).ToArray();

        // 팀 카드 인덱스를 4번 반복하여 배열 생성 후 랜덤하게 섞기
        int[] teamCardDistribution = Enumerable
            .Repeat(Enumerable.Range(0, teamCardsCount), 4) // 0부터 teamCardsCount-1까지의 범위를 생성하고, 이 범위를 4번 반복
            .SelectMany(x => x) // 생성된 여러 시퀀스를 하나의 시퀀스로 평탄화
            .ToArray(); // 평탄화된 시퀀스를 배열로 변환
        teamCardDistribution = teamCardDistribution
            .OrderBy(x => Random.Range(0, teamCardDistribution.Length)) // 배열의 각 요소에 대해 랜덤 숫자를 기준으로 정렬
            .ToArray(); // 정렬된 결과를 다시 배열로 변환

        // 카드를 생성하여 게임 보드에 배치하고 각 카드에 인덱스 할당
        for (int i = 0; i < 20; i++)
        {
            // card 오브젝트를 Board 오브젝트 아래에 생성
            GameObject go = Instantiate(card, this.transform);

            // card 오브젝트의 위치 설정
            float x = (i % 4) * 1.3f - 1.95f;
            float y = (i / 4) * 1.3f - 3.8f;
            go.transform.position = new Vector2(x, y);

            // 팀 카드 분배 배열에서 i번째 카드의 팀 인덱스를 가져옴
            int teamIdx = teamCardDistribution[i];
            // 해당 팀 인덱스의 현재 이미지 인덱스를 가져옴
            int imgIdx = imageIndexes[teamIdx];
            // 해당 팀의 이미지 인덱스를 하나 증가시키고, 4개의 이미지를 순환
            imageIndexes[teamIdx] = (imgIdx + 1) % 4;

            // 랜덤으로 생성된 인덱스 번호와 이미지 인덱스를 카드 스크립트의 Setting 메소드에 전달
            go.GetComponent<Card>().Setting(cardIndexes[i], teamIdx, imgIdx);
        }

        // GameManager에 생성된 카드 수를 업데이트
        GameManager.Instance.cardCount = cardIndexes.Length;
    }
}

using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;

    public int teamCardCount = 5;

    void Start()
    {
        // �̹��� �迭�� ���̸� �� ī�� ���� �°� �ʱ�ȭ
        int[] imgArr = new int[teamCardCount];

        // ī�� �迭�� ���� �� �����ϰ� ����
        int[] cardArr = Enumerable.Range(0, 20).ToArray();
        cardArr = cardArr.OrderBy(x => Random.Range(0, 20)).ToArray();

        // �� ī�� �迭�� ���� �� �����ϰ� ����
        int[] teamCardArr = Enumerable.Repeat(Enumerable.Range(0, teamCardCount), 4).SelectMany(x => x).ToArray();
        teamCardArr = teamCardArr.OrderBy(x => Random.Range(0, teamCardArr.Length)).ToArray();

        for (int i = 0; i < 20; i++)
        {
            // Board ������Ʈ �Ʒ��� card ������Ʈ ����
            GameObject go = Instantiate(card, this.transform);

            // card ������Ʈ�� ��ġ ����
            float x = (i % 4) * 1.3f - 1.95f;
            float y = (i / 4) * 1.3f - 3.8f;
            go.transform.position = new Vector2(x, y);

            // �� ī�� �迭���� ���� ī���� �� �ε����� ������
            int teamIdx = teamCardArr[i];
            // �ش� �� �ε����� ����Ͽ� �̹��� �迭���� �̹��� �ε����� ������
            int imgIdx = imgArr[teamIdx];
            // �̹��� �ε����� ������Ʈ�Ͽ� ���� ������ ���� ī�忡 �ٸ� �̹����� �Ҵ�
            imgArr[teamIdx] = (imgIdx + 1) % 4;

            // Setting �޼ҵ忡 ī�� �ε���, �� �ε���, �̹��� �ε����� �Ѱ���
            go.GetComponent<Card>().Setting(teamIdx, imgIdx);
        }

        // GameManager�� ������ ī�� ���� ������Ʈ
        GameManager.Instance.cardCount = cardArr.Length;
    }
}

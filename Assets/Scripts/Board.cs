using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;

    public int teamCardsCount = 5;
    private int[] imageIndexes;

    void Start()
    {
        // �̹��� �ε��� �迭 �ʱ�ȭ
        imageIndexes = new int[teamCardsCount];

        // ī�� �ε��� �迭�� ���� �� �����ϰ� ����
        int[] cardIndexes = Enumerable.Range(0, 20).ToArray();
        cardIndexes = cardIndexes.OrderBy(x => Random.Range(0, 20)).ToArray();

        // �� ī�� �ε����� 4�� �ݺ��Ͽ� �迭 ���� �� �����ϰ� ����
        int[] teamCardDistribution = Enumerable
            .Repeat(Enumerable.Range(0, teamCardsCount), 4) // 0���� teamCardsCount-1������ ������ �����ϰ�, �� ������ 4�� �ݺ�
            .SelectMany(x => x) // ������ ���� �������� �ϳ��� �������� ��źȭ
            .ToArray(); // ��źȭ�� �������� �迭�� ��ȯ
        teamCardDistribution = teamCardDistribution
            .OrderBy(x => Random.Range(0, teamCardDistribution.Length)) // �迭�� �� ��ҿ� ���� ���� ���ڸ� �������� ����
            .ToArray(); // ���ĵ� ����� �ٽ� �迭�� ��ȯ

        // ī�带 �����Ͽ� ���� ���忡 ��ġ�ϰ� �� ī�忡 �ε��� �Ҵ�
        for (int i = 0; i < 20; i++)
        {
            // card ������Ʈ�� Board ������Ʈ �Ʒ��� ����
            GameObject go = Instantiate(card, this.transform);

            // card ������Ʈ�� ��ġ ����
            float x = (i % 4) * 1.3f - 1.95f;
            float y = (i / 4) * 1.3f - 3.8f;
            go.transform.position = new Vector2(x, y);

            // �� ī�� �й� �迭���� i��° ī���� �� �ε����� ������
            int teamIdx = teamCardDistribution[i];
            // �ش� �� �ε����� ���� �̹��� �ε����� ������
            int imgIdx = imageIndexes[teamIdx];
            // �ش� ���� �̹��� �ε����� �ϳ� ������Ű��, 4���� �̹����� ��ȯ
            imageIndexes[teamIdx] = (imgIdx + 1) % 4;

            // �������� ������ �ε��� ��ȣ�� �̹��� �ε����� ī�� ��ũ��Ʈ�� Setting �޼ҵ忡 ����
            go.GetComponent<Card>().Setting(cardIndexes[i], teamIdx, imgIdx);
        }

        // GameManager�� ������ ī�� ���� ������Ʈ
        GameManager.Instance.cardCount = cardIndexes.Length;
    }
}

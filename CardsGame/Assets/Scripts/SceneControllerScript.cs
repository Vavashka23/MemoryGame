using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    [SerializeField] private CardsClickedScript originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;

    public const int gridRows = 4;
    public const int gridCols = 8;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    private int _score = 0;

    private CardsClickedScript _firstCard;
    private CardsClickedScript _secondCard;

    public bool SecondCardIsOpened
    {
        get { return _secondCard == null; }
    }

    public void CardRevealed(CardsClickedScript card)
    {
        if (_firstCard == null)
        {
            _firstCard = card;
        }
        else
        {
            _secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }
    private IEnumerator CheckMatch()
    {
        if (_firstCard.id == _secondCard.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
            _firstCard.GetComponent<SpriteRenderer>().color = Color.gray;
            _secondCard.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            _firstCard.SetBackCardActive();
            _secondCard.SetBackCardActive();
        }
        _firstCard = null;
        _secondCard = null;
    }

    void Start()
    {
        scoreLabel.text = "Score: " + _score;
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7 };
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                CardsClickedScript card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as CardsClickedScript;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);
                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];

        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OutGame()
    {
        Application.Quit();
    }
}
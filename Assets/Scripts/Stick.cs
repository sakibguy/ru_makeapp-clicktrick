using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stick : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI score2Text;
    public TextMeshPro centerText;
    public Image fillImage;
    private float initialSpeed;
    private float speed;
    private Dictionary<string, Color> colors = new Dictionary<string, Color>();
    private BoxCollider2D boxCollider;

    private float direction = 0.5f;

    private int scoreCounter = 0;
    private int score = 0;

    
    void Start() {
        colors.Add("black", Color.black);
        colors.Add("red", Color.red);
        colors.Add("cyan", Color.cyan);
        colors.Add("white", Color.white);
        if (Application.isEditor) {
            initialSpeed = 1;
        } else {
            initialSpeed = 3;
        }
        ChangeColor();
        ScoreCountChange();
    }

    void Update() {
        if (!GlobalValue.isGameOver) {
            transform.Rotate(new Vector3(0, 0, direction) * speed);
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) {
                boxCollider = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
                Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
                string hitObjectName = "";
                if (overlap.Length > 1) {
                    if(GameObject.ReferenceEquals(overlap[0].gameObject, gameObject.transform.GetChild(0).gameObject)) {
                        hitObjectName = overlap[1].gameObject.name;
                    } else {
                        hitObjectName = overlap[0].gameObject.name;
                    }
                }
                if (gameObject.transform.GetChild(0).gameObject.name == hitObjectName) {
                    direction = -direction;
                    ChangeColor();
                    ScoreCountChange();
                    if (SoundControl.Instance) {
                        SoundControl.Instance.PlayBonus();
                    }
                } else {
                    UIControl.instance.GameOver();
                }
            }
        }
    }

    private void ChangeColor() {
        int randomIndex = Random.Range(0, colors.Count);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors.ElementAt(randomIndex).Value;
        gameObject.transform.GetChild(0).gameObject.name = colors.ElementAt(randomIndex).Key;
    }

    private void ScoreCountChange() {
        scoreCounter--;
        if (scoreCounter <= 0) {
            score++;
            UIControl.instance.ShowScore(score);
            scoreCounter = score + 1;
            if (score > 1) {
                ColorCircle.instance.AnimationStart();
            }
            scoreText.text = score.ToString();
            score2Text.text = (score+1).ToString();
        }

        int totalLength = score + 1;
        int completePart = totalLength - scoreCounter;
        float fillAmount = ((float)completePart / (float)totalLength);
        fillImage.fillAmount = fillAmount;
        speed = initialSpeed + score * 0.1f;
        

        if (Application.isEditor) {
            if (speed > 3.0f) {
                speed = 3.0f;
            }
        } else {
            if (speed > 5.0f) {
                speed = 5.0f;
            }
        }
        centerText.text = scoreCounter.ToString();
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

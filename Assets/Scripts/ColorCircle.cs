using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class ColorCircle : MonoBehaviour {

    public static ColorCircle instance;

    public GameObject[] colorsObject;

    private Dictionary<string, Color> colors = new Dictionary<string, Color>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        colors.Add("black", Color.black);
        colors.Add("red", Color.red);
        colors.Add("cyan", Color.cyan);
        colors.Add("white", Color.white);
        SetColor();
        transform.DOKill();
        gameObject.transform.DORotate(new Vector3(0, 0, 50), 5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnDisable() {
        transform.DOKill();
    }


    private void SetColor() {
        transform.DOKill();
        for (int i = 0; i < colorsObject.Length; i++) {
            int randomIndex = Random.Range(0, colors.Count);
            colorsObject[i].GetComponent<SpriteRenderer>().color = colors.ElementAt(randomIndex).Value;
            colorsObject[i].gameObject.name = colors.ElementAt(randomIndex).Key;
            colors.Remove(colors.ElementAt(randomIndex).Key);
        }
    }

    public void AnimationStart() {
        transform.DOScale(new Vector3(0.1f, 0.1f, 1), 0.5f).SetEase(Ease.InOutSine).OnComplete(()=> {
            transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InOutSine);
        });
    }


}

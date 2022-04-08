using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixOrthographicSize : MonoBehaviour {

    [SerializeField] private SpriteRenderer box;
    [SerializeField] private bool isVertical;

    private float orthographicSize;

    private void Awake() {
        if (isVertical) {
            Vertically();
        } else {
            Horizontaly();
        }
        FixImage();
        GlobalValue.boundary = box.bounds.size.x * 0.5f;
    }

    private void Start() {
        
    }


    void Vertically() {
        orthographicSize = box.bounds.size.y / 2f;
        Camera.main.orthographicSize = orthographicSize;
    }

    void Horizontaly() {
        float ratio = Screen.height / (float)Screen.width;
        orthographicSize = box.bounds.size.x / 2f * ratio;
        Camera.main.orthographicSize = orthographicSize;
    }

    void FixImage() {
        box.drawMode = SpriteDrawMode.Sliced;
        if (isVertical) {
            float ratio = Screen.height / (float)Screen.width;
            Vector2 orginalImageSize = box.bounds.size;
            box.size = new Vector2(orthographicSize / ratio * 2f, orginalImageSize.y);
        } else {
            Vector2 orginalImageSize = box.bounds.size;
            box.size = new Vector2(orginalImageSize.x, orthographicSize * 2f);
        }
        box.transform.localScale = new Vector3(1, 1, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 设置一个范围，让GameObject移动到这个范围内的随机位置
public class PositionRandomizer : MonoBehaviour {
    [SerializeField]
    float leftPosX = -2.5f, rightPosX = 2.5f, topPosY = 4, bottomPosY = -4;

    public void GotoNewPosition() {
        float x = Random.Range(leftPosX, rightPosX);
        float y = Random.Range(bottomPosY, topPosY);
        transform.position = new Vector2(x, y);
    }
}

using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    IngameManager ingameManager = null;
    public void Start()
    {
        ingameManager = IngameManager.Instance;
    }
    public Vector3 SetRandomPlace(Vector2 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector2(0, halfHoleSize);
        bottomObject.localPosition = new Vector2(0, -halfHoleSize);

        Vector2 placePosition = lastPosition + new Vector2(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        FlappyPlayer player = other.GetComponent<FlappyPlayer>();
        if (player != null&&!player.isDead)
            ingameManager.AddScore(1);
    }
}
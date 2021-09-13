using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform playerTrans;
    public SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject[] cookies;
    [SerializeField] private float[] spawnRates;
    [SerializeField] private Sprite[] floorSprites;
    [SerializeField] private float[] maxDeviation;

    void Awake()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = floorSprites[Random.Range(0, 3)];

        for (int i = 0; i < cookies.Length; i++)
        {
            GameObject obj = cookies[i];
            if (SpawnCookie(spawnRates[i]))
            {
                obj.SetActive(true);
                float max = maxDeviation[i];
                obj.transform.position += new Vector3(Random.Range(-max, max), 0, 0);
            } else
            {
                obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - playerTrans.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private bool SpawnCookie(float chance)
    {
        return (Random.Range(0f, 1f) < chance);
    }
}

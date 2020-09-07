using System.Collections;
using UnityEngine;

public class BackGroundAction : MonoBehaviour
{
    public Data d;
    private Vector3 borders;
    [Header("Sprites")]
    public GameObject circle;
    public GameObject triangle;
    public GameObject cube;
    public Vector2 randomScale;
    public Vector2 randomSpawn;

    public void Start()
    {
        borders = d.cubeBorders;
        StartCoroutine(CountSpawn());
    }

    public IEnumerator CountSpawn()
    {
        yield return new WaitForSeconds(Random.Range(randomSpawn.x, randomSpawn.y));
        Vector3 v = new Vector3(Random.Range(-borders.x, borders.x), borders.y + randomScale.y, 10f);
        GameObject g = Instantiate(circle, v, Quaternion.identity, gameObject.transform);
        float x = Random.Range(randomScale.x, randomScale.y);
        g.transform.localScale = new Vector3(x, x, 1f);
        StartCoroutine(Delete(g));
        StartCoroutine(CountSpawn());
    }
    public IEnumerator Delete(GameObject g)
    {
        yield return new WaitForSeconds(0.1f);
        float posy = g.transform.position.y;
        if (posy <= -borders.y - 1f) { Destroy(g); }
        else { StartCoroutine(Delete(g)); }
    }
}

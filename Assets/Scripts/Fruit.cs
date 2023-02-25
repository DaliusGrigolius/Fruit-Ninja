using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;

    public void CreateSlicedFruit()
    {
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        FindObjectOfType<GameManager>().PlayRandomSliceSound();

        Rigidbody[] rbsOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rbsOnSliced)
        {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, 5f);
        }

        int randomScore = Random.Range(1, 4);
        FindObjectOfType<GameManager>().IncreaseScore(randomScore);

        Destroy(inst.gameObject, 5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade b = collision.GetComponent<Blade>();

        if (!b)
        {
            return;
        }

        CreateSlicedFruit();
    }
}

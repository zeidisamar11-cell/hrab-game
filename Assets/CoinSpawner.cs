using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;   // le modèle de pièce
    public float spacing = 2f;      // distance entre deux pièces
    public float halfSize = 13f;    // zone couverte : de -13 à +13

    void Start()
    {
        for (float x = -halfSize; x <= halfSize; x += spacing)
        {
            for (float z = -halfSize; z <= halfSize; z += spacing)
            {
                Vector3 pos = new Vector3(x, 0.5f, z);

                // Pas de pièce dans la maison des ennemis
                if (Mathf.Abs(x) < 5f && Mathf.Abs(z) < 4f) continue;

                // Pas de pièce là où il y a déjà un mur ou Pacman
                if (Physics.CheckSphere(pos, 0.45f)) continue;

                Instantiate(coinPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
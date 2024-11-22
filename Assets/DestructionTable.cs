using UnityEngine;

public class DestructionTable : MonoBehaviour
{
    private GameObject fruit1 = null; 
    private GameObject fruit2 = null; 
    public GameObject destroyEffectPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (fruit1 == null)
        {
            fruit1 = other.gameObject;
            Debug.Log("fruit1 eklendi: " + fruit1.name);
        }
        else if (fruit2 == null)
        {
            fruit2 = other.gameObject;
            Debug.Log("fruit2 eklendi: " + fruit2.name);
            CheckFruits();
        }
    }

    void CheckFruits()
    {
        if (fruit1 != null && fruit2 != null)
        {
            Debug.Log("Kontrol ediliyor: fruit1.tag = " + fruit1.tag + ", fruit2.tag = " + fruit2.tag);

            if (fruit1.tag == fruit2.tag) 
            {
                Debug.Log("Meyveler aynı! Efekt oynatılıyor.");

                PlayDestroyEffect(fruit1.transform.position);
                PlayDestroyEffect(fruit2.transform.position);

                Destroy(fruit1, 1f);
                Destroy(fruit2, 1f);
            }
            else
            {
                Debug.Log("Meyveler farklı! Yok edilmiyor.");
            }

            fruit1 = null; 
            fruit2 = null; 
        }
    }

    void PlayDestroyEffect(Vector3 position)
    {
        // Efekt oluştur
        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1.5f); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == fruit1)
        {
            fruit1 = null;
            Debug.Log("fruit1 masadan çıktı.");
        }
        else if (other.gameObject == fruit2)
        {
            fruit2 = null;
            Debug.Log("fruit2 masadan çıktı.");
        }
    }
}

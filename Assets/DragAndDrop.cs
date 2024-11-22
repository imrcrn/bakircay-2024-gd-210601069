using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Plane dragPlane; // Sürükleme düzlemi
    private Vector3 offset; // Fare ile obje arasındaki fark
    private bool isDragging = false;

    public GameObject dragEffectPrefab; // Sürükleme sırasında kullanılacak efekt
    private GameObject dragEffectInstance; // Oluşturulan efektin referansı

    void OnMouseDown()
    {
        // Kameranın bakış yönüne uygun bir düzlem oluştur
        dragPlane = new Plane(Vector3.up, Vector3.zero);

        // Fare pozisyonundan bir ışın oluştur
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (dragPlane.Raycast(ray, out distance))
        {
            offset = transform.position - ray.GetPoint(distance);
        }

        // Efekti oluştur
        if (dragEffectPrefab != null)
        {
            dragEffectInstance = Instantiate(dragEffectPrefab, transform.position, Quaternion.identity);
        }

        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;

            if (dragPlane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance) + offset;

                // Efekt pozisyonunu güncelle
                if (dragEffectInstance != null)
                {
                    dragEffectInstance.transform.position = transform.position;
                }
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Efekti yok et
        if (dragEffectInstance != null)
        {
            Destroy(dragEffectInstance);
        }
    }
}

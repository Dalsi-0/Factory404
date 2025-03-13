using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObejct;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("SpacePioneers/Prefab Factory")]
public class PrefabFactory : GameEventListener
{
    [SerializeField] GameObject prefab;
    [SerializeField] bool useMousePosition;
    [SerializeField] bool hasCost;
    [SerializeField] FloatVariable creationCost;

    void Start()
    {
        Response.AddListener(this.CreateObject);
    }

    public void CreateObject()
    {
        Vector3 position = transform.position;

        if(useMousePosition)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.transform.position.y - transform.position.y;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

            position = worldPoint;
        }
        

        GameObject obj = Instantiate(prefab, position, transform.rotation);
        obj.transform.parent = null;
    }

}
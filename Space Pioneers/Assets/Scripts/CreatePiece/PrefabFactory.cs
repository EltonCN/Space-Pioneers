using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("SpacePioneers/Prefab Factory")]
public class PrefabFactory : GameEventListener
{
    [SerializeField] GameObject prefab;
    [SerializeField] bool useMousePosition;
    [SerializeField] bool hasCost;
    [SerializeField] FloatVariable creationCost;
    [SerializeField] ActionSnapshotRS actionSet;

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

        if(hasCost)
        {
            CreateSnapshot snap = new CreateSnapshot(this, obj, creationCost.value);
            actionSet.Add(snap);
        }
    }

    public class CreateSnapshot : ActionSnapshot
    {
        PrefabFactory originator;
        float cost;
        GameObject createdObject;

        public CreateSnapshot(PrefabFactory originator, GameObject createdObject, float cost = 0)
        {
            this.originator = originator;
            this.cost = cost; 
            this.createdObject = createdObject;
        }

        public float getActionCost()
        {
            return this.cost;
        }

        public string getActionMessage()
        {
            return "Criar entidade";
        }

        public void undo()
        {
            Destroy(createdObject);   
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("SpacePioneers/Mechanics/Trajectory Preview")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    [SerializeField]
    private int maxIterations = 10;
    private float range = 10f;
    private Scene simulationScene;
    private PhysicsScene simulationPhysicsScene;

    private LineRenderer lineRenderer;

    private List<GameObject> dummys = new List<GameObject>();
    
    private bool initiated;

    private Rigidbody ownRb;

    private GameObject twin = null;

    private Vector3 v0;

    private bool running = false;

    bool subscribed = false;

    // Start is called before the first frame update
    void Start()
    {
        initiated = false;

        ownRb = this.GetComponent<Rigidbody>();

    }

    void destroyAll()
    {
        foreach(var o in dummys){
            Destroy(o);
        }
        dummys.Clear();
    }
    void simular()
    {
        
        twin = Instantiate(this.gameObject);
        twin.transform.position = this.transform.position;
        twin.transform.rotation = this.transform.rotation;

        twin.GetComponent<Renderer>().enabled = false;
        twin.GetComponent<TrajectoryPreview>().enabled = false;

        twin.GetComponent<Gravity>().v0 = this.GetComponent<Frozen>().velocity;

        SceneManager.MoveGameObjectToScene(twin, simulationScene);

        dummys.Add(twin);

        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        List<Rigidbody> rbs = new List<Rigidbody>();
        twin.GetComponent<Rigidbody>().isKinematic = false;

        foreach (Collider c in cols)
        {
            Rigidbody rb = c.attachedRigidbody;
            if (rb != ownRb )
            {
                GameObject go = c.gameObject;
                
                Gravity g = go.GetComponent<Gravity>();
                if(g == null)
                {
                    continue;
                }

                GameObject fakeT = Instantiate(go);

                g = fakeT.GetComponent<Gravity>();
                g.v0 = go.GetComponent<Frozen>().velocity;

                fakeT.transform.position = go.transform.position;
                fakeT.transform.rotation = go.transform.rotation;

                go.GetComponent<Rigidbody>().isKinematic = false;

                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if(fakeR){
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, simulationScene);

                dummys.Add(fakeT);
            }
        }

        foreach(GameObject o in dummys)
        {
            foreach (Renderer r in o.GetComponentsInChildren<Renderer>())
                r.enabled = false;

            
            Frozen fr = o.GetComponent<Frozen>();

            if(fr != null)
            {
                fr.enabled = false;
            }
        }

        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = maxIterations;

        for(int i = 0; i<maxIterations; i++)
        {
            foreach(GameObject o in dummys)
            {
                Gravity g = o.GetComponent<Gravity>();

                if(g != null)
                {
                    g.gravityRun(simulationPhysicsScene);
                }
            }

            simulationPhysicsScene.Simulate(Time.fixedDeltaTime);

            Vector3 linePosition = new Vector3(twin.transform.position.x, 
                                                twin.transform.position.y, 
                                                twin.transform.position.z);

            lineRenderer.SetPosition(i, linePosition);

            
        }
        destroyAll();
    }

    // Update is called once per frame
    void Update()
    {
        if(!initiated)
        {
            CreateSceneParameters param = new CreateSceneParameters(LocalPhysicsMode.Physics3D);

            string scene_name;
            Scene scene;
            do
            {
                scene_name = "SimulationTrajetory"+(int)Random.Range(0,100);
                scene = SceneManager.GetSceneByName(scene_name);
            }while(scene.IsValid());

            simulationScene = SceneManager.CreateScene(scene_name, param);
            simulationPhysicsScene = simulationScene.GetPhysicsScene();

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetWidth(0.04f, 0.04f);
            lineRenderer.SetColors(Color.white, Color.white);

            initiated = true;
        }

        if(running == true)
        {
            try
            {
                simular();
            }
            catch (System.Exception)
            {
                destroyAll();
                print("Erro no trajectory");
                throw;
            }
            
        }
    }

    public void OnEnterActionMode()
    {
        running = false;

        if(initiated)
        {
            this.lineRenderer.enabled = false;
        }
    }

    public void OnEnterPlanningMode()
    {
        running = true;

        if(initiated)
        {
            this.lineRenderer.enabled = true;
        }
    }


}

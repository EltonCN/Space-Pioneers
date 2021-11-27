using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.ExceptionServices;

[CreateAssetMenu(menuName = "Space Pioneers/Systems/Trajectory")]
public class TrajectorySystem : ScriptableObject
{
    [SerializeField] float computeInterval = 1f;
    [SerializeField] private int maxIterations = 10;
    [SerializeField] float deltaTFactor = 1f;

    [SerializeField] GravityRS gravitySet;
    [SerializeField] TrajectoryRS trajectorySet;

    float lastComputeTime = 0;

    Scene simulationScene;
    PhysicsScene simulationPhysicsScene;
    
    Dictionary<TrajectoryPreview, Vector3[]> trajectorysPositions;
    Dictionary<GameObject, GameObject> fakeRealMap;
    Dictionary<GameObject, GameObject> realFakeMap;
    Dictionary<GameObject, TrajectoryPreview> fakePreviewMap;
    List<GameObject> fakes;

    GravityRS gravRS;
    TrajectoryRS trajRS;

    public Vector3[] getPositions(TrajectoryPreview tp)
    {
        if((Time.time - lastComputeTime >= computeInterval) || 
            trajectorysPositions == null || !trajectorysPositions.ContainsKey(tp))
        {
            try
            {
                Compute();
            }
            catch(Exception ex)
            {
                destroyAll();
                ExceptionDispatchInfo ex_info = ExceptionDispatchInfo.Capture(ex);
                ex_info.Throw();
            }
            
        }

        Debug.Log(tp.name);

        return trajectorysPositions[tp];
    }

    void destroyAll()
    {
        foreach(GameObject go in fakes){
            Destroy(go);
        }
        fakes.Clear();
        fakePreviewMap.Clear();
        fakeRealMap.Clear();
        realFakeMap.Clear();

        Debug.LogWarning("Cleaning fakes list and maps");
    }

    void destroyWithoutReference()
    {
        foreach(GameObject fake in fakes)
        {
            if(fake == null || fakeRealMap[fake] == null)
            {
                fakes.Remove(fake);
                fakeRealMap.Remove(fake);
                realFakeMap.Remove(fakeRealMap[fake]);
                fakePreviewMap.Remove(fake);

                Destroy(fake);
            }
        }
    }

    void Compute()
    {
        if(!simulationScene.IsValid())
        {
            Start();
        }

        trajectorysPositions = new Dictionary<TrajectoryPreview, Vector3[]>();

        foreach(TrajectoryPreview tp in trajectorySet.Items)
        {
            trajectorysPositions.Add(tp, new Vector3[maxIterations]);
        }
        
        GameObject[] fakeWithTrajectory = new GameObject[trajectorySet.Items.Count];
        int index = 0;

        foreach(Gravity grav in gravitySet.Items.ToArray())
        {
            GameObject gravGameObj = grav.gameObject;
            GameObject fake;

            if(realFakeMap.ContainsKey(gravGameObj))
            {
                fake = realFakeMap[gravGameObj];
            }
            else
            {
                fake = Instantiate(gravGameObj);
                
                fake.layer = 8;
                fakeRealMap.Add(fake, gravGameObj);
                realFakeMap.Add(gravGameObj, fake);
                fakes.Add(fake);
                SceneManager.MoveGameObjectToScene(fake, simulationScene);

                Renderer[] fakeRs = fake.GetComponentsInChildren<Renderer>();
                foreach(Renderer r in fakeRs)
                {
                    if(r != null)
                    {
                        r.enabled = false;
                    }
                }

                Gravity fakeGrav = fake.GetComponent<Gravity>();
                fakeGrav.GravityRS = gravRS;
                fakeGrav.enabled = true;

                SpaceShip spaceShip = fake.GetComponent<SpaceShip>();
                if(spaceShip != null)
                {
                    spaceShip.enabled = false;
                }


            }

            fake.transform.position = gravGameObj.transform.position;
            fake.transform.rotation = gravGameObj.transform.rotation;

            GameModeMechanics modeMechanics = fake.GetComponent<GameModeMechanics>();
            Frozen fr = fake.GetComponent<Frozen>();
            if(fr != null)
            {
                if(fr.enabled == true)
                {
                    if(modeMechanics != null)
                    {
                        if(modeMechanics.IsActionMechanic<Frozen>())
                        {
                            fr.enabled = true;
                        }
                        else
                        {
                            fr.enabled = false;
                        }
                    }      
                }
            }

            if(modeMechanics != null)
            {
                modeMechanics.enabled = false;
            }

            Velocity vl = fake.GetComponent<Velocity>();
            if(vl != null)
            {
                vl.enabled = true;
                vl.ActualVelocity = gravGameObj.GetComponent<Velocity>().ActualVelocity;
                vl.enabled = false;
            }

            
            TrajectoryPreview fakeTraj = fake.GetComponent<TrajectoryPreview>();
            if(fakeTraj != null)    
            {
                TrajectoryRS originalSet = fakeTraj.trajectorySet;
                fakeTraj.trajectorySet = trajRS;
                originalSet.Remove(fakeTraj);
                fakeTraj.enabled = false;
            }
                    

            foreach(TrajectoryPreview tp in trajectorySet.Items)
            {
                GameObject tpGameObj = tp.gameObject;


                if(GameObject.ReferenceEquals(tpGameObj, gravGameObj))
                {
                    fakeWithTrajectory[index] = fake;
                    index += 1;

                    if(!fakePreviewMap.ContainsKey(fake))
                    {
                        fakePreviewMap.Add(fake, tp);
                    }
                    
                }
            }
        }

        Physics.autoSimulation = false;
        for(int i = 0; i<maxIterations; i++)
        {
            foreach(GameObject go in fakes)
            {
                Gravity grav = go.GetComponent<Gravity>();

                if(grav != null)
                {
                    grav.gravityRun(simulationPhysicsScene);
                }
            }


            simulationPhysicsScene.Simulate(Time.fixedDeltaTime*deltaTFactor);

            foreach(GameObject go in fakeWithTrajectory)
            {

                Vector3 position = new Vector3(go.transform.position.x, 
                                                go.transform.position.y, 
                                                go.transform.position.z);

                TrajectoryPreview realTP = fakePreviewMap[go];
                trajectorysPositions[realTP][i] = position;

                Debug.Log(realTP.name);
            }

        }
        Physics.autoSimulation = true;
        lastComputeTime = Time.time;

        destroyWithoutReference();
        if(fakes.Count > 100)
        {
            destroyAll();
        }
    }

    void Start()
    {
        CreateSceneParameters param = new CreateSceneParameters(LocalPhysicsMode.Physics3D);

        string scene_name;
        Scene scene;
        do
        {
            scene_name = "SimulationTrajetory"+(int)UnityEngine.Random.Range(0,100);
            scene = SceneManager.GetSceneByName(scene_name);
        }while(scene.IsValid());

        simulationScene = SceneManager.CreateScene(scene_name, param);
        simulationPhysicsScene = simulationScene.GetPhysicsScene();

        fakeRealMap = new Dictionary<GameObject, GameObject>();
        fakePreviewMap = new Dictionary<GameObject, TrajectoryPreview>();
        realFakeMap = new Dictionary<GameObject, GameObject>();
        fakes = new List<GameObject>();
        gravRS = ScriptableObject.CreateInstance<GravityRS>();
        trajRS = ScriptableObject.CreateInstance<TrajectoryRS>();

    }

}
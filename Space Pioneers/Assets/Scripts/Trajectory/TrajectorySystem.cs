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
    List<GameObject> fakes = new List<GameObject>();

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

        return trajectorysPositions[tp];
    }

    void destroyAll()
    {
        foreach(GameObject go in fakes){
            Destroy(go);
        }
        fakes.Clear();
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
        
        Dictionary<GameObject, TrajectoryPreview> fakeRealMap = new Dictionary<GameObject, TrajectoryPreview>();

        GameObject[] fakeWithTrajectory = new GameObject[trajectorySet.Items.Count];
        int index = 0;

        GravityRS gravRS = ScriptableObject.CreateInstance<GravityRS>();
        TrajectoryRS trajRS = ScriptableObject.CreateInstance<TrajectoryRS>();

        foreach(Gravity grav in gravitySet.Items.ToArray())
        {
            GameObject gravGameObj = grav.gameObject;

            GameObject fake = Instantiate(gravGameObj);
            fake.transform.position = gravGameObj.transform.position;
            fake.transform.rotation = gravGameObj.transform.rotation;


            Gravity fakeGrav = fake.GetComponent<Gravity>();
            fakeGrav.GravityRS = gravRS;
            fakeGrav.enabled = true;

            Renderer fakeR = fake.GetComponent<Renderer>();
            if(fakeR){
                fakeR.enabled = false;
            }

            GameModeMechanics modeMechanics = fake.GetComponent<GameModeMechanics>();

            Frozen fr = fake.GetComponent<Frozen>();
            Velocity vl = fake.GetComponent<Velocity>();
            if(fr != null)
            {
                vl.enabled = true;
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
                    
                    vl.ActualVelocity = gravGameObj.GetComponent<Velocity>().ActualVelocity;
                }
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

                    fakeRealMap.Add(fake, tp);
                }
            }

            fakes.Add(fake);
            SceneManager.MoveGameObjectToScene(fake, simulationScene);
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


            simulationPhysicsScene.Simulate(Time.fixedDeltaTime);
            simulationPhysicsScene.Simulate(Time.fixedDeltaTime*deltaTFactor);

            foreach(GameObject go in fakeWithTrajectory)
            {

                Vector3 position = new Vector3(go.transform.position.x, 
                                                go.transform.position.y, 
                                                go.transform.position.z);

                

                TrajectoryPreview realTP = fakeRealMap[go];
                trajectorysPositions[realTP][i] = position;
                
            }

        }
        Physics.autoSimulation = true;

        lastComputeTime = Time.time;
        destroyAll();
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

    }

}
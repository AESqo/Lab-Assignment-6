using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public int treeNum;
    public int pyramidBase;
    private GameObject selene;
    private Light glow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn_ground();
        spawn_trees();
        spawn_pyramid();
        spawn_celestial();
    }

    // Update is called once per frame
    void spawn_ground() {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.transform.localScale = new Vector3(30f, 1f, 30f);
        ground.transform.position = new Vector3(0f, -0.5f, 0f);
        ground.GetComponent<Renderer>().material.color = Color.teal;
    }
        void spawn_trees() {
            GameObject[] trees = new GameObject[treeNum];
        for(int i = 0; i < treeNum; i++) {
            trees[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            trees[i].transform.localScale = new Vector3(0.5f, 1f, 0.5f);
            trees[i].transform.position = new Vector3(Random.Range(4f,(float)treeNum * 0.5f), 0.5f, Random.Range(-5f,(float)treeNum * 0.5f));
            trees[i].GetComponent<Renderer>().material.color = Color.green;
        }
    }
    void spawn_pyramid() {
        if(pyramidBase > 10){pyramidBase = 10;}
        if(pyramidBase < 3){pyramidBase = 3;}
        int totalCubes = 0, counter = 0;
        Vector3 pointer = new Vector3(-5f, 0.5f, -5f);
        for(int i = 0; i < pyramidBase; i++)
        {
            totalCubes += (pyramidBase - i) * (pyramidBase - i);
        }
        GameObject[] pyramid = new GameObject[totalCubes];
        for(int i = 0; i < pyramidBase; i++){
            Color randColor = Random.ColorHSV();
            pointer.x = -5f + (0.25f * i);
            pointer.z = -5f + (0.25f * i);
            for(int j = pyramidBase; j > i; j--) {
                for(int k = pyramidBase; k > i; k--) {
                    pyramid[counter] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    pyramid[counter].transform.position = pointer;
                    pyramid[counter].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    pyramid[counter].GetComponent<Renderer>().material.color = randColor;
                    pointer.z += 0.55f;
                    counter++;
                }
                pointer.z = (float)(-5f + i * 0.25f);
                pointer.x += 0.55f;
            }
            pointer.y += 0.5f;
        }
    }
    void spawn_celestial() {
        selene = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        selene.GetComponent<Renderer>().material.color = Color.white;
        selene.transform.position = new Vector3(0f, 15f, 0f);
        selene.transform.Rotate(90f, 0f, 0f, Space.Self);
        glow = selene.AddComponent<Light>();
        glow.intensity = 2f;
        glow.type = LightType.Directional;
        glow.shadows = LightShadows.Soft;
    }
    void Update() {
        selene.transform.RotateAround(Vector3.zero, Vector3.forward, 10f * Time.deltaTime);
        if(selene.transform.position.y < -5f) {glow.intensity = 0.1f;}
        if(selene.transform.position.y > -5f) {glow.intensity = 2;}
    }
}

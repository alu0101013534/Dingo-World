using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public float turnRate;
    public int seed;
    public int depth;
    public int maxBranches;

    public int length;
    public int minLength;
    public List<Platform> startPlatforms;
    public List<Platform> finalPlatforms;
    public List<Platform> platforms;

    [System.Serializable]
    public class Platform
    {
        public GameObject prefab;
        public float probability;
    }

    public void Start()
    {
        GenerateMap();
    }

    private void StartGenerator()
    {
        seed = (seed != 0) ? seed : Random.Range(1, 100000);
        Random.InitState(seed);
    }

    /* Genera el mapa */
    private void GenerateMap()
    {
        StartGenerator();
        GameObject start = GetRandomCopy(startPlatforms);
        GeneratePath(start, platforms, finalPlatforms, length, depth, 0);
    }

    private void GeneratePath(GameObject start, List<Platform> middlePlatforms, List<Platform> finalPlatforms, int length, int depth, int baseRotation, bool forceFirstRotation = false)
    {
        int rotation = baseRotation;
        int sectionLength = 0;
        int nextRotation = rotation;

        GameObject current = start;
        GameObject next;
        List<GameObject> generated = new List<GameObject>();

        // Middle platforms
        for (int i = 0; i < length; ++i)
        {
            do
            {
                next = GetRandomCopy(middlePlatforms);

                if (forceFirstRotation || ((sectionLength > minLength) && (Random.Range(0f, 1f) < turnRate)))
                {
                    forceFirstRotation = false;
                    nextRotation = (rotation == 0) ? ((Random.Range(0f, 1f) < .5f) ? +270 : +90) : 0;
                    sectionLength = 0;
                }

            } while (!AddNext(current, next, rotation, nextRotation));

            generated.Add(next);
            sectionLength++;
            rotation = nextRotation;
            current = next;
        }

        // Final platform
        do
        {
            next = GetRandomCopy(finalPlatforms);
        } while (!AddNext(current, next, rotation, rotation));

        if ((depth > 0) && (generated.Count > 0))
        {
            for (int i = 0; i < maxBranches; ++i)
            {
                GameObject selected = generated[Random.Range(0, generated.Count)];
                GeneratePath(selected, middlePlatforms, finalPlatforms, (int)(length * 0.2f), depth - 1, (int)selected.transform.eulerAngles.y, true);
            }
        }
    }

    /* Devuelve una instancia nueva de una lista de prefabs. De acuerdo a una lista de probabilidades si la hubiera */
    private GameObject GetRandomCopy(List<Platform> list)
    {
        GameObject instance;
        float currentMax = 0;
        float random = Random.Range(0f, 1f);
        
        foreach (Platform platform in list)
        {
            currentMax += platform.probability;
            if (random < currentMax)
            {
                instance = Instantiate(platform.prefab);
                instance.transform.position = Vector3.zero;
                instance.transform.SetParent(this.transform);
                return instance;
            }
        }
        return null;
    }
    
    /* Añade un gameobject al mapa con una conexión aleatoria */
    private bool AddNext(GameObject current, GameObject next, int rotation, int nextRotation)
    {
        next.transform.Rotate(new Vector3(0f, nextRotation, 0f), Space.World);
        var nexts = new Connection(current, Connection.Type.NEXTS, rotation);
        var prevs = new Connection(next,    Connection.Type.PREVS, nextRotation);

        Vector3 moveHere;
        Vector3 moveFrom;

        List<int> actions = new List<int>();
        
        if ((nexts.front.Count > 0) && (prevs.back .Count > 0)) actions.Add(0);
        if ((nexts.left .Count > 0) && (prevs.right.Count > 0)) actions.Add(1);
        if ((nexts.right.Count > 0) && (prevs.left .Count > 0)) actions.Add(2);
        if ((nexts.top  .Count > 0) && (prevs.bot  .Count > 0)) actions.Add(3);
        if ((nexts.bot  .Count > 0) && (prevs.top  .Count > 0)) actions.Add(4);
        
        if (actions.Count == 0)
        {
            //Debug.Log("No combination found [" + current.transform.name + " (" + rotation.ToString() + ") + " + next.transform.name + " (" + newRotation.ToString() + ")]");
            Destroy(next);
            return false;
        }
        
        switch ((int)actions[Random.Range(0, actions.Count)])
        {
            default:
            case 0: // front <=> back
                moveHere = nexts.GetRandom(nexts.front);
                moveFrom = prevs.GetRandom(prevs.back);
                break;
            case 1: // left <=> right
                moveHere = nexts.GetRandom(nexts.left);
                moveFrom = prevs.GetRandom(prevs.right);
                break;
            case 2: // right <=> left
                moveHere = nexts.GetRandom(nexts.right);
                moveFrom = prevs.GetRandom(prevs.left);
                break;
            case 3: // top <=> bot
                moveHere = nexts.GetRandom(nexts.top);
                moveFrom = prevs.GetRandom(prevs.bot);
                break;
            case 4: // bot <=> top
                moveHere = nexts.GetRandom(nexts.bot);
                moveFrom = prevs.GetRandom(prevs.top);
                break;
        }

        Vector3 traslation =  moveHere - moveFrom;
        next.transform.Translate(traslation, Space.World);
        next.transform.SetParent(this.transform);
        return true;
    }

    /* Contiene las posibles conexión que ofrece un objeto nexts o prevs, pero no ambos a la vez */
    private class Connection
    {
        public List<Vector3> front;
        public List<Vector3> back;
        public List<Vector3> top;
        public List<Vector3> bot;
        public List<Vector3> left;
        public List<Vector3> right;
        public enum Type { NEXTS, PREVS };
        
        /* Construye las conexiones de tipo next o prev */
        public Connection(GameObject obj, Type type, int _rotation)
        {
            front = new List<Vector3>();
            back  = new List<Vector3>();
            top   = new List<Vector3>();
            bot   = new List<Vector3>();
            left  = new List<Vector3>();
            right = new List<Vector3>();

            string prefix = (type == Type.NEXTS) ? "next" : "prev";
            Transform[] allChildren = obj.GetComponentsInChildren<Transform>();
            foreach (Transform transform in allChildren)
            {
                if (transform.name.Contains("up"))    AddTo(top, transform, prefix);
                if (transform.name.Contains("down"))  AddTo(bot, transform, prefix);
                if (transform.name.Contains("left"))  AddTo(left, transform, prefix);
                if (transform.name.Contains("right")) AddTo(right, transform, prefix);
                if (transform.name.Contains("front")) AddTo(front, transform, prefix);
                if (transform.name.Contains("back"))  AddTo(back, transform, prefix); 
            }
            ApplyRotation(_rotation);
        }
        
        /* Añade las conexiones contenidas un empty (top, bot, left, right, ...) a una lista */
        private void AddTo(List<Vector3> list, Transform transform, string prefix)
        {
            foreach (Transform item in transform)
                if (item.name.StartsWith(prefix))
                    list.Add(item.position);
        }
        
        private void ApplyRotation(int rotation)
        {
            List<Vector3> aux;
            if (rotation == +270)
            {
                aux   = front;
                front = right;
                right = back;
                back  = left;
                left  = aux;
            }
            if (rotation == +90)
            {
                aux   = front;
                front = left;
                left  = back;
                back  = right;
                right = aux;
            }
        }
        
        /* Devuelve una posición aleatoria de una lista de posiciones */
        public Vector3 GetRandom(List<Vector3> list)
        {
            return list[(int)Random.Range(0, list.Count)];
        }
    } 
}

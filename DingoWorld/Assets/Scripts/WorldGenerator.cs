using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public float turnRate;
    public int seed;
    
	public int length;
    public int minLength;
    public List<GameObject> startPlatforms;
    public List<GameObject> finalPlatforms;
    public List<GameObject> platforms;
    public List<float> probabilities;
    private int rotation;
    private int sectionLength;

    public void Start()
    {
        GenerateMap();
    }

    private void StartGenerator()
    {
        seed = (seed != 0) ? seed : Random.Range(1, 100000);
        rotation = 0;
        sectionLength = 0;
        Random.InitState(seed);
    }

    /* Genera el mapa */
    private void GenerateMap()
    {
        // First platform
        StartGenerator();
        GameObject current = GetRandomCopy(startPlatforms);
        GameObject next;

        // Middle platforms
        for (int i = 0; i < length; ++i)
        {
            do
            {
                next = GetRandomCopy(platforms, probabilities);
            } while (!AddNext(current, next));
            current = next;
        }
        
        // Final platform
        do
        {
            next = GetRandomCopy(finalPlatforms);
        } while (!AddNext(current, next));
    }
    
    /* Devuelve una instancia nueva de una lista de prefabs. De acuerdo a una lista de probabilidades si la hubiera */
    private GameObject GetRandomCopy(List<GameObject> list, List<float> probabilities = null)
    {
        GameObject instance;
        if (probabilities == null)
        {
            instance = Instantiate(list[(int) Random.Range(0, list.Count)]);   
        }
        else
        {
            float currentMax = 0;
            float random = Random.Range(0f, 1f);
            int index;
        
            for (index = 0; (index < probabilities.Count); ++index)
            {
                currentMax += probabilities[index];
                if (random < currentMax)
                    break;
            }        
        
            instance = Instantiate(list[index]);   
        }
        instance.transform.position = Vector3.zero;
        instance.transform.SetParent(this.transform);
        return instance;
    }
    
    /* Añade un gameobject al mapa con una conexión aleatoria */
    private bool AddNext(GameObject current, GameObject next)
    {
        int newRotation = RandomRotation();
        next.transform.Rotate(new Vector3(0f, newRotation, 0f));
        var nexts = new Connection(current, Connection.Type.NEXTS, rotation);
        var prevs = new Connection(next,    Connection.Type.PREVS, newRotation);

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
            Debug.Log("No combination found [" + current.transform.name + " (" + rotation.ToString() + ") + " + next.transform.name + " (" + newRotation.ToString() + ")]");
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
        sectionLength++;
        rotation = newRotation;
        return true;
    }

    /* Calcula una rotación aleatoria en base a la actual */
    private int RandomRotation()
    {
        if ((sectionLength > minLength) && (Random.Range(0f, 1f) < turnRate))
        {
            sectionLength = 0;
            if (rotation == 0)
                return (Random.Range(0f, 1f) < .5f) ? -90 : +90;
            else
                return 0;
        }
        return rotation;
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
            foreach (Transform transform in obj.transform)
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
            if (rotation == -90)
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

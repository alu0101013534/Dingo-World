using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public float turnRate;
    public int seed;
    
	public int length;
    public List<GameObject> startPlatforms;
    public List<GameObject> finalPlatforms;
    public List<GameObject> platforms;
    public List<float> probabilities;
    private int rotation;

    public void Start ()
    {
        rotation = 0;
        if (seed != 0)
            Random.InitState(seed);
        GenerateMap();
    }

    private void GenerateMap()
    {
        GameObject current = GetRandomCopy(startPlatforms);

        for (int i = 0; i < length; ++i)
        {
            GameObject next = GetRandomCopy(platforms, probabilities);
            AddNext(current, next);
            current = next;
        }
        
        GameObject final = GetRandomCopy(finalPlatforms);
        AddNext(current, final);
    }

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

    private void AddNext(GameObject current, GameObject next)
    {
        var nexts = new Connection(current, Connection.Type.NEXTS, rotation);
        var prevs = new Connection(next,    Connection.Type.PREVS, rotation);

        Vector3 move_here;
        Vector3 move_from;

        List<int> actions = new List<int>();

        if ((nexts.front.Count > 0) && (prevs.back .Count > 0)) actions.Add(0);
        if ((nexts.left .Count > 0) && (prevs.right.Count > 0)) actions.Add(1);
        if ((nexts.right.Count > 0) && (prevs.left .Count > 0)) actions.Add(2);
        if ((nexts.top  .Count > 0) && (prevs.bot  .Count > 0)) actions.Add(3);
        if ((nexts.bot  .Count > 0) && (prevs.top  .Count > 0)) actions.Add(4);
        
        if (actions.Count == 0)
        {
            Debug.LogError("No combination found");
            Debug.Log(current.transform.name);
            Debug.Log(next.transform.name);
            return;
        }

        switch ((int)actions[Random.Range(0, actions.Count)])
        {
            default:
            case 0: // front <=> back
                move_here = nexts.GetRandom(nexts.front);
                move_from = prevs.GetRandom(prevs.back);
                break;
            case 1: // left <=> right
                move_here = nexts.GetRandom(nexts.left);
                move_from = prevs.GetRandom(prevs.right);
                break;
            case 2: // right <=> left
                move_here = nexts.GetRandom(nexts.right);
                move_from = prevs.GetRandom(prevs.left);
                break;
            case 3: // top <=> bot
                move_here = nexts.GetRandom(nexts.top);
                move_from = prevs.GetRandom(prevs.bot);
                break;
            case 4: // bot <=> top
                move_here = nexts.GetRandom(nexts.bot);
                move_from = prevs.GetRandom(prevs.top);
                break;
        }

        Vector3 traslation =  move_here - move_from;
        next.transform.Translate(traslation);
        next.transform.SetParent(this.transform);
    }

    private class Connection
    {
        public List<Vector3> front;
        public List<Vector3> back;
        public List<Vector3> top;
        public List<Vector3> bot;
        public List<Vector3> left;
        public List<Vector3> right;
        public enum Type { NEXTS, PREVS };

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

        private void AddTo(List<Vector3> list, Transform transform, string prefix)
        {
            foreach (Transform item in transform)
                if (item.name.StartsWith(prefix))
                    list.Add(item.position);
        }

        private void ApplyRotation(int rotation)
        {
            List<Vector3> aux;
            if (rotation == 90)
            {
                aux   = front;
                front = right;
                right = back;
                back  = left;
                left  = aux;
            }
            if (rotation == -90)
            {
                aux   = front;
                front = left;
                left  = back;
                back  = right;
                right = aux;
            }
        }

        public Vector3 GetRandom(List<Vector3> list)
        {
            return list[(int)Random.Range(0, list.Count)];
        }

        private void ParseName(Transform transform, string prefix)
        {
            string name = transform.name;

            if (name.StartsWith(prefix))
            {
                if (name.Contains("front"))
                    front.Add(transform.position);
                if (name.Contains("back"))
                    back.Add(transform.position);
                if (name.Contains("top"))
                    top.Add(transform.position);
                if (name.Contains("bot"))
                    bot.Add(transform.position);
                if (name.Contains("left"))
                    left.Add(transform.position);
                if (name.Contains("right"))
                    right.Add(transform.position);
            }
        }
    } 
}

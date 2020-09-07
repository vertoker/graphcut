using System.Collections;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    public float Stable(float input, float min, float max)
    {
        float dist = max - min;
        bool b = true;
        while (b)
        {
            if (input > max)
            {
                input = input - dist;
            }
            else if (input < min)
            {
                input = input + dist;
            }
            else { b = false; }
        }
        return input;
    }
    public float Stable2(float input, float min, float max)
    {
        if (input > max)
        {
            input = max;
        }
        else if (input < min)
        {
            input = min;
        }
        return input;
    }
    public float Stable3(float input, float min, float max)
    {
        float dist = max - min;
        if (input > max)
        {
            input = input - dist;
        }
        else if (input < min)
        {
            input = input + dist;
        }
        return input;
    }
    public float MoveToward(float current, float target, float speed, Vector2 ends)
    {
        if (current > target)
        {
            current = Stable2(current - speed, target, ends.y);
        }
        else if (current < target)
        {
            current = Stable2(current + speed, ends.x, target);
        }
        return current;
    }
    public float MoveToward2(float current, float target, float speed, float min, float max)
    {
        if (current > target)
        {
            current -= speed;
            if (current < target)
            {
                current += max - target;
            }
        }
        else if (current < target)
        {
            current += speed;
            if (current > target)
            {
                current -= target - min;
            }
        }
        return current;
    }
    public float MoveToward3(float current, float target, float speed)
    {
        if (current > target)
        {
            current -= speed;
            if (current < target)
            {
                current = target;
            }
        }
        else if (current < target)
        {
            current += speed;
            if (current > target)
            {
                current = target;
            }
        }
        return current;
    }

    public int[] Add(int[] old, int addComponent)
    {
        int[] n = new int[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }
    public int[] Remove(int[] old, int removeComponent)
    {
        int[] n = new int[old.Length - 1];
        if (old.Length != 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (i != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new int[0];
        }
        return n;
    }

    public MeshRenderer[] Add(MeshRenderer[] old, MeshRenderer addComponent)
    {
        MeshRenderer[] n = new MeshRenderer[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }
    public GameObject[] Add(GameObject[] old, GameObject addComponent)
    {
        GameObject[] n = new GameObject[old.Length + 1];

        if (old.Length != 0)
        {
            for (int i = 0; i < old.Length; i++)
            {
                n[i] = old[i];
            }
        }

        n[old.Length] = addComponent;
        return n;
    }
    public GameObject[] Remove(GameObject[] old, int removeComponent)
    {
        GameObject[] n = new GameObject[old.Length - 1];
        if (old.Length != 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (i != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new GameObject[0];
        }
        return n;
    }
    public GameObject[] Remove(GameObject[] old, GameObject removeComponent)
    {
        GameObject[] n = new GameObject[old.Length - 1];
        if (old.Length != 1)
        {
            int counter = 0;

            for (int i = 0; i < old.Length; i++)
            {
                if (old[i] != removeComponent)
                {
                    n[counter] = old[i];
                    counter++;
                }
            }
        }
        else
        {
            n = new GameObject[0];
        }
        return n;
    }
}
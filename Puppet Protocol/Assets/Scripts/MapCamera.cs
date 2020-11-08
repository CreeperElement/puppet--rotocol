using UnityEngine;

public class MapCamera : MonoBehaviour
{

    float y;
    GameObject camTarget;

    // Start is called before the first frame update
    void Start()
    {
        this.y = transform.position.y;
        camTarget = GameObject.Find("r1");
    }

    public string setTarget(string name)
    {
        var potentialMatch = GameObject.Find(name);
        if (potentialMatch == null)
            return $"No robot called {name}.";
        if (potentialMatch.GetComponent<Robot>() == null)
            return $"No robot called {name}.";
        camTarget = potentialMatch;

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        var targetPos = camTarget.transform.position;
        this.transform.position = new Vector3(targetPos.x, y, targetPos.z);
    }
}

using UnityEngine;

public enum ToolType
{
    None,
    Fishing,
    Arrow
}

public class EventObjectBase : MonoBehaviour
{
    public string objName;

    virtual public void OnInteract(GameObject player) { }
    virtual public void OnToolHit(GameObject player, ToolType tool) { }
}

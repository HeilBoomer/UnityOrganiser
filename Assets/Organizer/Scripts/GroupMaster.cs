using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class GroupMaster : Master
{
    #region Variables
    [SerializeField] public List<GameObject> groupObjects = new List<GameObject>();
    [HideInInspector] public bool drawAlways = false;
    [HideInInspector] public bool drawIconsAlways = false;
    [HideInInspector] public bool draw2D = false;
    [HideInInspector] public bool recording = false;

    bool removing = false;

    [HideInInspector] public Color curveColor = new Color (0.47f, .70f,.83f,1);
    [Space]
    [HideInInspector] public bool bezierCurve = false;

    List<GameObject> carryObjects;
    #region Bezier Curve
    [Header("Bezier Curve")]
    [Tooltip("You can change the offset of the bezier curve tangents")]
    [HideInInspector] public Vector3 startTangentOffset;

    [Tooltip("You can change the offset of the bezier curve tangents")]
    [HideInInspector] public Vector3 endTangentOffset;
    [Space]

    [HideInInspector] public float width = 3;
        
    [Space]
    [HideInInspector] public Texture2D curveTexture;
    [Space]
    [Tooltip("Leave it blank if you don't want it.")]
    [HideInInspector] public bool pointPathLock = false;
    [HideInInspector] public string pointIconPath = "def_point_group.png";
    [HideInInspector] public bool objectPathLock = false;
    [HideInInspector] public string objectIconPath = "GroupIcon.png";


    public void Carry()
    {
        if (!carryObjects.Contains(gameObject)) carryObjects.Add(gameObject);
        Selection.objects = carryObjects.ToArray();

    }
    #endregion
    #endregion
    private void OnEnable()
    {
        InvokeRepeating("UpdateGraphics",0,.1f);
    }
    private void OnDisable()
    {
        CancelInvoke("UpdateGraphics");
    }
    private void Update()
    {
        if (Selection.activeGameObject != this.gameObject) return;


        // movement
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawIcon(transform.position, objectIconPath);
        if (drawAlways && groupObjects != null)
        {
            foreach (GameObject _obj in groupObjects)
            {
                if (_obj != null&& _obj != this.gameObject)
                {
                    Vector3 desiredMasterPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z * System.Convert.ToInt32(!draw2D));
                    Vector3 desiredObjectPosition = new Vector3(_obj.transform.position.x, _obj.transform.position.y, _obj.transform.position.z * System.Convert.ToInt32(!draw2D));
                    Gizmos.color = curveColor;

                    if (bezierCurve)
                    {
                        Handles.DrawBezier(desiredMasterPosition, desiredObjectPosition, desiredMasterPosition + startTangentOffset, desiredObjectPosition + endTangentOffset, curveColor, curveTexture, width);
                        if (drawIconsAlways) Gizmos.DrawIcon(desiredObjectPosition, pointIconPath, true);

                    }
                    else Gizmos.DrawLine(transform.position, _obj.transform.position);
                }
            }
        }
        if (recording)
        {
            foreach(GameObject _obj in Selection.gameObjects)
            {
                Gizmos.DrawIcon(_obj.transform.position, "def_point_cover.png", true);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (!drawAlways && groupObjects!= null)
        {
            Gizmos.color = Color.green;
            foreach (GameObject _obj in groupObjects)
            {
                if(_obj != null && _obj != this.gameObject)
                {
                    Vector3 desiredMasterPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z * System.Convert.ToInt32(!draw2D));
                    Vector3 desiredObjectPosition = new Vector3(_obj.transform.position.x, _obj.transform.position.y, _obj.transform.position.z * System.Convert.ToInt32(!draw2D));
                    Gizmos.color = curveColor;
                    if (bezierCurve)
                    {
                        Handles.DrawBezier(desiredMasterPosition, desiredObjectPosition, desiredMasterPosition + startTangentOffset, desiredObjectPosition + endTangentOffset, curveColor, curveTexture, width);
                        Gizmos.DrawIcon(desiredObjectPosition, pointIconPath, true);
                    }
                    else
                    {
                        Gizmos.DrawLine(transform.position, _obj.transform.position);
                    }
                }
                
            }
        }
        
    }
    //Update the list of objects
    void UpdateGraphics()
    {

        //groupObjects = FindObjectsWithTag(targetTag);
        carryObjects = groupObjects;
        
        //carryObjects.Add(gameObject);

    }

    public void AddObjectsToGroup(GameObject[] _toAdd)
    {
        if (groupObjects == null) return;
        foreach (GameObject go in _toAdd)
        {

            if (!groupObjects.Contains(go) && go != gameObject) groupObjects.Add(go);
        }
    }
    public void RemoveObjectsFromGroup(GameObject[] _toAdd)
    {
        if (groupObjects == null) return;
        foreach (GameObject go in _toAdd)
        {
            if (groupObjects.Contains(go) && go != gameObject) groupObjects.Remove(go);
        }
    }
    public void StartSelect()
    {
        Selection.activeGameObject = gameObject;
        ActiveEditorTracker.sharedTracker.isLocked = true;
        removing = false;
        recording = true;
        drawAlways = true;
        drawIconsAlways = true;
    }
    public void StartRemove()
    {
        Selection.activeGameObject = gameObject;
        ActiveEditorTracker.sharedTracker.isLocked = true;
        removing = true;
        recording = true;
        drawAlways = true;
        drawIconsAlways = true;
    }
    public void RecordEvents()
    {
        if (!removing) AddObjectsToGroup(Selection.gameObjects);
        else RemoveObjectsFromGroup(Selection.gameObjects);
        recording = false;
        drawAlways = false;
        drawIconsAlways = false;

        Selection.activeGameObject = gameObject;
        ActiveEditorTracker.sharedTracker.isLocked = false;
    }

    public override void SetMasterObjects(bool _value)
    {
        base.SetMasterObjects(_value);
        foreach (GameObject go in groupObjects)
        {
            go.SetActive(_value);
        }
        
    }
}

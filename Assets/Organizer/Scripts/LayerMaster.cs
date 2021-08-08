using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LayerMaster : Master
{
    #region Variables
    [SerializeField] LayerMask targetLayer = 1;
    [SerializeField] public bool drawAlways = false;
    [SerializeField] public bool drawIconsAlways = false;
    [SerializeField] bool draw2D = false;

    [SerializeField] Color curveColor = new Color (.12f,.137f,.18f,1);
    [Space]
    [HideInInspector] public bool bezierCurve = false;

    List<GameObject> layerObjects;
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
    [HideInInspector] public string pointIconPath = "def_point.png";
    [HideInInspector] public bool objectPathLock = false;
    [HideInInspector] public string objectIconPath = "LayerIcon.png";


    public void Carry()
    {
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
        if (drawAlways)
        {
            foreach (GameObject _obj in layerObjects)
            {
                Vector3 desiredMasterPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z * System.Convert.ToInt32(!draw2D));
                Vector3 desiredObjectPosition = new Vector3(_obj.transform.position.x, _obj.transform.position.y, _obj.transform.position.z * System.Convert.ToInt32(!draw2D));
                Gizmos.color = curveColor;

                if (bezierCurve)
                {
                    Handles.DrawBezier(desiredMasterPosition, desiredObjectPosition, desiredMasterPosition + startTangentOffset, desiredObjectPosition + endTangentOffset, curveColor, curveTexture, width);
                    if(drawIconsAlways)Gizmos.DrawIcon(desiredObjectPosition, pointIconPath, true);

                }

                else Gizmos.DrawLine(transform.position, _obj.transform.position);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (!drawAlways)
        {
            Gizmos.color = Color.green;
            foreach (GameObject _obj in layerObjects)
            {
                Vector3 desiredMasterPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z * System.Convert.ToInt32(!draw2D));
                Vector3 desiredObjectPosition = new Vector3(_obj.transform.position.x,_obj.transform.position.y,_obj.transform.position.z*System.Convert.ToInt32(!draw2D));
                Gizmos.color = curveColor;
                if (bezierCurve)
                {
                    Handles.DrawBezier(desiredMasterPosition, desiredObjectPosition, desiredMasterPosition + startTangentOffset, desiredObjectPosition+endTangentOffset, curveColor, curveTexture, width);
                    Gizmos.DrawIcon(desiredObjectPosition, pointIconPath, true);
                }
                else
                {

                    Gizmos.DrawLine(transform.position, _obj.transform.position);
                }
            }
        }
        
    }
    //Update the list of objects
    void UpdateGraphics()
    {
        
        layerObjects = FindObjectsWithLayer(targetLayer);
        carryObjects = FindObjectsWithLayer(targetLayer);
        carryObjects.Add(gameObject);

    }

    List<GameObject> FindObjectsWithLayer(LayerMask _layer)
    {
        GameObject[] gos = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> gosList = Organizer.ArrayToList<GameObject>(gos);
        List<GameObject> addedList = new List<GameObject>();


        foreach (GameObject go in gosList)
        {
            if (_layer == (_layer | (1 << go.layer))&& go != this.gameObject) 
            {
                addedList.Add(go);
            }
            
        }

        return addedList;
    }

    public override void SetMasterObjects(bool _value)
    {
        base.SetMasterObjects(_value);
        foreach (GameObject go in layerObjects)
        {
            go.SetActive(_value);
        }
    }
}

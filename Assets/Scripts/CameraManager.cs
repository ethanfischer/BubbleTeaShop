using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Pose _defaultPose;
    [SerializeField]
    Pose _bobaPose;
    [SerializeField]
    Pose _teaPose;
    
    //singleton unity pattern
    static CameraManager _instance;
    public static CameraManager Instance
    { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<CameraManager>();
        }
        return _instance;
    } }

    // Start is called before the first frame update
    void Start()
    {
        _defaultPose = new Pose(transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ActivateBobaPose()
    {
        transform.position = _bobaPose.position;
        transform.rotation = _bobaPose.rotation;
    }
    
    public void ActivateTeaPose()
    {
        transform.position = _teaPose.position;
        transform.rotation = _teaPose.rotation;
    }
    
    public void ActivateDefaultPose()
    {
        transform.position = _defaultPose.position;
        transform.rotation = _defaultPose.rotation;
    }
}

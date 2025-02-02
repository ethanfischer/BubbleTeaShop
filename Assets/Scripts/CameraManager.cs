using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Pose _defaultPose;
    [SerializeField]
    Pose _bobaPose;
    [SerializeField]
    Pose _teaPose;
    [SerializeField]
    Pose _cupPose;
    [SerializeField]
    GameObject _ingredientsUI;
    
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
        _ingredientsUI.GetComponent<CanvasGroup>().alpha = 0f;
        transform.position = _bobaPose.position;
        transform.rotation = _bobaPose.rotation;
    }
    
    public void ActivateTeaPose()
    {
        _ingredientsUI.GetComponent<CanvasGroup>().alpha = 0f;
        transform.position = _teaPose.position;
        transform.rotation = _teaPose.rotation;
    }
    
    public void ActivateDefaultPose()
    {
        _ingredientsUI.GetComponent<CanvasGroup>().alpha = 1f;
        transform.position = _defaultPose.position;
        transform.rotation = _defaultPose.rotation;
    }
    
    public void ActivateCupPose()
    {
        _ingredientsUI.GetComponent<CanvasGroup>().alpha = 0f;
        transform.position = _cupPose.position;
        transform.rotation = _cupPose.rotation;
    }
}

using UnityEngine;

public class LivesTrackerUI : MonoBehaviour
{
    [SerializeField] private Transform _lifeIconContainerTransform;
    [SerializeField] private Transform _lifeIconTemplateTransform;

    private void Start()
    {
        GameManager.Instance.OnLivesUpdated += OnLivesUpdated;
    }

    private void OnLivesUpdated(object sender, GameManager.IntEventArgs e)
    {
        // set lives to e.Value
        foreach(Transform child in _lifeIconContainerTransform)
        {
            if(child == _lifeIconTemplateTransform)
                continue;
            
            Destroy(child.gameObject);
        }

        for(int i = 0; i < e.Value; i++)
        {
            Transform lifeIcon = Instantiate(_lifeIconTemplateTransform, _lifeIconContainerTransform);
            lifeIcon.gameObject.SetActive(true);
        }
    }
}

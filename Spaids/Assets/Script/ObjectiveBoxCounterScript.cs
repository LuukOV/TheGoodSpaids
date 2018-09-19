using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveBoxCounterScript : MonoBehaviour
{

    public List<Sprite> _sprites = new List<Sprite>();
    private Image _image;

    int ObjectiveBoxes = 10;


    // Use this for initialization
    void Start()
    {

        Image[] _tempImages = GetComponentsInChildren<Image>();

        for (int i = 0; i < _tempImages.Length; i++)
        {
            _sprites.Add(_tempImages[i].sprite);
            if (i != ObjectiveBoxes)
            {
                Destroy(_tempImages[i].gameObject);
            }
            else
            {
                _image = _tempImages[i];
            }
        }
    }

    public void AddBox()
    {
        ObjectiveBoxes++;
        UpdateImage();
    }

    public void RemoveBox()
    {
        ObjectiveBoxes--;
        UpdateImage();
    }

    private void UpdateImage()
    {
        _image.sprite = _sprites[ObjectiveBoxes];
    }
}

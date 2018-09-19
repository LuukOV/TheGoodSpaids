using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialBoxCounterScript : MonoBehaviour {

    public List<Sprite> _sprites = new List<Sprite>();
    private Image _image;

    int SocialBoxes = 0;


	// Use this for initialization
	void Start () {

        Image[] _tempImages = GetComponentsInChildren<Image>();

        for (int i = 0; i < _tempImages.Length; i++)
        {
            _sprites.Add(_tempImages[i].sprite);
            if(i != 0)
            {
                Destroy(_tempImages[i].gameObject);
            }
            else
            {
                _image = _tempImages[i];
            }
        }

        /*foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }*/
    }

    public void AddBox()
    {
        SocialBoxes++;
        UpdateImage();
    }

    public void RemoveBox()
    {
        SocialBoxes--;
        UpdateImage();
    }
	
    private void UpdateImage()
    {
        _image.sprite = _sprites[SocialBoxes];
    }
}

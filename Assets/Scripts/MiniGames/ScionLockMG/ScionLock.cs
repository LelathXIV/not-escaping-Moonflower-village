using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScionLock : MonoBehaviour
{
    public List<GameObject> spheresInLine;
    public List<bool> _isLitBools;
    public Material litMaterial;
    public Material darkMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "miniGame")
        {
            _isLitBools.Clear();
            spheresInLine.Add(other.gameObject);
            foreach(GameObject sphere in spheresInLine)
            {
                if (sphere.GetComponent<SphereLitting>().isLit)
                    _isLitBools.Add(sphere.GetComponent<SphereLitting>().isLit);
            }
            if(_isLitBools.Count >= (spheresInLine.Count - _isLitBools.Count))
            {
                foreach (GameObject sphere in spheresInLine)
                {
                    sphere.GetComponent<MeshRenderer>().material = litMaterial;
                    sphere.GetComponent<SphereLitting>().isLit = true;
                }
            }
            else
            {
                foreach (GameObject sphere in spheresInLine)
                {
                    sphere.GetComponent<MeshRenderer>().material = darkMaterial;
                    sphere.GetComponent<SphereLitting>().isLit = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "miniGame")
        {
            spheresInLine.Remove(other.gameObject);
            _isLitBools.Clear();
        }
    }
}

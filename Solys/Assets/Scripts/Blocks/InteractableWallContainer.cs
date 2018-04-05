using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWallContainer : MonoBehaviour {

    bool isSimulation = false;
    bool isInteracted = false;
    float alpha = 0.3f;

    public bool isDeactivating = true;

    void Start()
    {
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        GeneralLogic.StartSimulationEvent += StartSimulation;
        GeneralLogic.StopSimulationEvent += StopSimulation;
        StopInteractBlock();
        foreach (Collider2D child in transform.GetComponentsInChildren<Collider2D>())
            child.enabled = false;
    }

    private void OnDisable()
    {
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
        GeneralLogic.StartSimulationEvent -= StartSimulation;
        GeneralLogic.StopSimulationEvent += StopSimulation;
    }

    private void StartSimulation()
    {
        isSimulation = true;
        foreach (Collider2D child in transform.GetComponentsInChildren<Collider2D>())
            child.enabled = true;
    }

    private void ResetSimulation()
    {
        isSimulation = false;
        isInteracted = false;
        foreach (Collider2D child in transform.GetComponentsInChildren<Collider2D>())
            child.enabled = false;
    }

    private void StopSimulation()
    {
        isSimulation = false;
        isInteracted = false;
        foreach (Collider2D child in transform.GetComponentsInChildren<Collider2D>())
            child.enabled = false;
    }

    private void Update()
    {
        if (isSimulation && Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                if (TestTouchPosition(Input.GetTouch(0).position))
                {
                    isInteracted = true;
                    StartInteractBlock();
                }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                if (isInteracted)
                {
                    isInteracted = false;
                    StopInteractBlock();
                }
        }


        //DEBUG blok
        if (isSimulation && Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TestTouchPosition(Input.mousePosition))
                {
                    isInteracted = true;
                    StartInteractBlock();
                }

            }

            
            if (Input.GetMouseButtonUp(0))
            {
                if (isInteracted)
                {
                    isInteracted = false;
                    StopInteractBlock();
                }
            }

        }
    }

    private bool TestTouchPosition(Vector2 pos)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(worldPoint.x, worldPoint.y);
        Collider2D hitCollider = Physics2D.OverlapPoint(touchPos);  
        if (hitCollider)
            return gameObject == hitCollider.gameObject.transform.parent.gameObject;
        else return false;
    }

    private void StartInteractBlock()
    {
        if(isDeactivating)
            foreach(Transform child in transform)
            {
                GameObject childObject = child.gameObject;
                childObject.GetComponent<Collider2D>().isTrigger = true;
                Color startColor = childObject.GetComponent<SpriteRenderer>().color;
                startColor.a = alpha;
                childObject.GetComponent<SpriteRenderer>().color = startColor;
            }
        else
            foreach (Transform child in transform)
            {
                GameObject childObject = child.gameObject;
                childObject.GetComponent<Collider2D>().isTrigger = false;
                Color startColor = childObject.GetComponent<SpriteRenderer>().color;
                startColor.a = 255;
                childObject.GetComponent<SpriteRenderer>().color = startColor;
            }
    }

    private void StopInteractBlock()
    {
        if(isDeactivating)
            foreach (Transform child in transform)
            {
                GameObject childObject = child.gameObject;
                childObject.GetComponent<Collider2D>().isTrigger = false;
                Color startColor = childObject.GetComponent<SpriteRenderer>().color;
                startColor.a = 255;
                childObject.GetComponent<SpriteRenderer>().color = startColor;
            }
        else
            foreach (Transform child in transform)
            {
                GameObject childObject = child.gameObject;
                childObject.GetComponent<Collider2D>().isTrigger = true;
                Color startColor = childObject.GetComponent<SpriteRenderer>().color;
                startColor.a = alpha;
                childObject.GetComponent<SpriteRenderer>().color = startColor;
            }
    }
}

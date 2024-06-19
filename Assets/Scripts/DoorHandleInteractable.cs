using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.XR.Interaction.Toolkit;  
using UnityEngine.Events;

[System.Serializable]
public class LoadSceneEvent : UnityEvent<string> { }

public class DoorHandleInteractable : XRBaseInteractable
{ 
    // Public Unity Event
    public LoadSceneEvent onSceneLoad;

    // Scene name for loading
    public string sceneName;

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // Debug log
        Debug.Log("Door handle selected. Attempting to load scene: " + sceneName);

        // Invoke the event
        if (onSceneLoad != null)
        {
            Debug.Log("onSceneLoad is not null. Invoking with scene name: " + sceneName);
            onSceneLoad.Invoke(sceneName);
        }
    }
    
    private void Start()
    {
        if (onSceneLoad == null)
        {
            onSceneLoad = new LoadSceneEvent();
        }

        // Find the SceneLoader in the scene and add its LoadScene method to onSceneLoad
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader != null)
        {
            onSceneLoad.AddListener(sceneLoader.LoadScene);
            Debug.Log("SceneLoader found and method attached.");
        }
    }
}

/*
public class DoorHandleInteractable : MonoBehaviour {  
    public string sceneName;  
    private void OnEnable(){  
    // Register the interactable event  
    var interactable = GetComponent<XRBaseInteractable>(); 
    interactable.selectEntered.AddListener(OnInteract); 
    }  

    private void OnDisable(){  
    // Unregister the interactable event  
    var interactable = GetComponent<XRBaseInteractable>(); 
    interactable.selectEntered.RemoveListener(OnInteract); 
    }  

    private void OnInteract(XRBaseInteractor interactor){  
    // Find the Scene Loader GameObject and call its LoadScene method  
    var sceneLoader = FindObjectOfType<SceneLoader>();  
        if (sceneLoader != null) { 
        sceneLoader.LoadScene(sceneName);  
        }  
    }
} */
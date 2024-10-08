using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : MonoBehaviour {

    [System.Serializable]
    private class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPool Instance { get; private set; }

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    [SerializeField] private List<Pool> _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;


    // Al activar el script nos suscribimos el evento sceneLoaded para cargar la pool 
    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Al desactivar el script nos desuscribimos
    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start(){
        LoadPool();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) {
        LoadPool();
    }

    private void LoadPool(){
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in _pools) {
            Queue<GameObject> queue = new Queue<GameObject>();

            for(int i = 0;i < pool.size;i++){
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag,Vector3 position,Quaternion rotation) {
        if(!_poolDictionary.ContainsKey(tag)){
            Debug.LogWarning($"No hay cositas con este tag: {tag}");
            return null;
        }
        
        GameObject obj = _poolDictionary[tag].Dequeue();
        
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        _poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    public T SpawnFromPool<T>(string tag,Vector3 position,Quaternion rotation){
        GameObject obj = SpawnFromPool(tag,position,rotation);
        
        T component = obj.GetComponent<T>();

        if(component == null){
            Debug.LogWarning($"No hay cositas con este component: {typeof(T).Name}");
            return component;
        }

        return component;
    }
}

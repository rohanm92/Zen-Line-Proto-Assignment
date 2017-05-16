using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    public GameObject[] levels;
    public GameObject player;

    public Text LevelText;
    public Text ScoreText;

    private GameObject active_level;
    private GameObject old_level;

    List<List<GameObject>> level_list;

    // Use this for initialization
    void Start () {
        level_list = new List<List<GameObject>>();

        // Create Level pool for all levels
        for(int i = 0; i < levels.Length; i++) {
            List<GameObject> pooled_levels = new List<GameObject>();
            for(int j = 0; j < 3; j++) {
                GameObject level = (GameObject)Instantiate(levels[i]);
                level.SetActive(false);
                pooled_levels.Add(level);
            }
            level_list.Add(pooled_levels);
        }

        active_level = level_list[0][0];
        active_level.SetActive(true);
        active_level.GetComponent<Transform>().position = new Vector3(-2.8f, 27.8f, 3.5f);
    }
    
    // Update is called once per frame
    void Update () {
    	// Update UI level and score
        LevelText.text = "Lvl " + player.GetComponent<PlayerScript>().level;
        ScoreText.text = "Score:" + player.GetComponent<PlayerScript>().score/10;

        // Check if level is about to reach top of screen
        if (active_level.GetComponent<Transform>().position.y < 10f) {
            //Increase Levels Crossed
            player.GetComponent<PlayerScript>().incrLevel();

            int curr_level = player.GetComponent<PlayerScript>().level; 
            int rand = Random.Range(0, level_list.Count);
            // Randomize level if level is 
            if (curr_level < 4) {
                rand = curr_level;
            }
            List<GameObject> pooled_levels = level_list[rand];

			// Set new active level
            for (int i = 0; i < pooled_levels.Count; i++) {
                if (!pooled_levels[i].activeInHierarchy) {
                    active_level = pooled_levels[i];
                    active_level.SetActive(true);
                    break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDownSpeechBubble : MonoBehaviour
{
    GameObject speechBubble;
    GameObject agents;
    void Start()
    {
        speechBubble = GameObject.Find("Get Down Text");
        speechBubble.SetActive(false);
        agents = GameObject.Find("Agents");
    }

    public IEnumerator ShowSpeechBubble()
    {
        int agentIndex = Random.Range(0, agents.transform.childCount);
        float randomAngle = Random.Range(-15.0f, 15.0f);
        speechBubble.transform.position = Camera.main.WorldToScreenPoint(agents.transform.GetChild(agentIndex).transform.position);
        speechBubble.transform.Rotate(0, 0, randomAngle);
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(1);
        speechBubble.SetActive(false);
    }
}

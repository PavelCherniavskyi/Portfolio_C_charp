using UnityEngine;
using System.Collections;

public class AIJourney : MonoBehaviour {

    private Vector3 startposition;
    private MoveController ComponentMoveController;
    private Attack_System ComponentAI_Infighting;
    private float time;

    void Start () {
        ComponentMoveController = GetComponent<MoveController>();
        ComponentAI_Infighting = GetComponent<Attack_System>();
        startposition = transform.position;
        time = Random.Range(2f, 10f);
    }

	void Update () {
        if (GetComponent<UnitOptions>().isDead == false)
        {
            if (ComponentAI_Infighting.target == null)
            {
                if (time > 0)
                    time -= Time.deltaTime;
                else
                {
                    time = 10f;
                    float rX = Random.Range(-1.5f, 1.5f);
                    float rY = Random.Range(-1.5f, 1.5f);
                    ComponentMoveController.Destination(startposition + new Vector3(rX, rY, 0), true);
                }
            }
        }
    }
}

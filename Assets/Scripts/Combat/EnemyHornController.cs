using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*****
Class Name: EnemyHornController
Purpose: On Enemy Phase, enemies utilize a horn to attack,
        this class holds the functions that allow enemies to make attacks
*****/
public class EnemyHornController : MonoBehaviour {

    //public static EnemyHornController hornControl;

    //Collission variables
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected Rigidbody2D rb2d;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Collider2D lensCollider;

    protected float speed = 250F; //speed the horn moves side to side at
    int count;
    
    //Prefabs for all of the generatable attack objects
    //Liekly more will be added in the future
    public Transform trebleClefObj;
    public Transform musicNoteObj;
    public Transform singleBarObj;
    public Transform doubleBarObj;
    public Transform parent;
    Transform newObject;

    //Speed the attacks move down the speed at
    public float hornTempo;

    //direction the horn should move: 1 is right, 3 is left.
    private int direction;

	// Use this for initialization
	void Start ()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //moves the horn left if the direction is set
        if(direction == 3)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        //moves the horn right if the direction is set
        else if(direction == 1)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    /*****
    Function Name: Left
    Function Type: IEnumerator
    Purpose: Moves sets the horns direction to left for a given amount of time
    Pre: Enemy phase. Takes a float time.
    Post: Horn has moved
    *****/
    public IEnumerator Left(float time)
    {
        //Sets horn direction to left
        direction = 3;
        //Waits for the amount of time neccessary
        yield return new WaitForSeconds(time);
        //Sets horn direction back to 0
        direction = 0;
        //returns
        yield return null;
    }

    /*****
    Function Name: Right
    Function Type: IEnumerator
    Purpose: Moves sets the horns direction to right for a given amount of time
    Pre: Enemy phase. Takes a float time.
    Post: Horn has moved
    *****/
    public IEnumerator Right(float time)
    {
        //Sets horn direction to right
        direction = 1;
        //Waits for the amount of time neccessary
        yield return new WaitForSeconds(time);
        //Sets horn direction back to 0
        direction = 0;
        //returns
        yield return null;
    }

    /*****
    Function Name: SetSpeed
    FunctionType: void
    Purpose: Sets the speed the horn moves at
    Pre: Takes a float newSpeed
    Post: Speed is now newSpeed
    *****/
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    /*****
    Function Name: BeginAttack
    Function Type: void
    Purpose: The openning attack for any enemy attack pattern
    Pre: Enemy Phase
    Post: Treble Clef Obj has been instantiated and is moving
    *****/
    public void BeginAttack()
    {
        //Instantiates the trebleClefObject at the horns current position and down slightly
        newObject = Instantiate(trebleClefObj, new Vector2(transform.position.x, transform.position.y - 30), trebleClefObj.rotation, parent.transform);
        //Treble Clef's movement speed gets the current tempo
        newObject.GetComponent<NoteController>().tempo = hornTempo;
    }

    /*****
    Function Name: ShootNote
    Function Type: void
    Purpose: Basic attack for any enemy attack pattern
    Pre: Enemy Phase
    Post: music note obj has been instantiated and is moving
    *****/
    public void ShootNote()
    {
        //Instantiates the musicNoteObj at the horns current position and down slightly
        newObject = Instantiate(musicNoteObj, new Vector2(transform.position.x, transform.position.y - 30), musicNoteObj.rotation, parent.transform);
        //Music Note's movement speed gets the current tempo
        newObject.GetComponent<NoteController>().tempo = hornTempo;
    }

    /*****
    Function Name: PauseAttack
    Function Type: void
    Purpose: instantiates a single bar object
            note: not an actual attack, and only servers to indicate the end of specific attack pattern,
                not neccessarily the end of an enemys phase
    Pre: Enemy Phase
    Post: Single Bar Obj has been instantiated and is moving
    *****/
    public void PauseAttack()
    {
        //Instantiates singleBarObj at the horns current position and down slightly
        newObject = Instantiate(singleBarObj, new Vector2(parent.transform.position.x, transform.position.y - 30), singleBarObj.rotation, parent.transform);
        //Single Bars's movement speed gets the current tempo
        newObject.GetComponent<NoteController>().tempo = hornTempo;
    }

    /*****
    Function Name: EndAttack
    Fuinction Type: void
    Purpose: instantiates a double bar object
            note: not an actual attack. Enemy phase ends when this object collides with an is deleted by the bar deleter object
    Pre: Enemy Phase
    Post: Double Bar Obj has been instantiated and is moving. Enemy Phase ends when object collides with bar deleter.
    *****/
    public void EndAttack()
    {
        //Instantiates the doubleBarObj at the horns current position and down slightly
        newObject = Instantiate(doubleBarObj, new Vector2(parent. transform.position.x, transform.position.y - 30), doubleBarObj.rotation, parent.transform);
        //Double Bar's movement speed gets the current tempo
        newObject.GetComponent<NoteController>().tempo = hornTempo;
    }
}

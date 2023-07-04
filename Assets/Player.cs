using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float jumpForce = 10f;

	public Rigidbody2D rb;
	public SpriteRenderer sr;
	public GameObject SmallCirclePrefab;
    public GameObject ColorChangerPrefab;
	public GameObject BigCirclePrefab;
	public GameObject Coco1;
	public GameObject Coco2;
	public GameObject Coco3;
	public string currentColor;

	public Color colorCyan;
	public Color colorYellow;
	public Color colorMagenta;
	public Color colorPink;
	public int counter=0;
	public int circle_counter = 1;
	public const int circlePos = 14;
	public bool DeadFlag = false;

	public Text GameOverText;
	public RectTransform GameOverPanel;

	void Start ()
	{
		Score.scoreValue = -1;
		SetRandomColor();
		GameOverPanel.gameObject.SetActive(false);
		GameOverText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(DeadFlag==true){
			if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))	
			{
				DeadFlag=false;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			rb.gravityScale = 4;
			rb.velocity = Vector2.up * jumpForce;
		}
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		counter++;
		if (col.tag == "StartingPoint")
		{	
			if(counter==1)
			{
			rb.velocity = Vector2.up *0;
			rb.gravityScale = 0;
			}
			return;
		}

		if (col.tag == "ColorChanger")
		{
			Score.scoreValue +=1;
			SetRandomColor();
			Destroy(col.gameObject);
			
			int cocoRandom = Random.Range(1, 3);
			if (cocoRandom == 1){
				Instantiate(Coco1,new Vector2(-3.5f,circlePos*circle_counter+5),transform.rotation);
			}
			else
			if (cocoRandom == 2){
				Instantiate(Coco2,new Vector2(3.5f,circlePos*circle_counter+5),transform.rotation);
			}
			else
			if (cocoRandom == 3){
				Instantiate(Coco3,new Vector2(3.5f,circlePos*circle_counter+5),transform.rotation);
			}


			if(Random.Range(1,4)==1)
			{
				Instantiate(BigCirclePrefab,new Vector2(0,circlePos*circle_counter),transform.rotation);
			}
			Instantiate(SmallCirclePrefab,new Vector2(0,circlePos*circle_counter),transform.rotation);
			Instantiate(ColorChangerPrefab,new Vector2(0,circlePos*circle_counter-circlePos/2),transform.rotation);
			circle_counter++;
			return;
		}
		
		if (col.tag != currentColor)
		{
			DeadFlag = true;
			if(Score.scoreValue==-1){
				GameOverText.text = "Coconut Over \nScore: 0"  + "\nPress Space or Click";
			}
			else{
			GameOverText.text = "Coconut Over \nScore: " + Score.scoreValue + "\nPress Space or Click";
			}
			GameOverText.gameObject.SetActive(true);
			GameOverPanel.gameObject.SetActive(true);
			
		}
	}

	void SetRandomColor ()
	{
		int index = Random.Range(0, 4);
		switch (index)
		{
			case 0:
				currentColor = "Cyan";
				sr.color = colorCyan;
				break;
			case 1:
				currentColor = "Yellow";
				sr.color = colorYellow;
				break;
			case 2:
				currentColor = "Magenta";
				sr.color = colorMagenta;
				break;
			case 3:
				currentColor = "Pink";
				sr.color = colorPink;
				break;
		}
	}
}

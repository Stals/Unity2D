using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	public float jetpackForce = 75.0f;
	public float forwardMovementSpeed = 3.0f;


	public Transform groundCheckTransform;
	private bool grounded;
	public LayerMask groundCheckLayerMask;

	private bool jetpackActive = false;
	
	Animator animator;

	public ParticleSystem jetpack;

	private bool dead = false;

	private uint coins = 0;

    public uint getCointsCount()
    {
        return coins;
    }

	void AdjustJetpack (bool jetpackActive)
	{
		jetpack.enableEmission = !grounded;
		jetpack.emissionRate = jetpackActive ? 300.0f : 75.0f; 
	}

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateGroundedStatus()
	{
		//1
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.05f, groundCheckLayerMask);
		
		//2
		animator.SetBool("grounded", grounded);
	}

	void FixedUpdate()
	{
		updateVerticalSpeed();
		updateHorizontalSpeed();

		UpdateGroundedStatus();
		AdjustJetpack(jetpackActive);
	}

	void updateVerticalSpeed()
	{
		jetpackActive = Input.GetButton("Fire1");
		jetpackActive = jetpackActive && !dead;
		if (jetpackActive){
			rigidbody2D.AddForce(new Vector2(0, jetpackForce));
		}
	}

	void updateHorizontalSpeed()
	{
		if (!dead) {
			Vector2 newVelocity = rigidbody2D.velocity;
			newVelocity.x = forwardMovementSpeed;
			rigidbody2D.velocity = newVelocity;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag ("Coins")) {
			CollectCoin (collider);
		}else {
			HitByLaser (collider);
		}
	}
	
	void HitByLaser(Collider2D laserCollider)
	{
		dead = true;
		animator.SetBool("dead", true);
	}

	void CollectCoin(Collider2D coinCollider)
	{
		coins++;
		
		Destroy(coinCollider.gameObject);
	}


    void OnGUI()
    {
        DisplayRestartButton();
    }

    void DisplayRestartButton()
    {
        if (dead && grounded)
        {
            Rect buttonRect = new Rect(Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
            if (GUI.Button(buttonRect, "Tap to restart!"))
            {
                Application.LoadLevel (Application.loadedLevelName);
            };
        }
    }
}

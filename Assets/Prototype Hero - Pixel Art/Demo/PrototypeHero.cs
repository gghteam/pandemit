using UnityEngine;
using System.Collections;

public class PrototypeHero : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] float m_maxSpeed = 4.5f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_dodgeForce = 8.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] bool m_hideSword = false;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private SpriteRenderer m_SR;
    private ParticleSystem particle;

    [SerializeField]
    private Sensor_Prototype m_groundSensor;
    [SerializeField]
    private Sensor_Prototype m_wallSensorR1;
    [SerializeField]
    private Sensor_Prototype m_wallSensorR2;
    [SerializeField]
    private Sensor_Prototype m_wallSensorL1;
    [SerializeField]
    private Sensor_Prototype m_wallSensorL2;

    public bool m_grounded = false;
    public bool m_moving = false;
    private bool m_dead = false;
    private bool m_dodging = false;
    public bool m_wallSlide = false;
    private bool m_ledgeGrab = false;
    private bool m_ledgeClimb = false;
    private bool m_crouching = false;
    private Vector3 m_climbPosition;
    public int m_facingDirection = 1;
    private float m_disableMovementTimer = 0.0f;
    private float m_respawnTimer = 0.0f;
    private Vector3 m_respawnPosition = Vector3.zero;
    private int m_currentAttack = 0;
    public float curtime;
    public float cooltime;
    public chest chestcode;
    public Transform pos;
    public Transform attackeffectpos;
    public Vector2 boxsize;
    public GameObject attackshaft;
    public GameObject dil;
    public GameObject playercamera;
    public int damage;
    public Color attackCOLOR;

    //대화
    public TextManager textmanager;



    // AE_SheathSwordUse this for initialization
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_SR = GetComponentInChildren<SpriteRenderer>();
        m_body2d.AddForce(new Vector2(200,200));
        attackeffectpos = GameObject.Find("attackeffectpos").transform;

        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_Prototype>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_Prototype>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_Prototype>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_Prototype>();
    }

    // Update is called once per frame ������ ���� ����
    void Update()
    {
        // Decrease death respawn timer  ������ Ÿ�̸� ����
        m_respawnTimer -= Time.deltaTime;

        // Increase timer that controls attack combo ���� �޺� ���� Ÿ�̸� ����

        // Decrease timer that disables input movement. Used when attacking �Է� �̵��� ��Ȱ��ȭ�ϴ� Ÿ�̸Ӹ� ���Դϴ�. ���� �� ���
        m_disableMovementTimer -= Time.deltaTime;

        // Respawn Hero if dead ���ΰ� ������ ��Ȱ
        if (m_dead && m_respawnTimer < 0.0f)
            RespawnHero();

        if (m_dead)
            return;

        


        // -- Handle Animations -- �ִϸ��̼� ó��
        //Death ����
        if (Input.GetKeyDown("e") && !m_dodging)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            m_respawnTimer = 2.5f;
            DisableWallSensors();
            m_dead = true;
        }

        //Hurt ����
        else if (Input.GetKeyDown("q") && !m_dodging)
        {
            m_animator.SetTrigger("Hurt");
            // Disable movement 
            m_disableMovementTimer = 0.1f;
            DisableWallSensors();
        }

        //Attack ������
        Onmove();







        Onattack();

            //Leading chest
            Debug.DrawRay(m_body2d.position, Vector3.right, new Color(0, 1, 0));
            Debug.DrawRay(m_body2d.position, Vector3.left, new Color(1, 1, 1));

        RaycastHit2D rayHit = Physics2D.Raycast(m_body2d.position, Vector3.right, 1, LayerMask.GetMask("chest"));
        if(Input.GetKeyDown(KeyCode.T))
        {
            if( rayHit.collider.tag == "chest")
            {
                if (chestcode.isopen == 0)
                {
                    chestcode.isopen++;
                    chestcode.change();
                    chestcode.reward_event();

                }
            }
            if (rayHit.collider.tag == "sign")
            {
                textmanager.Action(rayHit.collider.gameObject);
            }
        }

       
    }
    //움직임
    public void Onmove()
    {
        //if (textmanager.isAction) return;
        // Dodge ȸ����
        //Check if character just landed on the ground ���� ���������� �˻�
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling �������� �ִ��� �˻�
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement -- �Է°� ������
        float inputX = 0.0f;

        if (m_disableMovementTimer < 0.0f && curtime <= 0)
            inputX = Input.GetAxis("Horizontal");
        else
            inputX = 0;

        // GetAxisRaw returns either -1, 0 or 1 ���� ������ �Է�
        float inputRaw = Input.GetAxisRaw("Horizontal");

        // Check if character is currently moving ���ΰ��� �̵� ������ �˻�
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
            m_moving = true;
        else
            m_moving = false;

        // Swap direction of sprite depending on move direction q �̵����⿡ ���� ��������Ʈ ����
        if (inputRaw > 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb && curtime < 0)
        {
            m_SR.flipX = false;
            m_facingDirection = 1;
            attackshaft.transform.localScale = new Vector3(1, 1, 1);
        }

        else if (inputRaw < 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb && curtime < 0)
        {
            m_SR.flipX = true;
            m_facingDirection = -1;
            attackshaft.transform.localScale = new Vector3(-1, 1, 1);
        }

        // SlowDownSpeed helps decelerate the characters when stopping ������
        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
        // Set movement //������ ����
        if (!m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching)
        {
            if (m_grounded)
            {
                m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);
            }
            else
            {
                if (Mathf.Abs(m_body2d.velocity.x) < m_maxSpeed)
                {
                    float airControl = 3.0f;
                    m_body2d.velocity += new Vector2(inputRaw * m_maxSpeed * airControl * Time.deltaTime, 0);
                    m_body2d.velocity = new Vector2(Mathf.Clamp(m_body2d.velocity.x, -m_maxSpeed, m_maxSpeed), m_body2d.velocity.y);
                }
            }
        }

        // AE_WallSlideSet AirSpeed in animator �ִϸ����� AirSpeedY ����
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword ���� ����� ���� �ִϸ��̼� ���̾�
        int boolInt = m_hideSword ? 1 : 0;
        m_animator.SetLayerWeight(1, boolInt);


        if (m_wallSensorR1 && m_wallSensorR2 && m_wallSensorL1 && m_wallSensorL2)
        {
            //Wall Slide �� �̲�����
            bool prevWallSlide = m_wallSlide;
            m_wallSlide = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
            if ((m_wallSensorR1.State() && m_wallSensorR2.State()) && m_facingDirection == -1 || (m_wallSensorL1.State() && m_wallSensorL2.State()) && m_facingDirection == 1)
            {
                m_wallSlide = false;
            }
            if (m_grounded)
                m_wallSlide = false;
            m_animator.SetBool("WallSlide", m_wallSlide);
            //Play wall slide sound �̲������� �Ҹ�
            if (!m_wallSlide)
                AudioManager_PrototypeHero.instance.StopSound("WallSlide");


            //Grab Ledge ���� ���
            bool shouldGrab = !m_ledgeClimb && !m_ledgeGrab && ((m_wallSensorR1.State() && !m_wallSensorR2.State()) || (m_wallSensorL1.State() && !m_wallSensorL2.State()));
            if (shouldGrab)
            {
                Vector3 rayStart;
                if (m_facingDirection == 1)
                    rayStart = m_wallSensorR2.transform.position + new Vector3(0.2f, 0.0f, 0.0f);
                else
                    rayStart = m_wallSensorL2.transform.position - new Vector3(0.2f, 0.0f, 0.0f);

                var hit = Physics2D.Raycast(rayStart, Vector2.down, 1.0f);

                GrabableLedge ledge = null;
                if (hit)
                    ledge = hit.transform.GetComponent<GrabableLedge>();

                if (ledge)
                {
                    m_ledgeGrab = true;
                    m_body2d.velocity = Vector2.zero;
                    m_body2d.gravityScale = 0;

                    m_climbPosition = ledge.transform.position + new Vector3(ledge.topClimbPosition.x, ledge.topClimbPosition.y, 0);
                    if (m_facingDirection == 1)
                        transform.position = ledge.transform.position + new Vector3(ledge.leftGrabPosition.x, ledge.leftGrabPosition.y, 0);
                    else
                        transform.position = ledge.transform.position + new Vector3(ledge.rightGrabPosition.x, ledge.rightGrabPosition.y, 0);
                }
                m_animator.SetBool("LedgeGrab", m_ledgeGrab);
            }

        }
        if (Input.GetKeyDown("left shift") && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb)
        {
            m_dodging = true;
            m_crouching = false;
            m_animator.SetBool("Crouching", false);
            m_animator.SetTrigger("Dodge");
            m_body2d.velocity = new Vector2(m_facingDirection * m_dodgeForce, m_body2d.velocity.y);
        }

        // Ledge Climb ���� ����
        else if (Input.GetKeyDown(KeyCode.Space) && m_ledgeGrab)
        {
            DisableWallSensors();
            m_ledgeClimb = true;
            m_body2d.gravityScale = 0;
            m_disableMovementTimer = 6.0f / 14.0f;
            m_animator.SetTrigger("LedgeClimb");
        }

        // Ledge Drop ���� ����
        else if (Input.GetKeyDown(KeyCode.LeftControl) && m_ledgeGrab)
        {
            DisableWallSensors();
        }
        //Jump ����
        else if (Input.GetKeyDown(KeyCode.Space) && (m_grounded || m_wallSlide) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching)
        {
            // Check if it's a normal jump or a wall jump �Ϲ��������� ���������� �˻�
            if (!m_wallSlide)
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            else
            {
                m_body2d.velocity = new Vector2(-m_facingDirection * m_jumpForce / 3.0f, m_jumpForce);
                m_facingDirection = -m_facingDirection;
                m_SR.flipX = !m_SR.flipX;
            }

            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_groundSensor.Disable(0.2f);
        }

        //Crouch / Stand up �ɱ�/�Ͼ��
        else if (Input.GetKeyDown(KeyCode.LeftControl) && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb)
        {
            m_crouching = true;
            m_animator.SetBool("Crouching", true);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x / 2.0f, m_body2d.velocity.y);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && m_crouching)
        {
            m_crouching = false;
            m_animator.SetBool("Crouching", false);
        }

        //Run �޸���
        if (m_moving)
            m_animator.SetInteger("AnimState", 1);

        //Idle ������ �ֱ�
        else
            m_animator.SetInteger("AnimState", 0);
    }
    //공격
    public void Onattack()
    {
//        if (textmanager.isAction) return;
        if (curtime < 0)
        {
            if (Input.GetMouseButtonDown(0) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && !m_wallSlide)
            {


                //Input.ResetInputAxes();
                m_currentAttack++;


                // Loop back to one after second attack ����2�� ����� �� ����1 ���ư�
                if (m_currentAttack > 2)
                {
                    curtime = 0.2f;
                    m_currentAttack = 1;
                }
                else curtime = 0.4f;

                // Reset Attack combo if time since last attack is too large ������ ���� ���� �ð��� �� �� ��� ����1�� �ʱ�ȭ
                if (curtime < 0)
                {
                    m_currentAttack = 1;
                }
                // Call one of the two attack animations "Attack1" or "Attack2" �� �ִϸ��̼� ����1,����2�� �ϳ��� �θ� 
                m_animator.SetTrigger("Attack" + m_currentAttack);

                if (m_currentAttack == 1)
                    damage = Random.Range(9, 12);
                else
                    damage = Random.Range(10, 13);


                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "monster")
                    {
                        attack(collider.gameObject, new Color(0.682353f, 0, 0, 1));
                        collider.GetComponent<Enemy>().Hit(damage);
                        //Debug.Log("hit");
                        //Debug.Log(collider.tag);
                    }

                    if (collider.tag == "box")
                    {
                        attack(collider.gameObject, new Color(0.3679245f, 0.2641726f, 0.1853506f, 1));
                        collider.GetComponent<boxbox>().boxhit(damage);
                        //attackCOLOR = new Color(0.682353f,0,0,1);
                    }
                }
                // Reset timer Ÿ�̸� ����

                if (m_grounded)
                {
                    // Disable movement ������ ����
                    if (m_grounded == true)
                        m_disableMovementTimer = 0.35f;
                }
            }
        }
        else curtime -= Time.deltaTime;
    }

    // Function used to spawn a dust effect ���� ����
    // All dust effects spawns on the floor �ٴڿ� ����ϴ� ���� ȿ��
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero


    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position ������ ���� ��ġ ����
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // PlaySoundTurn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }
    public void damagedani()
    {
            m_animator.SetTrigger("Hurt");
            // Disable movement ������ ����
            m_disableMovementTimer = 0.1f;
            DisableWallSensors();
    }
    void DisableWallSensors()
    {
        m_ledgeGrab = false;
        m_wallSlide = false;
        m_ledgeClimb = false;
        m_wallSensorR1.Disable(0.8f);
        m_wallSensorR2.Disable(0.8f);
        m_wallSensorL1.Disable(0.8f);
        m_wallSensorL2.Disable(0.8f);
        m_body2d.gravityScale = 2;
        m_animator.SetBool("WallSlide", m_wallSlide);
        m_animator.SetBool("LedgeGrab", m_ledgeGrab);
    }

    // Called in AE_resetDodge in PrototypeHeroAnimEvents �ִϸ��̼��̺�Ʈ���� ȸ�Ǹ����� �θ���
    public void ResetDodging()
    {
        m_dodging = false;
    }

    public void SetPositionToClimbPosition()
    {
        transform.position = m_climbPosition;
        m_body2d.gravityScale = 2;
        m_wallSensorR1.Disable(3.0f / 14.0f);
        m_wallSensorR2.Disable(3.0f / 14.0f);
        m_wallSensorL1.Disable(3.0f / 14.0f);
        m_wallSensorL2.Disable(3.0f / 14.0f);
        m_ledgeGrab = false;
        m_ledgeClimb = false;
    }

    public bool IsWallSliding()
    {
        return m_wallSlide;
    }
    void attack(GameObject collider,Color color){
        
        GameObject hello = Instantiate (dil);
        particle = hello.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule parmain = particle.main;
        parmain.startColor=color;
        hello.transform.position=(attackeffectpos.transform.position+new Vector3(Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f),0));
        hello.GetComponent<damage>().damagechk = damage;
        playercamera.GetComponent<playercamera>().startshake(0.2f,0.1f);
    }

    void RespawnHero()
    {
        transform.position = Vector3.zero;
        m_dead = false;
        m_animator.Rebind();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }
}

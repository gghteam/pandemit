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
    private Sensor_Prototype m_groundSensor;
    private Sensor_Prototype m_wallSensorR1;
    private Sensor_Prototype m_wallSensorR2;
    private Sensor_Prototype m_wallSensorL1;
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
    public Transform pos;
    public Vector2 boxsize;
    public GameObject attackshaft;
    public GameObject dil;
    public GameObject playercamera;
    public int damage;



    // AE_SheathSwordUse this for initialization
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_SR = GetComponentInChildren<SpriteRenderer>();
        m_body2d.AddForce(new Vector2(200,200));

        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_Prototype>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_Prototype>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_Prototype>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_Prototype>();
    }

    // Update is called once per frame 프레임 마다 실행
    void FixedUpdate()
    {
        // Decrease death respawn timer  리스폰 타이머 감소
        m_respawnTimer -= Time.deltaTime;

        // Increase timer that controls attack combo 공격 콤보 제어 타이머 증가

        // Decrease timer that disables input movement. Used when attacking 입력 이동을 비활성화하는 타이머를 줄입니다. 공격 시 사용
        m_disableMovementTimer -= Time.deltaTime;

        // Respawn Hero if dead 주인공 죽으면 부활
        if (m_dead && m_respawnTimer < 0.0f)
            RespawnHero();

        if (m_dead)
            return;

        //Check if character just landed on the ground 땅에 떨어졌는지 검사
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling 떨어지고 있는지 검사
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement -- 입력과 움직임
        float inputX = 0.0f;

        if (m_disableMovementTimer < 0.0f && curtime <= 0)
            inputX = Input.GetAxis("Horizontal");
        else
            inputX = 0;

        // GetAxisRaw returns either -1, 0 or 1 왼쪽 오른쪽 입력
        float inputRaw = Input.GetAxisRaw("Horizontal");

        // Check if character is currently moving 주인공이 이동 중인지 검사
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
            m_moving = true;
        else
            m_moving = false;

        // Swap direction of sprite depending on move direction q 이동방향에 따른 스프라이트 반전
        if (inputRaw > 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb && curtime <0)
        {
            m_SR.flipX = false;
            m_facingDirection = 1;
            attackshaft.transform.localScale=new Vector3(1,1,1);
        }

        else if (inputRaw < 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb && curtime <0)
        {
            m_SR.flipX = true;
            m_facingDirection = -1;
            attackshaft.transform.localScale=new Vector3(-1,1,1);
        }

        // SlowDownSpeed helps decelerate the characters when stopping 마찰력
        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
        // Set movement //움직임 세팅
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

        // AE_WallSlideSet AirSpeed in animator 애니메이터 AirSpeedY 변수
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword 검을 숨기기 위한 애니메이션 레이어
        int boolInt = m_hideSword ? 1 : 0;
        m_animator.SetLayerWeight(1, boolInt);


        if (m_wallSensorR1 && m_wallSensorR2 && m_wallSensorL1 && m_wallSensorL2)
        {
            //Wall Slide 벽 미끄러짐
            bool prevWallSlide = m_wallSlide;
            m_wallSlide = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
            if ((m_wallSensorR1.State() && m_wallSensorR2.State()) && m_facingDirection == -1 || (m_wallSensorL1.State() && m_wallSensorL2.State()) && m_facingDirection == 1)
            {
                m_wallSlide = false;
            }
            if (m_grounded)
                m_wallSlide = false;
            m_animator.SetBool("WallSlide", m_wallSlide);
            //Play wall slide sound 미끄러지는 소리
            if (!m_wallSlide)
                AudioManager_PrototypeHero.instance.StopSound("WallSlide");


            //Grab Ledge 절벽 잡기
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


        // -- Handle Animations -- 애니메이션 처리
        //Death 죽음
        if (Input.GetKeyDown("e") && !m_dodging)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            m_respawnTimer = 2.5f;
            DisableWallSensors();
            m_dead = true;
        }

        //Hurt 맞음
        else if (Input.GetKeyDown("q") && !m_dodging)
        {
            m_animator.SetTrigger("Hurt");
            // Disable movement 
            m_disableMovementTimer = 0.1f;
            DisableWallSensors();
        }

        //Attack 공격함






        // Dodge 회피함
        else if (Input.GetKeyDown("left shift") && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb)
        {
            m_dodging = true;
            m_crouching = false;
            m_animator.SetBool("Crouching", false);
            m_animator.SetTrigger("Dodge");
            m_body2d.velocity = new Vector2(m_facingDirection * m_dodgeForce, m_body2d.velocity.y);
        }

        // Ledge Climb 절벽 점프
        else if (Input.GetButtonDown("Jump") && m_ledgeGrab)
        {
            DisableWallSensors();
            m_ledgeClimb = true;
            m_body2d.gravityScale = 0;
            m_disableMovementTimer = 6.0f / 14.0f;
            m_animator.SetTrigger("LedgeClimb");
        }

        // Ledge Drop 절벽 놓기
        else if (Input.GetKeyDown(KeyCode.LeftControl) && m_ledgeGrab)
        {
            DisableWallSensors();
        }
        //Jump 점프
        else if (Input.GetButtonDown("Jump") && (m_grounded || m_wallSlide) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching)
        {
            // Check if it's a normal jump or a wall jump 일반점프인지 벽점프인지 검사
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

        //Crouch / Stand up 앉기/일어서기
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

        //Run 달리기
        if (m_moving)
            m_animator.SetInteger("AnimState", 1);

        //Idle 가만히 있기
        else
            m_animator.SetInteger("AnimState", 0);
        if (curtime < 0)
        {
            if (Input.GetMouseButtonDown(0) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && !m_wallSlide)
            {
                

                //Input.ResetInputAxes();
                m_currentAttack++;
                

                // Loop back to one after second attack 공격2를 사용한 후 공격1 돌아감
                if (m_currentAttack > 2){
                    curtime = 0.2f;
                    m_currentAttack = 1;
                }
                else curtime = 0.4f;

                // Reset Attack combo if time since last attack is too large 마지막 공격 이후 시간이 다 될 경우 공격1로 초기화
                if (curtime < 0){
                    m_currentAttack = 1;
                }
                // Call one of the two attack animations "Attack1" or "Attack2" 두 애니메이션 공격1,공격2중 하나를 부름 
                m_animator.SetTrigger("Attack" + m_currentAttack);

                if (m_currentAttack==1)
                    damage=Random.Range(9,12);
                else 
                    damage=Random.Range(10,13);


                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "monster"){
                        collider.GetComponent<wolf>().Hit(damage);
                        GameObject hello = Instantiate (dil);
                        hello.transform.position=(collider.transform.position+new Vector3(Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f),0));
                        hello.GetComponent<damage>().damagechk = damage;
                        playercamera.GetComponent<playercamera>().startshake(0.2f,0.1f);
                        //Debug.Log("hit");
                    //Debug.Log(collider.tag);
                    }
                }
                // Reset timer 타이머 리셋

                if (m_grounded)
                {
                    // Disable movement 움직임 끄기
                    if (m_grounded==true)
                    m_disableMovementTimer = 0.35f;
                }
            }
        }
        else curtime -= Time.deltaTime;
    }

    // Function used to spawn a dust effect 먼지 생성
    // All dust effects spawns on the floor 바닥에 산란하는 먼지 효과
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero
    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position 생선한 먼지 위치 설정
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // PlaySoundTurn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }
    public void damagedani()
    {
            m_animator.SetTrigger("Hurt");
            // Disable movement 움직임 끄기
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

    // Called in AE_resetDodge in PrototypeHeroAnimEvents 애니메이션이벤트에서 회피리셋을 부른다
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

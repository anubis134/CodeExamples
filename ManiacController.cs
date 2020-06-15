using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.SceneManagement;


public class ManiacController : MonoBehaviour, IPunObservable { 
  CharacterController characterController;

    Vector3 moveDir;
public float jumpSpeed = 8.0f;
public float gravity = 20.0f;

private Vector3 moveDirection = Vector3.zero;

    Vector3 desiredMove;
   public float VerticalSpeedforJump = 0;
    public GameObject PlayerPrefab;
    public Animator PlayerAnim;
    Conditions cond;
    [SerializeField]
    private bool IsDeath = false;
    private GameObject DeathPlayer;
    public GameObject Axe;
    private bool CheckCondOfAttack = false;

    private Camera ManiacCam;

    // Скорость передвижения игрока
    [SerializeField] private float speed = 10.0f;
    public PhotonView View;

    // Компонент CharacterController
    private CharacterController cc;
    // Start is called before the first frame update
    public string forTest;
    public string second;

    Quaternion PlayerRot;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        forTest ="" + Random.Range(0, 6);
        cc = GetComponent<CharacterController>();
      

        ManiacCam = GetComponentInChildren<Camera>();


    }
    private void Update()
    {
        JumpLogic();
        CheckStationOfTheCondAttack(CheckCondOfAttack);
        Debug.DrawRay(Axe.transform.position, Axe.transform.TransformDirection(Vector3.forward) * 0.3f, Color.black,0.1f);
        PlayerAnim.SetInteger("Cond", (int)cond);
        MovementLogic();
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        AnimationsLogic();
        if (DeathPlayer != null) {
            DeathPlayer.transform.rotation = PlayerRot;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
       
        
    }

    private void MovementLogic()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Записываем данные в отдельную переменную
        Vector2 input = new Vector2(horizontal, vertical);
        // Определяем направление движения
         desiredMove = transform.forward * input.y + transform.right * input.x;
         moveDir = new Vector3(desiredMove.x * speed, VerticalSpeedforJump + (-3f), desiredMove.z * speed);
        // Передвигаем объект
        cc.Move(moveDir * Time.fixedDeltaTime);
        if (VerticalSpeedforJump > 0) {
            VerticalSpeedforJump--;
        }
    }
    private void AnimationsLogic()
    {

        if (!PlayerAnim.GetBool("Attack"))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                cond = Conditions.Walk;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                cond = Conditions.Idle;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                cond = Conditions.Walk;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                cond = Conditions.Idle;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                cond = Conditions.Walk;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                cond = Conditions.Idle;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                cond = Conditions.Walk;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                cond = Conditions.Idle;
            }
        }


    }

    public void JumpLogic() {
        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                VerticalSpeedforJump = 12f;
            }
        }
       
    }

    private void Attack() {
        if (!PlayerAnim.GetBool("Attack"))
        {
            Invoke("StopAnimAttack", 1f);
            PlayerAnim.SetBool("Attack", true);
            CheckCondOfAttack = true;
        }
    }
   
   

    enum Conditions { 
    Idle,
    Walk,
    Run,
    Attack
    
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           
            stream.SendNext(forTest);
            Debug.Log("IsWrite" + forTest);
        }
        else
        {

            second = (string) stream.ReceiveNext();
            Debug.Log("IsRecieve" + second);
        }
       
    }

   public void ReConnect() {
        
            PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
        Debug.Log("rec");
    }
    public void StopAnimAttack() {
        PlayerAnim.SetBool("Attack", false);
        CheckCondOfAttack = false;
    }

    public void CheckStationOfTheCondAttack(bool CheckCond) {
        if (CheckCond) {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(Axe.transform.position, Axe.transform.TransformDirection(Vector3.forward) * 0.3f, out hit))
            {
                if (hit.collider.gameObject.tag == "Player" && Vector3.Distance(Axe.transform.position, hit.collider.gameObject.transform.position) < 1f)
                {
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("DeathPlayerFunction", RpcTarget.All);
                   


                }
            }
        }
    }
}
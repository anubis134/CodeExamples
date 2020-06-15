using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMine : MonoBehaviour
{
    private PhotonView View;
    public GameObject Camera;
    public PlayerController PlayerControl;
    public ManiacController ManiacControl;
    public CameraRotation CamRotation;
    public bool IsDeath = false;
    public Quaternion PlayerRot;
    // Start is called before the first frame update
    void Start()
    {
        View = gameObject.GetComponent<PhotonView>();
        
        if (!View.IsMine) {
            
            Camera.SetActive(false);
            PlayerControl.enabled = false;
            ManiacControl.enabled = false;
            CamRotation.enabled = false;
                    } 
    }

    // Update is called once per frame
    void Update()
    {

        if (IsDeath)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
            ManiacControl.enabled = false;
        }



    }


    [PunRPC]
    public void DeathPlayerFunction()
    {
        IsDeath = true;

    }
}

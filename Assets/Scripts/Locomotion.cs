using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    // 十字キーとマウスで操作(矢印キー＝前後左右移動，マウス＝回転)
    // 参考:https://qiita.com/tomopiro/items/87b634e98975b3c87c26

    public float speed = 6.0F;          //歩行速度
    public float gravity = 20.0F;       //重力の大きさ
    public float rotateSpeed = 1.0F;    //回転速度
    public float camRotSpeed = 5.0f;    //視点の上下スピード
    // public float jumpSpeed = 8.0F;      //ジャンプ力

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float h,v;
    private float mX, mY;
    private float lookUpAngle;

    void Start ()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update ()
    {
        h = Input.GetAxis ("Horizontal");      //左右矢印キーの値(-1.0~1.0)
        v = Input.GetAxis ("Vertical");        //上下矢印キーの値(-1.0~1.0)
        mX = Input.GetAxis ("Mouse X");        //マウスの左右移動量(-1.0~1.0)
        mY = Input.GetAxis ("Mouse Y") * -1;   //マウスの上下移動量(-1.0~1.0)

        //カメラのみ上下に回転させる，180-100=80より上下60度まで見ることができる
        lookUpAngle = Camera.main.transform.eulerAngles.x - 180 + camRotSpeed * mY;
        if (Mathf.Abs (lookUpAngle) > 100)
            Camera.main.transform.Rotate (new Vector3 (camRotSpeed * mY, 0, 0));

        //キャラクターの移動と回転
        if (controller.isGrounded) {
            moveDirection = speed * new Vector3 (h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            gameObject.transform.Rotate (new Vector3 (0, rotateSpeed * mX, 0));
            // if (Input.GetButton("Jump"))
            //     moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RequireComponent  :  해당 컴포넌트를 자동으로 추가함
[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]

public class Player : MonoBehaviour
{
    public float moveSpeed = 5;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    void Update()
    {
        //수평(Horizontal)과 수직(Vertical)방향에 대한 입력을 받음
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // normalized  :  벡터의 방향값을 가져옴
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        //화면 상에서 마우스의 위치를 반환
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }

        //발사 입력 : 마우스 좌측 버튼 입력 받기
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}

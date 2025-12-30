using UnityEngine;

public class PlayerAnimConnector : MonoBehaviour
{
    private Animator anim;
    private PlayerMove moveScript; 

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<PlayerMove>();
    }

    void Update()
    {
        // 1. 친구의 코드에서 'move' 변수를 직접 가져오기 어렵다면 
        // 입력값(movement)의 크기를 확인하여 Speed 파라미터를 조절합니다.
        // 현재 PlayerMove에서 movement는 private이므로, 
        // 실제 캐릭터가 한 프레임당 이동한 거리를 계산하는 게 가장 확실합니다.

        // 캐릭터의 실제 물리적 속도 계산
        float currentSpeed = moveScript.enabled ? moveScript.transform.InverseTransformDirection(GetComponent<Rigidbody>()?.linearVelocity ?? Vector3.zero).magnitude : 0;

        // 하지만 더 간단하게, 캐릭터가 이동 중인지를 'move' 벡터의 크기로 판단합니다.
        // (친구의 스크립트에서 move를 public으로 바꾸거나, 아래와 같이 현재 위치 변화를 감지)

        // 가장 쉬운 방법: 애니메이터에게 현재 '입력'이 있는지 알려주기
        // (이 부분은 친구의 PlayerMove 스크립트 Update문 끝에 추가하는 것이 가장 빠릅니다)
    }
}
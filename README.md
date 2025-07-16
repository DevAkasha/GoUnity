GoUnity - 3D 메타버스 프로젝트
📋 프로젝트 소개
GoUnity는 범용성을 중시하여 튼튼한 토대를 만들고자 노력한 3D 메타버스 프로젝트입니다. 복잡한 캐릭터 시스템을 다루며, 제네릭을 활용한 확장 가능한 아키텍처에 중점을 두어 개발되었습니다.
Unity-Chan을 활용한 3D 액션 어드벤처 게임으로, 반응형 아키텍처(Reactive Architecture)와 모듈화된 컴포넌트 시스템을 통해 확장성과 유지보수성을 보장합니다.
🎮 게임 특징

3D 캐릭터 컨트롤: Unity-Chan을 활용한 부드러운 캐릭터 조작
다양한 시점: 1인칭/3인칭 시점 전환 가능
상호작용 시스템: 문, 트램펄린, 체력팩터, 스피드팩터 등 다양한 오브젝트
상태 관리: HP, 스태미나 등 캐릭터 상태 시스템
직관적 UI: 상태 표시, 프롬프트, 상호작용 안내

🎯 조작법
키동작W/A/S/D이동Space점프Left Shift수동 걷기F상호작용V화면 전환 (1인칭 ↔ 3인칭)마우스1인칭 시점에서 카메라 무빙
📈 개발 상태
✅ 필수 구현 완료

기본 캐릭터 이동 및 조작
핵심 아키텍처 구현
기본 상호작용 시스템

🎯 도전 기능 완료

추가 UI: 스태미나 UI 구현
3인칭 시점: V키로 1인칭/3인칭 전환 가능
1인칭 카메라: 마우스로 카메라 무빙 가능
상호작용 표시: 카메라로 오브젝트를 바라보면 프롬프트 표시

🔧 구현된 상호작용 오브젝트

문(Door): 기본 상호작용
트램펄린(Trampoline): 플레이어를 위로 튕겨줌
체력팩터(HealFactor): 체력 회복
스피드팩터(SpeedFactor): 일정 시간 속도 증가

🏗️ 핵심 기술
제네릭 클래스를 이용한 Core 구조
csharp// Spirit-Persona-Entity-Part (SPEP) 아키텍처
public abstract class BaseSprit<T, U> : MonoBehaviour 
    where T : Persona where U : BaseEntity

public abstract class BasePersona<U> : Persona 
    where U : BaseEntity

public abstract class BaseEntity : MonoBehaviour, IFunctionalSubscriber

public abstract class BasePart : MonoBehaviour, IEntityComponent
제네릭 클래스를 활용한 구현

Interactable<T>: 상호작용 오브젝트의 제네릭 기반 구조
PlayerSprit: 플레이어 입력 처리 및 애니메이션 제어
PlayerPersona: UI 연동 및 상호작용 관리
DUnityChan: 캐릭터 엔티티 (이동, 점프, 스태미나 관리)

🏗️ 아키텍처
Spirit-Persona-Entity-Part (SPEP) 아키텍처
프로젝트는 계층화된 아키텍처 패턴을 사용합니다:

Spirit: 게임 로직 및 이벤트 핸들링을 담당하는 컨트롤러
Persona: 뷰모델 및 프레젠터 역할
Entity: 순수한 데이터와 비즈니스 로직을 담당하는 모델
Part: Entity의 세부 기능을 담당하는 컴포넌트

반응형 시스템 (Reactive System)
csharp// Akasha 네임스페이스의 반응형 프레임워크 사용
RxVar<float> health = new RxVar<float>(100f);
RxBind.Bind(health, OnHealthChanged, this);

RxVar: 반응형 변수
RxExpr: 반응형 표현식
RxEvent: 반응형 이벤트
RxList: 반응형 리스트

🎯 주요 시스템
1. 캐릭터 시스템

DUnityChan: 캐릭터 엔티티 (이동, 점프, 스태미나 관리)
PlayerSprit: 입력 처리 및 애니메이션 제어
PlayerPersona: UI 연동 및 상호작용 관리

2. 상호작용 시스템

Interactable<T>: 상호작용 가능한 오브젝트의 제네릭 기본 클래스
InteractableItem: 아이템 상호작용 (포션 등)
InteractableStuff: 기능성 오브젝트 상호작용 (문, 트램펄린 등)

3. 카메라 시스템

ThirdPersonCamera: 3인칭 시점 카메라
1인칭 모드: 마우스 룩 기능 지원
V키 전환: 실시간 시점 변경

4. UI 시스템

UIManager: UI 창 관리 및 라이프사이클
StateIndicator: HP/스태미나 표시
Prompt: 상호작용 안내 메시지
DataSlot: 현재 보유 아이템 표시

5. 매니저 시스템

CharacterManager: 플레이어 캐릭터 관리
DataManager: 게임 데이터 관리 (몬스터, 아이템 등)
ResourceManager: 리소스 로딩 및 캐싱
PoolManager: 오브젝트 풀링

🔧 개발 환경
필요 조건

Unity: 2022.3.17f1
IDE: Visual Studio 2022
OS: Windows 10
Unity Input System Package
TextMeshPro

설치 방법

프로젝트를 Unity에서 열기
Package Manager에서 필요한 패키지 설치
Scene/Main 씬 로드
플레이 버튼 클릭

📁 프로젝트 구조
Assets/
├── Scripts/
│   ├── Framework/           # 아키텍처 프레임워크
│   │   ├── Base/           # 기본 클래스들 (BaseSprit, BasePersona 등)
│   │   ├── Reactive/       # 반응형 시스템 (RxVar, RxExpr 등)
│   │   └── Managers/       # 매니저 클래스들
│   ├── Character/          # 캐릭터 관련 (DUnityChan, PlayerSprit 등)
│   ├── Interaction/        # 상호작용 시스템 (Interactable<T> 등)
│   ├── UI/                # UI 시스템
│   └── Data/              # 데이터 클래스들
├── Prefabs/               # 프리팹들
├── Scenes/                # 씬 파일들
└── Resources/             # 리소스 파일들
🔍 주요 클래스 설명
제네릭 기반 상호작용 시스템
csharppublic abstract class Interactable<T> : Interactable where T : InteractableData
{
    public T data;
    protected virtual void ApplyPlayer(PlayerSprit player) { }
    protected virtual void OccupidPlayer(PlayerSprit player) { }
}
캐릭터 컨트롤러
csharppublic class PlayerSprit : BaseSprit<PlayerPersona, DUnityChan>
{
    // 입력 처리, 애니메이션, 물리 연동
}
상태 관리 엔티티
csharppublic class DUnityChan : BaseEntity
{
    // HP, 스태미나, 이동, 점프 로직
    public float CurHP { get; private set; }
    public float CurST { get; private set; }
}
🐛 디버깅
반응형 시스템 로깅
csharpRxLogger.Log("상태 변경 로그");
RxLogger.Dump(); // 로그 출력
상호작용 디버깅

Scene 뷰에서 Ray 디버깅 라인 확인 (초록색)
Console에서 상호작용 로그 확인

카메라 시스템 디버깅

ThirdPersonCamera의 CameraPositionIndex 확인
Ray 방향 및 Hit 정보 디버그 출력

🚀 확장 방법
새로운 상호작용 오브젝트 추가

InteractableStuff 또는 InteractableItem 상속
ApplyPlayer() 메서드 구현
ScriptableObject로 데이터 생성
예시:

csharppublic class SpeedFactor : InteractableStuff
{
    protected override void ApplyPlayer(PlayerSprit player)
    {
        player.Entity.StartCoroutine(player.Entity.IncreaseSpeed(10f, 5f));
    }
}
새로운 UI 창 추가

UIBase 상속 클래스 생성
UIManager.Show<T>() 사용
Resources 폴더에 프리팹 배치

새로운 매니저 추가

Manager<T> 상속
필요한 로직 구현
다른 매니저에서 Instance 접근

📈 성능 최적화

오브젝트 풀링: PoolManager 활용
리소스 캐싱: ResourceManager 사용
반응형 구독 관리: RxBind.UnbindAll() 호출
레이캐스팅 최적화: 상호작용 체크 주기 조절

🤝 기여 방법

이슈 등록
브랜치 생성 (feature/새기능)
커밋 (git commit -m '새기능 추가')
푸시 (git push origin feature/새기능)
Pull Request 생성

📝 라이선스
이 프로젝트는 MIT 라이선스 하에 있습니다.
👥 개발자 & 크레딧
개발자

허민영 - 메인 개발자
티스토리: 개발 블로그

크레딧

아키텍처 프레임워크: 소재철 튜터님
Unity-Chan: Unity Technologies Japan
반응형 시스템: Akasha Framework

 

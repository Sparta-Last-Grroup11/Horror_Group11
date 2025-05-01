# Horror_Group11
내일배움캠프 최종 팀프로젝트
<br><br>

## 침묵의 유산 (The Silent Inheritance)
> Unity 3D로 제작된 1인칭 호러 게임.<br>
> 어느 날, 할아버지에게서 낯선 폐병원을 유산으로 물려받은 주인공.<br>
> 병원을 둘러보던 중 정체불명의 존재와 마주하게 되며,<br>
> 끊긴 기억과 숨겨진 진실을 파헤치며 이곳에서 탈출해야 한다.<br>
- 개발 기간: 2025.04.04 ~ 2025.06.02<br>
- 장르: 화이트 게임류 호러, 탈출<br>
- 2종류의 퍼즐과 3종류의 몬스터를 통해 심리적 긴장감과 공포를 극대화
<br>

## 기술 스택
- Unity 2022.3.17f1
- C#
- Json
- Singleton Pattern
- Finite State Machine (FSM)
- NavMesh
- Sound Object Pooling
- Resource Manage
- Cinemachine
- Character Controller
- Input System (Invoke C# Events)
- Scriptable Object
<br>

## 기획 / 개발
| 이름      | 파트               |
|:---------:|:------------------:|
| 김영환     | 기획 리드 (게임 전체 흐름, 동선) |
| 박성준     | 개발 리드 (전체 시스템 구조, UI/데이터 관리, 스타트씬) |
| 나재현     | 플레이어, 몬스터(순찰형) FSM, 로비씬 |
| 김학영     | 몬스터(점프스케어, 웃는 천사형 AI) FSM |
| 정승제     | 오브젝트 상호작용, 퍼즐 로직 |
</details><br>

## 게임 영상
영환님께서 시연 영상 완성되면 이 곳에 삽입
(용량이 크면 유튜브 링크로 우회하거나, 프리뷰 영상 삽입)
<br><br>

## 조작
| 키 입력 | 동작    |
|:-----------:|:-------:|
| `W,S,A,D`       |  상,하,좌,우 이동   |
| `SHIFT` |  달리기 |
| `Left Control` | 앉기 |
| `E`       | 오브젝트 상호작용  |
| `F` |  손전등 ON / OFF |
| `R` | 손전등 배터리 사용(Reload) |
<br>

## 게임 플레이
- Unity 3D 기반 1인칭 시점
- 손전등과 조작키를 활용한 어두운 환경 탐색
- 병원 내 단서 수집 및 퍼즐 해결
- 몬스터에게서 도망치며 탈출 경로 확보
<br>

## 주요 기능
### 🕹️ 오브젝트 상호작용 시스템
1) 문 개폐: 조건 (특정 아이템 소유 등) 만족 및 상호작용 시 작동<br>
2) 전등 On/Off<br>
 - 발전기와 상호작용 후 2층의 전기 박스와 상호작용 시 전등 On<br>
 - 일정 시간마다 소등 및 특정 몬스터의 행동 시작<br>
3) 아이템 획득<br>
<br>

### 🧩 퍼즐 요소 2종 구현
1) 금고 여는 퍼즐<br>
 - A,D를 통해 조작<br>
 - 시계 방향으로 시작, 장치가 돌아가는 소리가 들린다면 반대 방향으로 조작<br>
 - 총 4회 암호를 맞추면 금고가 열림<br>
2) 의식 공간 퍼즐<br>
- 맵에 숨겨진 특정 아이템들을 모아 촛불을 밝히고 상호작용 시 완료
<br>

### 👹 몬스터 AI 3종
본 프로젝트의 몬스터들은 모두 FSM(Finite State Machine) 구조를 기반으로 설계되어, 상황에 따라 다양한 행동을 유기적으로 전환함<br>
NavMesh를 활용해 현실감 있는 이동 경로를 구현
1) 웃는 천사형:<br>
- 플레이어가 바라보는 동안 정지하고, 시야에서 벗어나면 플레이어를 추적. 불이 켜져있는 동안에는 발동하지 않음.<br>
- Idle → Chase → Attack 상태 순환<br>
<img src="https://github.com/user-attachments/assets/015d04bf-23cb-4d44-ae67-536b0fc1dac7" align="left" width="300"/>

<br clear="both"/><br>

2) 점프스케어형:
- 특정 조건에서 발동하여 갑자기 튀어나오는 공포 연출, 무해한 몬스터<br>
- Ambush(매복) 상태 기반, TriggerReceiver로 이벤트 감지
<img src="https://github.com/user-attachments/assets/42755460-2460-4910-b2e0-d85fc73aae77" align="left" width="300"/>

<br clear="both"/><br>

3) 순찰형:
- 일정 시간마다 랜덤 위치를 지정하여, 해당 위치로 이동하며 플레이어를 탐지<br>
- 플레이어를 탐지하면, 플레이어에게 달려감<br>
- 플레이어가 5초이상 감지 되지 않으면, 어그로가 풀림.<br>
<img src = "https://github.com/user-attachments/assets/3a7e9dd3-5d6c-470e-b3b7-ee0b4d05499a" align="left" width="300"/>

<br clear="both"/><br>
   
### 🖥️ UI 시스템
- 리소스 매니저를 이용한 프리팹 불러오기.<br>
- 딕셔너리를 이용하여 현재 활성화된 UI를 저장, 외부 스크립트에서 접근 가능하도록 한다.<br>
- UI에 3D 물체를 표현하는 것 또한 이쪽 매니저에서 관리.
<br>

### 🎬 특수 효과 및 연출
- 글리칭 효과 (화면의 왜곡) 쉐이더 그래프를 전체 화면 렌더링에 적용하여 쉐이더 그래프 값에 따라 글리칭 효과가 일어남
<br>

### 💾 데이터 관리
- JSON 컨버터
<br>


## 트러블 슈팅
오브젝트 상호작용
 문제 상황
- 전등 On/Off 시 몬스터가 정지/행동하는 기능을 구현하는 과정에서 동적 생성되는 몬스터에 접근할 방법이 없음

시도한 방법
- 몬스터 직접 참조 -> 동적 생성이라 연결 불가능

해결 방법
- ScriptableObject를 이용해 전원 상태를 저장
- 상태 변화 시. Event를 통해 몬스터에게 상태 변경을 통지
- 각 몬스터는 SO의 이벤트에 구독, 상태 변화 시 행동 상태를 변하도록 구현

결과
- 동적으로 생성되는 몬스터도 문제없이 전원 상태에 반응하도록 동작
<br>


## 향후 추가 예정
영환님(기획적인 추가), 성준님(개발적인 추가)께서 작성해주시면 되겠습니다.
- 캐릭터에게 다음 목표를 나타내는 유사 퀘스트 시스템
- 다양한 점프 스케어 오브젝트 ex) 갑자기 쓰러지는 물체, 움직이는 의자
- 다양한 퍼즐
<br>

## 라이선스
| 에셋 이름     |출처| 라이선스        |
|:-----------:|:---:|:-------------:|
| Nurse of Horror   |https://sketchfab.com/3d-models/nurse-of-horror-1ff4e40c27724291ae4d9cfd51c57239| CC |
| SkinLess Zombie   |https://assetstore.unity.com/packages/3d/characters/humanoids/skinless-zombie-226029| Standard |
| Horror Elements   |https://assetstore.unity.com/packages/audio/sound-fx/horror-elements-112021| Standard |
| Door, Cabinets & Lockers |https://assetstore.unity.com/packages/audio/sound-fx/foley/door-cabinets-lockers-free-257610 | Standard |
| Doors Small Sound Pack |https://assetstore.unity.com/packages/p/doors-small-sound-pack-262071 | Standard |
| Horror SFX - 082918 |https://assetstore.unity.com/packages/audio/sound-fx/horror-sfx-082918-127389 | Standard |
| Safe |https://free3d.com/3d-model/safe-58973.html | Personal Use License |
|horror mansion|
https://assetstore.unity.com/packages/3d/environments/horror-mansion-254104|Personal Use License |
|Abandoned Psychiatric Hospital|https://assetstore.unity.com/packages/3d/environments/urban/abandoned-psychiatric-hospital-188270|Personal Use License |
|Electric Substation Transformer Set|
https://assetstore.unity.com/packages/3d/props/industrial/electric-substation-transformer-set-316718|
Personal Use License |
|Starter Horror Items|
|https://assetstore.unity.com/packages/3d/props/tools/starter-horror-items-158511|
|Standard Unity Asset Store EULA|
|Xerography|FFC License|
|SpecialElite|FFC License|
|RomanUncialModern|OFL|
|Canterbury|FFC License|
|BLKCHCRY|OFL|
|Zombie Animations Set|https://assetstore.unity.com/packages/3d/animations/zombie-animations-set-220537|
각자 임포트한 건 각자가 추가. 

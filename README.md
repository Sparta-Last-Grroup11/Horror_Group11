   # Horror_Group11
내일배움캠프 최종 팀프로젝트
<br><br>

## 침묵의 유산 (The Silent Inheritance)
> Unity 3D로 제작된 1인칭 호러 게임.<br>
> 어느 날, 할아버지에게서 낯선 폐병원을 유산으로 물려받은 주인공.<br>
> 병원을 둘러보던 중 정체불명의 존재와 마주하게 되며,<br>
> 끊긴 기억과 숨겨진 진실을 파헤치며 이곳에서 탈출해야 한다.<br>
- 개발 기간: 2025.04.04 ~ 2025.06.02
- 장르: 화이트 게임류 호러, 탈출
- 2종류의 퍼즐과 3종류의 몬스터, 3종류의 엔딩 분기
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

## 팀원
| 이름   | 역할       | 담당 |
|:------:|:----------:|:--------------------------------------------:|
| 김영환 | 기획 리드  | 게임 흐름, 시나리오 |
| 박성준 | 개발 리드  | 시스템, UI/데이터 관리, 최적화, 현지화 |
| 나재현 | 개발       | 플레이어, 몬스터 FSM, 적AI, 현지화 |
| 김학영 | 개발       | 몬스터 FSM, 적AI, 이벤트 |
| 정승제 | 개발       | 오브젝트 상호작용, 퍼즐, 퀘스트 시스템, 최적화 |
</details><br>

## 게임 영상
[![게임 플레이 영상](https://img.youtube.com/vi/CBpSLINRp00/0.jpg)](https://youtu.be/CBpSLINRp00)

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
 - 발전기와 상호작용 후 2층의 전기 박스와 상호작용 시 전등 On
 - 일정 시간마다 소등 및 특정 몬스터의 행동 시작
3) 아이템 획득<br>
<br>

### 🧩 퍼즐 요소 구현
1) 금고 여는 퍼즐<br>
 - A,D를 통해 조작
 - 시계 방향으로 시작, 장치가 돌아가는 소리가 들린다면 반대 방향으로 조작
 - 총 4회 암호를 맞추면 금고가 열림
<img src="https://github.com/user-attachments/assets/502fd93c-b0fe-4752-9b59-a581ad3ab3ef" align="left" width="300"/>
 <br clear="both"/><br> 

2) 의식 공간 퍼즐<br>
- 맵에 숨겨진 특정 아이템들을 모아 촛불을 밝히고 상호작용 시 완료
 <img src="https://github.com/user-attachments/assets/7dc0c024-0155-45a3-94ca-79a975abb154" align="left" width="300"/>
 <br clear="both"/><br> 

### 👹 몬스터 AI 및 이벤트 시스템
- FSM(Finite State Machine) 구조로 몬스터를 Idle → Chase → Attack 상태로 전환 설계
- 옵저버 패턴을 활용하여 트리거 이벤트 → 사운드/연출/UI에 실시간 반응
- NavMesh를 통해 현실감 있는 몬스터 이동 구현
<br>
1) 웃는 천사형:<br>
- **행동 조건**: 플레이어가 바라볼 땐 정지, 시야 밖에서는 추적  
- **상태 전이**: Idle → Chase → Attack  
- **기타**: 전등이 켜져 있으면 비활성화됨  
<img src="https://github.com/user-attachments/assets/015d04bf-23cb-4d44-ae67-536b0fc1dac7" align="left" width="300"/>

<br clear="both"/><br>

2) 점프스케어형:
- **행동 조건**: 특정 조건에서만 등장, 피해 없음  
- **상태 전이**: Ambush 상태에서 Trigger 이벤트 발생 시 연출  
- **기타**: 옵저버 패턴 기반 이벤트 반응  
<img src="https://github.com/user-attachments/assets/42755460-2460-4910-b2e0-d85fc73aae77" align="left" width="300"/>

<br clear="both"/><br>

3) 순찰형:
- **행동 조건**: 일정 시간마다 랜덤 위치를 순찰, 시야 내 플레이어 탐지 시 추적  
- **상태 전이**: Patrol → Chase → Idle  
- **기타**: 탐지 종료 후 5초가 지나면 어그로 해제  
<img src = "https://github.com/user-attachments/assets/3a7e9dd3-5d6c-470e-b3b7-ee0b4d05499a" align="left" width="300"/>

<br clear="both"/><br>

### 🖥️ UI 시스템
- 리소스 매니저를 이용한 프리팹 불러오기
- 딕셔너리를 이용하여 현재 활성화된 UI를 저장, 외부 스크립트에서 접근 가능하도록 함
- UI에 3D 물체를 표현하는 것 또한 이쪽 매니저에서 관리
- json을 이용한 퀘스트 트랙
- 로컬라이징 시스템 도입 (fallback 폰트 활용)
<br>

### 🎬 특수 효과 및 연출
- 글리칭 효과 (화면의 왜곡): 쉐이더 그래프를 전체 화면 렌더링에 적용하여 쉐이더 그래프 값에 따라 글리칭 효과가 일어남
- 심장 박동 효과: 몬스터에게 추적당하는 상태가 되면, 심장박동 진폭 및 사운드 증가하는 시그널
- 컷씬 연출: Timeline을 활용해 추격씬 및 엔딩 컷씬 구현, DOTween 애니메이션 적용
<br>

### 💾 데이터 관리 및 세이브 기능
- JSON 컨버터
- 구글 스프레드시트와 연동된 리소스 매니저에서 LoadAsync로 필요한 리소스를 비동기 로딩
- 특정 위치에서 자동 저장되는 체크포인트 기능 구현, 컷씬 스킵과 함께 반복 플레이 피로도 감소 
<br>

### ⚙️ 최적화 전략
- 라이트맵 베이킹: 정적 조명 라이트맵을 적용해 조명 품질을 유지하면서도 평균 10~15 FPS 향상
- 오클루전 컬링: 시야 밖 오브젝트 렌더링 제거로 Draw Call 약 30~40% 감소
- 오디오 최적화: AudioClip 압축 및 사운드 오브젝트 풀링 적용 → 메모리 점유율 절반 이하로 감소
- 오브젝트 풀링: JumpScareZombie, 사운드 등 반복 생성 객체 풀링 처리로 GC 및 프레임 드랍 최소화
- Unity Profiler 기반 병목 분석: Profiler 및 Frame Debugger를 통한 FPS 저하 구간 진단 및 구조 개선
<br>

## 사용자 피드백 기반 개선사항
- 오브젝트 식별을 도와주는 아웃라인 적용
- 배터리 스폰량 증가 및 손전등 밝기 조절
- 튜토리얼 및 힌트 UI 추가
- 게임오버 시 반복 피로를 줄이기 위해 컷씬 스킵과 체크포인트 기능 구현
- 퀘스트 갱신 인지를 돕기 위해 알림 UI 및 효과 추가
<br>

## 트러블 슈팅 내용
### 로컬라이제이션 사용 관련
- 선택한 언어에 따라 적용되는 폰트를 변경하려 에셋 테이블을 만듬.
- 다만 에셋 테이블에서 폰트를 매개변수로 전달하는 과정에서 null 참조가 발생.
- 해결이 되지 않아 언어에 따른 폰트 보정은 폰트의 fallback을 이용해서 해결.
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
|horror mansion|https://assetstore.unity.com/packages/3d/environments/horror-mansion-254104|Personal Use License |
|Abandoned Psychiatric Hospital|https://assetstore.unity.com/packages/3d/environments/urban/abandoned-psychiatric-hospital-188270|Personal Use License |
|Electric Substation Transformer Set|https://assetstore.unity.com/packages/3d/props/industrial/electric-substation-transformer-set-316718|Personal Use License |
|Starter Horror Items|https://assetstore.unity.com/packages/3d/props/tools/starter-horror-items-158511|Standard Unity Asset Store EULA|
|Xerography||FFC License|
|SpecialElite||FFC License|
|RomanUncialModern||OFL|
|Canterbury||FFC License|
|BLKCHCRY||OFL|
|Zombie Animations Set|https://assetstore.unity.com/packages/3d/animations/zombie-animations-set-220537||

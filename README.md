# Horror_Group11
내일배움캠프 최종 팀프로젝트

## 침묵의 유산 (The Silent Inheritance)
> Unity 3D로 제작된 1인칭 호러 게임.<br>
> 어느 날, 할아버지에게서 낯선 폐병원을 유산으로 물려받은 주인공.<br>
> 병원을 둘러보던 중 정체불명의 존재와 마주하게 되며,<br>
> 끊긴 기억과 숨겨진 진실을 파헤치며 이곳에서 탈출해야 한다.<br>
> 두 종류의 퍼즐과 세 종류의 몬스터를 통해 심리적 긴장감과 공포를 극대화<br>
> 개발 기간: 2025.04.04 ~ 2025.06.02<br>
> 장르: 화이트 게임류 호러, 탈출<br>

## 기술 스택
- Unity 2022.3.17f1
- C#
- Json

## 개발
| 이름      | 파트               |
|:---------:|:------------------:|
| 김영환     | 기획 리드 |
| 박성준     | 개발 리드, 데이터 관리, UI |
| 나재현     | 플레이어, 몬스터(순찰형) |
| 김학영     | 몬스터(점프스케어, 웃는 천사 기믹) |
| 정승제     | 상호작용, 퍼즐 |
</details>

## 게임 영상
영환님께서 시연 영상 완성되면 이 곳에 삽입
(용량이 크면 유튜브 링크로 우회하거나, 프리뷰 영상 삽입)

## 조작
| 키 입력 | 동작    |
|:-----------:|:-------:|
| `W,S,A,D`       |  상,하,좌,우 이동   |
| `SHIFT` |  달리기 |
| `E`       | 오브젝트 상호작용  |
| `F` |  손전등 ON / OFF |

## 게임 플레이
- Unity 3D 기반 1인칭 시점
- 손전등과 조작키를 활용한 어두운 환경 탐색
- 병원 내 단서 수집 및 퍼즐 해결
- 몬스터에게서 도망치며 탈출 경로 확보

## 주요 기능
### 플레이어 상호작용 시스템
### 퍼즐 요소 2종 구현
- 금고 여는 퍼즐
- 의식 공간 퍼즐
### 몬스터 AI 3종
본 프로젝트의 몬스터들은 모두 FSM(Finite State Machine) 구조를 기반으로 설계되어, 상황에 따라 다양한 행동을 유기적으로 전환함
NavMesh를 활용해 현실감 있는 이동 경로를 구현
#### 공통
- FSM 구조기반 설계: EnemyStateMachine, EnemyBaseState, EnemyReceiver 등을 통해 상태 전이 관리
- 각 몬스터는 독립적인 StateMachine 폴더로 구성되어 유지보수 및 확장 용이
- Unity의 NavMesh를 통해 이동 범위 제어 및 경로 탐색 처리
1) 웃는 천사형:
   - 플레이어가 바라보는 동안 정지하고, 시야에서 벗어나면 플레이어를 추적. 불이 켜져있는 동안에는 발동하지 않음.
   - Idle → Chase → Attack 상태 순환
   ![image](https://github.com/user-attachments/assets/015d04bf-23cb-4d44-ae67-536b0fc1dac7)

2) 점프스케어형: 특정 조건에서 발동하여 갑자기 튀어나오는 공포 연출, 무해한 몬스터
   - Ambush(매복) 상태 기반, TriggerReceiver로 이벤트 감지
   ![image](https://github.com/user-attachments/assets/42755460-2460-4910-b2e0-d85fc73aae77)

3) 순찰형: 일정 경로를 따라 이동하며 플레이어를 탐지
   재현님이 채워주시면 되겠습니다.
### UI 시스템
### 특수 효과 및 연출
### 데이터 관리
- JSON 컨버터
- 세이브 및 로드 시스템

## 향후 추가 예정
영환님(기획적인 추가), 성준님(개발적인 추가)께서 작성해주시면 되겠습니다.

## 라이선스
| 에셋 이름     |출처| 라이선스        |
|:-----------:|:---:|:-------------:|
| Nurse of Horror   |https://sketchfab.com/3d-models/nurse-of-horror-1ff4e40c27724291ae4d9cfd51c57239| CC |
| SkinLess Zombie   |https://assetstore.unity.com/packages/3d/characters/humanoids/skinless-zombie-226029| Standard |
| Horror Elements   |https://assetstore.unity.com/packages/audio/sound-fx/horror-elements-112021| Standard |
각자 임포트한 건 각자가 추가. 

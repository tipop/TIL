# Summary of Quality Code

## 1. 뭐부터 하지?
*   [Happy Path](https://www.h2kinfosys.com/blog/happy-path-testing/) 부터 테스트하라.
*   Workaround path를 테스트하라.
    파일 저장 기능이라면, 네트워크 폴더에 파일을 저장하는 테스트가 통과하는가?
*   Error path를 테스트하라.
*   결함을 테스트하라.
*   다양한 데이터를 테스트하라.
*   경계 조건을 테스트하라.
     - 정상 입력의 끝부분에서 한 끗 차 에러 등의 문제를 탐지
     - 비정상 입력의 끝 부분에서 마찬가지로 한 끗 차 에러를 탐지한다.
     - 예상되는 비정상 입력 값을 사용하여 Security 등의 문제를 검증한다.      

## 2. 테스팅 원칙
* 테스트를 공들여 작성하라.
  `class AClassTest, testMethodA()`
* 제품 내에는 테스트 코드를 넣지 마라.
* 구현상의 의도를 검증하라. 코드가 만들어내려던 의도를 절대 놓쳐서는 안된다.
* 결함을 최소화하라.
* Minimal, fresh, 일회용 픽스처를 선호하라. 유일한 값을 사용할 수 있으므로.
* 사용 가능한 장치들을 이용하라.
* 불완전한 검증보다 완전한 검증을 택하라.
* 작은 테스트를 작성하라.
* 문제를 분리하라. unit test가 복잡해 지지 않게 하라.
* 간단하게 유지하라: 코드를 제거하라.
* 프레임워크를 테스트하지 마라.
* 자동 생성된 코드를 테스트하지 마라.
## 3. 기초
* [생성자 부트스트랩하기](CreatorBoostrap.java)
* getter(), setter() 테스트 하기
* [상수를 공유하라](ShareConstant.java)
* 로컬 범위에서 재정의하라.
* 일시적으로 교체하라.
* 캡슐화하고 오버라이딩하라.
* 노출도를 조정하라 (private -> protected)
* 주입에 의한 검증
* 이름 없는 데이터에 이름을 부여하라.
* [루프 조건을 캡슐화하라](InfiniteLoop.java)
* [에러 주입](ErrorInjection.java)
* [협업자를 대체시켜라](CollaboratorDouble.java)
## 4. 병렬 처리
* [공용 lock 으로 동기화하라](LockSynchronization.java)
* [감시 제어를 사용하라](RaceCondition.java)

## 5. Practice
* [WebRetriever](https://github.com/srvance/QualityCode14)

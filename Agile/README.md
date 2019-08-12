# Agile
### Agile과 순차적 개발 방법의 가장 큰 차이점은 이 그림으로 설명된다.

![](https://pbs.twimg.com/media/CoIEpFGXgAE27dK.png)

### 매 스프린트 마다 출시 가능한 제품 증분이 나오려면 만족해야 하는 조건
1) 모든 기능을 병렬로 개발하여 마지막에 integration 하여 출시하는 Big bang 방식이 아니라, 고객에게 가치있는 작은 기능 단위로 릴리즈 하는 것이다. 
 작은 기능 단위 개발에서는 매번 분석->설계->코딩->테스트->integration 단계를 거치며, 비지니스적인 결정에 따라 릴리즈하여 고객에게 제품 또는 서비스가 제공된다.
2) Release 는 몇 개의 스프린트를 묶어서 할 수 있다.
3) CI/CD에 unit test가 포함되어 퀄리티를 높인다.
4) TDD 개발 방법을 사용하면 처음부터 비지니스 코드가 테스트 가능한 이음매를 가질 수 있다.
4) Done 조건은 아래 경로가 모두 테스트되어야 한다. 
   white box test에서는 모두 테스트 되어야 하며, error path test를 QA팀에 미루면 안된다. Black box test에서는 error path와 workaround path의 모든 input을 만들어  수 없기 때문이다.
  - happy path
  - workaround path
  - error path

### 새로운 개발 패러다임
Agile 실천 방법을 실천하지 않으면 Agile로 개발한다고 할 수 없다. 
  - Defines detailed feature specification before development
  - TDD and unit test (happy / workaournd / error path)
  - CI/CD (Contineous Integration / Contineous Deployment)
  - Iterative Improvement Development
  

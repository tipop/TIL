# 1. Learning Modern Core Language Features
## AAA - Almost Always Auto
[가능하면 항상 auto를 써라.](Chapter1/AAA.cpp)

타입을 추론해야 하므로 auto에서는 초기값 없는 변수 선언이 불가능 하다.
auto를 사용하면 항상 정확한 타입을 사용하게 된다. 암시적 변환이 일어나지 않는다.

```C++
auto vv = std::vector<int>{ 1,2,3 };
int size1 = v.size();

auto size2 = v.size();
auto size3 = int{ v.size() };
```

auto 사용은 구현이 아니라 interface 지향 개발이 되기에 good object-oriented을 촉진한다.

그 말은 타이핑을 적게 하고 실제 타입이 뭔지 고려를 적게 해도 된다는 것이다.
```C++
std::map<int, std::string> m;

for (std::map<int, std::string>::const_iterator it = m.cbegin(); it != m.cend(); ++it)
{
}

for (auto it = m.cbegin(); it != m.cend(); ++it)
{
}
```

auto로 선언하는 것은 항상 오른쪽에 타입이 존재하는 일관성 있는 코딩 스타일을 제공한다.
```C++
int* pInt = new int(42);
auto pAuto = new int(42);
```

## Creating type aliases and alias templates
typedef 대신 using을 써라
```C++
using byte = unsigned char;
using pbyte = unsigned char *;
using array_t = int[10];
using fn = void(byte, double);

void func(byte b, double d) { /* ... */ }

byte b {42};
pbyte pb = new byte[10] {0};
array_t a{0,1,2,3,4,5,6,7,8,9};
fn* f = func;
```

## Understanding uniform initialization
#### Getting Ready
std::string s1("test);   // direct initialization
std::string s2 = "test"; // copy initialization

#### = 

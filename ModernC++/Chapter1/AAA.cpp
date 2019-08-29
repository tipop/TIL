#include "stdafx.h"
#include "Chaper1.h"
#include <iostream>
#include <vector>
#include <memory>


template <typename F, typename T>
auto Chaper1::apply(F&& f, T value)
{
	return f(value);
}

void Chaper1::usingAutoWheneverPossible()
{
	// AAA (Almost Always auto)
	auto i = 42;		// int
	auto d = 42.5;		// double
	auto s = "text";	// char const*
	auto v = { 1, 2, 3 }; // std::initializer_list<int>

	auto b = new char[10]{ 0 };				// char*
	auto s1 = std::string{ "text" };		// std::string
	auto v1 = std::vector<int>{ 1,2,3 };	// std::vector<int>
	auto p = std::make_shared<int>(42);		// std::shared_ptr<int>

	auto upper = [](char const c) { return toupper(c); };
	char c = upper('a');
	std::cout << c << std::endl;

	auto add = [](auto const a, auto const b) { return a + b; };
	std::cout << add(3, 4) << std::endl;

	char up = apply(upper, 'd');
	std::cout << up << std::endl;


	//auto vv = std::vector<int>{ 1,2,3 };
	//int size1 = v.size();

	//auto size2 = v.size();
	//auto size3 = int{ v.size() };

	int* pInt = new int(42);
	auto pAuto = new int(42);

	foo f(42);
	auto x = f.get();
	x = 100;
	std::cout << f.get() << std::endl;	// prints 42

	auto ladd = [](auto const a, auto const b) {return a + b; };

	std::cout << ladd(2, 3) << std::endl;
	
	//std::cout << ladd("forty", "two");
}

cmake_minimum_required (VERSION 3.14)

project(CorProfiler)

if(NOT WIN32)
    set(BASE_SOURCES )
    add_compile_options(-Wno-invalid-noreturn -Wno-pragma-pack -Wno-int-to-pointer-cast -fPIC -fms-extensions -DBIT64 -DPAL_STDCPP_COMPAT -DPLATFORM_UNIX -DHOST_64BIT -DHOST_AMD64 -std=c++17 -stdlib=libc++ -gdwarf-4)
    add_link_options(--no-undefined -pthread)

    include_directories(src/linux)
endif(NOT WIN32)

if (WIN32)
    set(BASE_SOURCES src/windows/CorProfiler.def)
    add_compile_options(/std:c++17)

    include_directories(src/windows)
endif(WIN32)

include_directories(src)
include_directories(src/sdk)

set(SOURCES
    ${BASE_SOURCES}
    src/guids.cpp
    src/ClassFactory.cpp
    src/CorProfiler.cpp
    src/dllmain.cpp)

add_library(CorProfiler SHARED ${SOURCES})

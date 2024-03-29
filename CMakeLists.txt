cmake_minimum_required (VERSION 3.14)

project(CorProfiler)

if(NOT WIN32)
    set(BASE_SOURCES )
    add_compile_options(-Wno-invalid-noreturn -Wno-pragma-pack -Wno-int-to-pointer-cast -fPIC -fms-extensions -DBIT64 -DPAL_STDCPP_COMPAT -DPLATFORM_UNIX -DHOST_64BIT -std=c++17)
    add_link_options(--no-undefined -pthread)

    include_directories($ENV{CORECLR_PATH}/pal/inc/rt $ENV{CORECLR_PATH}/pal/inc $ENV{CORECLR_PATH}/inc)
endif(NOT WIN32)

if (WIN32)
    set(BASE_SOURCES src/windows/CorProfiler.def)
    add_compile_options(/std:c++17)
endif(WIN32)
include_directories($CORECLR_BIN/inc $ENV{CORECLR_PATH}/pal/prebuilt/inc)

set(SOURCES
    ${BASE_SOURCES}
    src/ClassFactory.cpp
    src/CorProfiler.cpp
    src/dllmain.cpp
    $ENV{CORECLR_PATH}/pal/prebuilt/idl/corprof_i.cpp)

add_library(CorProfiler SHARED ${SOURCES})
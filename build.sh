#!/bin/bash

printf '  Building ...'

if [ ! -d "bin/" ]; then
    mkdir bin/
fi

pushd bin

export CC=/usr/bin/clang
export CXX=/usr/bin/clang++
cmake ../ -DCMAKE_BUILD_TYPE=Debug

make -j8

popd

printf '  Copying libCorProfiler.so to main directory\n'
cp bin/libCorProfiler.so .

printf 'Done.\n'

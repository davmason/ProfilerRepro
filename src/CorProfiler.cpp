// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#include "CorProfiler.h"
#include "corhlpr.h"
#include "CComPtr.h"
#include <string>
#include <sstream>
#include <assert.h>
#include <cstdlib>
#include <ctime>
#include <locale.h>

using std::shared_ptr;
using std::vector;
using std::mutex;
using std::lock_guard;
using std::map;
using std::thread;
using std::wstring;
using std::wcout;

CorProfiler::CorProfiler() :
    _pCorProfilerInfo(),
    _refCount(0)
{
    setlocale(LC_ALL, "");
}

CorProfiler::~CorProfiler()
{
    if (this->_pCorProfilerInfo != nullptr)
    {
        this->_pCorProfilerInfo->Release();
        this->_pCorProfilerInfo = nullptr;
    }
}

HRESULT STDMETHODCALLTYPE CorProfiler::Initialize(IUnknown *pICorProfilerInfoUnk)
{
    wcout << L"CorProfiler::Initialize" << std::endl;

    HRESULT hr = S_OK;
    if (FAILED(hr = pICorProfilerInfoUnk->QueryInterface(__uuidof(ICorProfilerInfo12), (void **)&_pCorProfilerInfo)))
    {
        wcout << L"FAIL: failed to QI for ICorProfilerInfo12." << std::endl;
        return hr;
    }

    if (FAILED(hr = _pCorProfilerInfo->SetEventMask2(COR_PRF_MONITOR_JIT_COMPILATION,
                                                       0)))
    {
        wcout << L"FAIL: ICorProfilerInfo::SetEventMask2() failed hr=0x" << std::hex << hr << std::endl;
        return hr;
    }
    
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CorProfiler::Shutdown()
{
    return S_OK;
}

HRESULT CorProfiler::JITCompilationStarted(FunctionID functionId, BOOL fIsSafeToBlock)
{
    String profName = GetFunctionIDName(functionId);
    wstring name = profName.ToWString();
    if (name.find(L"GenericMethod") != wstring::npos)
    {
        wcout << name << std::endl;
    }

    return S_OK;
}


String CorProfiler::GetFunctionIDName(FunctionID funcId)
{
    // If the FunctionID is 0, we could be dealing with a native function.
    if (funcId == 0)
    {
        return WCHAR("unknown");
    }

    String name;

    ClassID classId = NULL;
    ModuleID moduleId = NULL;
    mdToken token = NULL;
    ULONG32 nTypeArgs = NULL;
    ClassID typeArgs[SHORT_LENGTH];
    COR_PRF_FRAME_INFO frameInfo = NULL;

    HRESULT hr = S_OK;
    hr = _pCorProfilerInfo->GetFunctionInfo2(funcId,
                                            frameInfo,
                                            &classId,
                                            &moduleId,
                                            &token,
                                            SHORT_LENGTH,
                                            &nTypeArgs,
                                            typeArgs);
    if (FAILED(hr))
    {
        wcout << L"FAIL: GetFunctionInfo2 call failed with hr=0x" << std::hex << hr << std::endl;
        return WCHAR("FuncNameLookupFailed");
    }

    COMPtrHolder<IMetaDataImport2> pIMDImport;
    hr = _pCorProfilerInfo->GetModuleMetaData(moduleId,
                                             ofRead,
                                             IID_IMetaDataImport2,
                                             (IUnknown **)&pIMDImport);
    if (FAILED(hr))
    {
        wcout << L"FAIL: GetModuleMetaData call failed with hr=0x" << std::hex << hr << std::endl;
        return WCHAR("FuncNameLookupFailed");
    }

    WCHAR funcName[STRING_LENGTH];
    hr = pIMDImport->GetMethodProps(token,
                                    NULL,
                                    funcName,
                                    STRING_LENGTH,
                                    0,
                                    0,
                                    NULL,
                                    NULL,
                                    NULL,
                                    NULL);
    if (FAILED(hr))
    {
        wcout << L"FAIL: GetMethodProps call failed with hr=0x" << std::hex << hr << std::endl;
        return WCHAR("FuncNameLookupFailed");
    }

    name += funcName;

    // Fill in the type parameters of the generic method
    if (nTypeArgs > 0)
        name += WCHAR("<");

    for(ULONG32 i = 0; i < nTypeArgs; i++)
    {
        name += GetClassIDName(typeArgs[i]);

        if ((i + 1) != nTypeArgs)
            name += WCHAR(", ");
    }

    if (nTypeArgs > 0)
    {
        name += WCHAR(">");
    }

    HCORENUM hEnum = nullptr;
    const ULONG MaxGenericParametersCount = 128;
    ULONG genericParamsCount = MaxGenericParametersCount;
    mdGenericParam genericParams[MaxGenericParametersCount];
    hr = pIMDImport->EnumGenericParams(&hEnum, token, genericParams, MaxGenericParametersCount, &genericParamsCount);

    name += WCHAR(" EnumGenericParams<");
    if (hr == S_OK)
    {
        WCHAR paramName[64];
        ULONG paramNameLen = 64;

        for (size_t currentParam = 0; currentParam < genericParamsCount; currentParam++)
        {
            ULONG index;
            DWORD flags;
            hr = pIMDImport->GetGenericParamProps(genericParams[currentParam], &index, &flags, nullptr, nullptr, paramName, paramNameLen, &paramNameLen);
            if (SUCCEEDED(hr))
            {
                name += paramName;
            }
            else
            {
                // this should never happen if the enum succeeded: no need to count the parameters
                name += WCHAR("Failed");
            }

        }
        pIMDImport->CloseEnum(hEnum);
    }

    name += WCHAR(">");
    return name;
}

String CorProfiler::GetClassIDName(ClassID classId)
{
    ModuleID modId;
    mdTypeDef classToken;
    ClassID parentClassID;
    ULONG32 nTypeArgs;
    ClassID typeArgs[SHORT_LENGTH];
    HRESULT hr = S_OK;

    if (classId == NULL)
    {
        wcout << L"FAIL: Null ClassID passed in" << std::endl;
        return WCHAR("");
    }

    hr = _pCorProfilerInfo->GetClassIDInfo2(classId,
                                           &modId,
                                           &classToken,
                                           &parentClassID,
                                           SHORT_LENGTH,
                                           &nTypeArgs,
                                           typeArgs);
    if (CORPROF_E_CLASSID_IS_ARRAY == hr)
    {
        // We have a ClassID of an array.
        return WCHAR("ArrayClass");
    }
    else if (CORPROF_E_CLASSID_IS_COMPOSITE == hr)
    {
        // We have a composite class
        return WCHAR("CompositeClass");
    }
    else if (CORPROF_E_DATAINCOMPLETE == hr)
    {
        // type-loading is not yet complete. Cannot do anything about it.
        return WCHAR("DataIncomplete");
    }
    else if (FAILED(hr))
    {
        wcout << L"FAIL: GetClassIDInfo returned 0x" << std::hex << hr << L"for ClassID 0x" << std::hex << classId << std::endl;
        return WCHAR("GetClassIDNameFailed");
    }

    COMPtrHolder<IMetaDataImport> pMDImport;
    hr = _pCorProfilerInfo->GetModuleMetaData(modId,
                                             (ofRead | ofWrite),
                                             IID_IMetaDataImport,
                                             (IUnknown **)&pMDImport );
    if (FAILED(hr))
    {
        wcout << L"FAIL: GetModuleMetaData call failed with hr=0x" << std::hex << hr << std::endl;
        return WCHAR("ClassIDLookupFailed");
    }

    WCHAR wName[LONG_LENGTH];
    DWORD dwTypeDefFlags = 0;
    hr = pMDImport->GetTypeDefProps(classToken,
                                         wName,
                                         LONG_LENGTH,
                                         NULL,
                                         &dwTypeDefFlags,
                                         NULL);
    if (FAILED(hr))
    {
        wcout << L"FAIL: GetModuleMetaData call failed with hr=0x" << std::hex << hr << std::endl;
        return WCHAR("ClassIDLookupFailed");
    }

    String name = wName;
    if (nTypeArgs > 0)
        name += WCHAR("<");

    for(ULONG32 i = 0; i < nTypeArgs; i++)
    {

        String typeArgClassName;
        typeArgClassName.Clear();
        name += GetClassIDName(typeArgs[i]);

        if ((i + 1) != nTypeArgs)
            name += WCHAR(", ");
    }

    if (nTypeArgs > 0)
        name += WCHAR(">");

    return name;
}

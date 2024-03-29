// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#include "CorProfiler.h"
#include "corhlpr.h"
#include "CComPtr.h"
#include "profiler_pal.h"
#include <string>
#include <sstream>
#include <assert.h>
#include <cstdlib>
#include <ctime>

using std::shared_ptr;
using std::vector;
using std::mutex;
using std::lock_guard;
using std::map;
using std::thread;

CorProfiler::CorProfiler() :
    _pCorProfilerInfo12(),
    _refCount(0)
{

}

CorProfiler::~CorProfiler()
{
    if (this->_pCorProfilerInfo12 != nullptr)
    {
        this->_pCorProfilerInfo12->Release();
        this->_pCorProfilerInfo12 = nullptr;
    }
}

HRESULT STDMETHODCALLTYPE CorProfiler::Initialize(IUnknown *pICorProfilerInfoUnk)
{
    HRESULT hr = S_OK;
    if (FAILED(hr = pICorProfilerInfoUnk->QueryInterface(__uuidof(ICorProfilerInfo12), (void **)&_pCorProfilerInfo12)))
    {
        printf("FAIL: failed to QI for ICorProfilerInfo12.\n");
        return hr;
    }

    if (FAILED(hr = _pCorProfilerInfo12->SetEventMask2(COR_PRF_MONITOR_JIT_COMPILATION,
                                                       0)))
    {
        printf("FAIL: ICorProfilerInfo::SetEventMask2() failed hr=0x%x\n", hr);
        return hr;
    }
    
    return S_OK;
}

HRESULT STDMETHODCALLTYPE CorProfiler::Shutdown()
{
    return S_OK;
}

// EventPipeEventDelivered is how the profiler receives EventPipe events for a session. it is called synchronously 
// on the thread that generates the event. It will block the runtime's execution until it returns
// so be careful not to start any long running operations. This method can (and will) be called 
// concurrently from multiple threads.
HRESULT STDMETHODCALLTYPE CorProfiler::EventPipeEventDelivered(
    EVENTPIPE_PROVIDER provider,
    DWORD eventId,
    DWORD eventVersion,
    ULONG cbMetadataBlob,
    LPCBYTE metadataBlob,
    ULONG cbEventData,
    LPCBYTE eventData,
    LPCGUID pActivityId,
    LPCGUID pRelatedActivityId,
    ThreadID eventThread,
    ULONG numStackFrames,
    UINT_PTR stackFrames[])
{
    return S_OK;
}

// This method is called synchronously from the provider creator's thread. Just like
// EventPipeEventDelivered above, any long running operations will block the runtime from continuing.
// This method will be called before any events are fired from the provider.
HRESULT CorProfiler::EventPipeProviderCreated(EVENTPIPE_PROVIDER provider)
{
    return S_OK;
}


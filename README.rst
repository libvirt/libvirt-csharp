=======================
C# Libvirt API bindings
=======================

Description
-----------

The C# libvirt bindings are a class library. They use a Microsoft Visual Studio
project architecture, and have been tested with Windows .NET, and Mono, on both
Linux and Windows.

Compiling them produces **LibvirtBindings.dll**, which can be added as a .NET
reference to any .NET project needing access to libvirt.

Requirements
------------

These bindings depend upon the libvirt libraries being installed.

In the .NET case, this is **libvirt-0.dll**, produced from compiling libvirt for
windows.

Usage
-----

The libvirt C# bindings class library exposes the **Libvirt** namespace. This
namespace exposes all of the needed types (enum, struct), plus many classes
exposing the libvirt API methods.

These classes are grouped into functional areas, with each class exposing
libvirt methods related to that area.

| For example, the libvirt methods related to connections, such as
  **virConnectOpenAuth** and **virConnectNumOfDomains**, are in the **Connect**
  class.
| They are accessed as **Connect.OpenAuth**, and **Connect.NumOfDomains**
  respectively.

In the same manner, the other class name mappings are:

======================== =============
Name of libvirt function C# class name
======================== =============
virDomain...             Domain
virEvent...              Event
virInterface...          Interface
virNetwork...            Network
virNode...               Node
virSecret...             Secret
virStoragePool...        StoragePool
virStorageVolume...      StorageVolume
virStream...             Stream
======================== =============

There are some additions as well:

-  There is a class named **Library**, exposing the **virGetVersion** and
   **virInitialize** methods
-  There is a class named **Errors**, exposing the error related methods. For
   example, **virSetErrorFunc** and **virConnResetLastError**.

Authors
-------

The C# bindings are the work of Arnaud Champion <`arnaud.champion AT
devatom.fr <mailto:arnaud.champion%20AT%20devatom.fr>`__>, based upon the
previous work of Jaromír Červenka.

Test Configuration
------------------

Testing is performed using the following configurations:

-  Windows 7 (64 bits) / .Net 4
-  Windows 7 (64 bits) / Mono 2.6.7 (compiled in 32 bits)
-  Ubuntu 10.10 amd64 / Mono 2.6.7 (compiled in 64 bits)

Type Coverage
-------------

Coverage of the libvirt types is:

======== ======================================= ========= ======== ======================================== ======= ========== =================== ===================
\                                                                                                                                    Tested
-------- --------------------------------------- --------- -------- ---------------------------------------- ------- --------------------------------------------------
Type     Name                                    Binding   Tested   Example project                          Works   .Net/Win   Mono (32-bit)/Win   Mono (64-bit)/Linux
======== ======================================= ========= ======== ======================================== ======= ========== =================== ===================
enum     virCPUCompareResult                     No
struct   virConnect                              IntPtr    Yes                                               Yes
struct   virConnectAuth                          Yes       Yes      virConnectOpenAuth                       Yes     Yes        Yes                 Yes
struct   virConnectCredential                    Yes       Yes      virConnectOpenAuth                       Yes     Yes        Yes                 Yes
enum     virConnectCredentialType                Yes       Yes      virConnectOpenAuth                       Yes     Yes        Yes                 Yes
enum     virConnectFlags                         No
struct   virDomain                               IntPtr
struct   virDomainBlockInfo                      No
struct   virDomainBlockStatsInfo                 Yes       Yes      virDomainStats                           Yes     Yes        Yes                 Yes
enum     virDomainCoreDumpFlags                  No
enum     virDomainCreateFlags                    No
enum     virDomainDeviceModifyFlags              No
enum     virDomainEventDefinedDetailType         Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
struct   virDomainEventGraphicsAddress           No
enum     virDomainEventGraphicsAddressType       No
enum     virDomainEventGraphicsPhase             No
struct   virDomainEventGraphicsSubject           No
struct   virDomainEventGraphicsSubjectIdentity   No
enum     virDomainEventID                        No
enum     virDomainEventIOErrorAction             No
enum     virDomainEventResumedDetailType         Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
enum     virDomainEventStartedDetailType         Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
enum     virDomainEventStoppedDetailType         Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
enum     virDomainEventSuspendedDetailType       Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
enum     virDomainEventType                      Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
enum     virDomainEventUndefinedDetailType       Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
enum     virDomainEventWatchdogAction            No
struct   virDomainInfo                           Yes       Yes      virConnectSetErrorFunc, virDomainStats   Yes     Yes        Yes                 Yes
struct   virDomainInterfaceStatsStruct           Yes       Yes      virDomainStats                           Yes     Yes        Yes                 Yes
struct   virDomainJobInfo                        No
enum     virDomainJobType                        No
enum     virDomainMemoryFlags                    No
struct   virDomainMemoryStatStruct               No
enum     virDomainMemoryStatTags                 Yes       No                                                Maybe
enum     virDomainMigrateFlags                   No
struct   virDomainSnapshot                       No
enum     virDomainSnapshotDeleteFlags
enum     virDomainState                          Yes       Yes                                               Yes
enum     virDomainXMLFlags                       Yes       Yes                                               Yes
enum     virEventHandleType                      Yes       Yes      virEventRegisterImpl                     Yes     Yes        Yes                 Yes
struct   virInterface                            IntPtr
enum     virInterfaceXMLFlags                    No
struct   virNWFilter                             No
struct   virNetwork                              IntPtr
struct   virNodeDevice                           IntPtr
struct   virNodeInfo                             Yes       No                                                Maybe
struct   virSchedParameter                       No
enum     virSchedParameterType                   No
struct   virSecret                               No
enum     virSecretUsageType                      No
struct   virSecurityLabel                        No
struct   virSecurityModel                        No
enum     virStoragePoolBuildFlags                Yes       No                                                Maybe
enum     virStoragePoolDeleteFlags               Yes       No                                                Maybe
struct   virStoragePoolInfo                      Yes       Yes                                               Yes
struct   virStoragePool                          IntPtr
enum     virStoragePoolState                     Yes       Yes                                               Yes
struct   virStorageVol                           IntPtr
enum     virStorageVolDeleteFlags                No
struct   virStorageVolInfo                       Yes       Yes                                               Yes
enum     virStorageVolType                       Yes       Yes                                               Yes
struct   virStream                               No
enum     virStreamEventType                      No
enum     virStreamFlags                          No
struct   virVcpuInfo                             No
enum     virVcpuState                            No
struct   virError                                Yes       Yes      virConnectSetErrorFunc, virDomainStats   Yes     Yes        Yes                 Yes
======== ======================================= ========= ======== ======================================== ======= ========== =================== ===================

**Note:** ``IntPtr`` in the above table means that the struct is not public
so it's exposed as an ``IntPtr`` type.

Function Coverage
-----------------

Coverage of the libvirt functions is:

============================================ ======= ======== ====== ====================================== ======= ======== ========== ============
\                                                                                                                            Tested
-------------------------------------------- ------- -------- ------ -------------------------------------- ------- --------------------------------
Name                                         Binding Type     Tested Example project                        Working .Net/Win Mono32/Win Mono64/Linux
============================================ ======= ======== ====== ====================================== ======= ======== ========== ============
virConnectAuthCallback                       Yes     delegate Yes    virConnectOpenAuth                     Yes     Yes      Yes        Yes
virConnectBaselineCPU                        No      function
virConnectClose                              Yes     function Yes    virConnectOpenAuth                     Yes     Yes      Yes        Yes
virConnectCompareCPU                         No      function
virConnectDomainEventCallback                Yes     delegate Yes                                           Yes
virConnectDomainEventDeregister              No      function
virConnectDomainEventDeregisterAny           No      function
virConnectDomainEventGenericCallback         No      delegate
virConnectDomainEventGraphicsCallback        No      delegate
virConnectDomainEventIOErrorCallback         No      delegate
virConnectDomainEventIOErrorReasonCallback   No      delegate
virConnectDomainEventRTCChangeCallback       No      delegate
virConnectDomainEventRegister                Yes     function Yes    virEventRegisterImpl                   Yes     Yes      Yes        Yes
virConnectDomainEventRegisterAny             No      function
virConnectDomainEventWatchdogCallback        No      delegate
virConnectDomainXMLFromNative                No      function
virConnectDomainXMLToNative                  No      function
virConnectFindStoragePoolSources             No      function
virConnectGetCapabilities                    Yes     function Yes                                           Yes
virConnectGetHostname                        Yes     function Yes
virConnectGetLibVersion                      Yes     function No                                            Maybe
virConnectGetMaxVcpus                        Yes     function No                                            Maybe
virConnectGetType                            Yes     function No                                            Maybe
virConnectGetURI                             Yes     function Yes                                           Yes
virConnectGetVersion                         Yes     function No                                            Maybe
virConnectIsEncrypted                        Yes     function No                                            Maybe
virConnectIsSecure                           Yes     function No                                            Maybe
virConnectListDefinedDomains                 Yes     function Yes    virConnectOpenAuth                     Yes     Yes      Yes        Yes
virConnectListDefinedInterfaces              Yes     function Yes                                           Yes
virConnectListDefinedNetworks                Yes     function Yes                                           Yes
virConnectListDefinedStoragePools            Yes     function Yes                                           Yes
virConnectListDomains                        Yes     function Yes    virConnectOpenAuth, virDomainInfos     Yes     Yes      Yes        Yes
virConnectListInterfaces                     Yes     function Yes                                           Yes
virConnectListNWFilters                      No      function
virConnectListNetworks                       Yes     function Yes                                           Yes
virConnectListSecrets                        Yes     function No                                            Maybe
virConnectListStoragePools                   Yes     function Yes    virConnectOpen                         Yes     Yes      Yes        Yes
virConnectNumOfDefinedDomains                Yes     function Yes    virConnectOpenAuth                     Yes     Yes      Yes        Yes
virConnectNumOfDefinedInterfaces             Yes     function No                                            Maybe
virConnectNumOfDefinedNetworks               Yes     function Yes                                           Yes
virConnectNumOfDefinedStoragePools           Yes     function Yes                                           Yes
virConnectNumOfDomains                       Yes     function Yes    virConnectOpenAuth, virDomainInfos     Yes     Yes      Yes        Yes
virConnectNumOfInterfaces                    Yes     function No                                            Maybe
virConnectNumOfNWFilters                     No      function
virConnectNumOfNetworks                      Yes     function Yes                                           Yes
virConnectNumOfSecrets                       Yes     function No                                            Maybe
virConnectNumOfStoragePools                  Yes     function Yes    virConnectOpen                         Yes     Yes      Yes        Yes
virConnectOpen                               Yes     function Yes    virEventRegisterImpl, virDomainInfos   Yes     Yes      Yes        Yes
virConnectOpenAuth                           Yes     function Yes    virConnectOpenAuth                     Yes     Yes      Yes        Yes
virConnectOpenReadOnly                       Yes     function No                                            Maybe
virConnectRef                                Yes     function No                                            Maybe
virDomainAbortJob                            No      function
virDomainAttachDevice                        Yes     function No                                            Maybe
virDomainAttachDeviceFlags                   Yes     function No                                            Maybe
virDomainBlockPeek                           No      function
virDomainBlockStats                          Yes     function Yes    virDomainInfos                         Yes     Yes      Yes        Yes
virDomainCoreDump                            Yes     function No                                            Maybe
virDomainCreate                              Yes     function Yes                                           Yes
virDomainCreateLinux                         No      function
virDomainCreateWithFlags                     No      function
virDomainCreateXML                           Yes     function No                                            Maybe
virDomainDefineXML                           Yes     function Yes                                           Yes
virDomainDestroy                             Yes     function Yes                                           Yes
virDomainDetachDevice                        Yes     function No                                            Maybe
virDomainDetachDeviceFlags                   Yes     function No                                            Maybe
virDomainFree                                Yes     function Yes                                           Yes
virDomainGetAutostart                        Yes     function No                                            Maybe
virDomainGetBlockInfo                        No      function
virDomainGetConnect                          Yes     function No                                            Maybe
virDomainGetID                               Yes     function No                                            Maybe
virDomainGetInfo                             Yes     function Yes    virDomainInfos                         Yes     Yes      Yes        Yes
virDomainGetJobInfo                          No      function
virDomainGetMaxMemory                        Yes     function No                                            Maybe
virDomainGetMaxVcpus                         Yes     function No                                            Maybe
virDomainGetName                             Yes     function Yes    virConnectOpenAuth, virDomainInfos     Yes     Yes      Yes        Yes
virDomainGetOSType                           Yes     function No                                            Maybe
virDomainGetSchedulerParameters              No      function
virDomainGetSchedulerType                    No      function
virDomainGetSecurityLabel                    No      function
virDomainGetUUID                             Yes     function No                                            Maybe
virDomainGetUUIDString                       Yes     function No                                            Maybe
virDomainGetVcpus                            No      function
virDomainGetXMLDesc                          Yes     function Yes    virDomainInfos                         Yes     Yes      Yes        Yes
virDomainHasCurrentSnapshot                  No      function
virDomainHasManagedSaveImage                 No      function
virDomainInterfaceStats                      No      function Yes    virDomainInfos                         Yes     Yes      Yes        Yes
virDomainIsActive                            Yes     function Yes                                           Yes
virDomainIsPersistent                        Yes     function No                                            Maybe
virDomainLookupByID                          Yes     function Yes    virConnectOpenAuth, virDomainInfos     Yes     Yes      Yes        Yes
virDomainLookupByName                        Yes     function Yes    virDomainInfos                         Yes     Yes      Yes        Yes
virDomainLookupByUUID                        Yes     function No                                            Maybe
virDomainLookupByUUIDString                  Yes     function No                                            Maybe
virDomainManagedSave                         No      function
virDomainManagedSaveRemove                   No      function
virDomainMemoryPeek                          No      function
virDomainMemoryStats                         No      function
virDomainMigrate                             No      function
virDomainMigrateSetMaxDowntime               No      function
virDomainMigrateToURI                        No      function
virDomainPinVcpu                             No      function
virDomainReboot                              Yes     function Yes                                           Yes
virDomainRef                                 Yes     function No                                            Maybe
virDomainRestore                             Yes     function No                                            Maybe
virDomainResume                              Yes     function Yes                                           Yes
virDomainRevertToSnapshot                    No      function
virDomainSave                                Yes     function No                                            Maybe
virDomainSetAutostart                        Yes     function No                                            Maybe
virDomainSetMaxMemory                        Yes     function No                                            Maybe
virDomainSetMemory                           Yes     function No                                            Maybe
virDomainSetSchedulerParameters              No      function
virDomainSetVcpus                            Yes     function No                                            Maybe
virDomainShutdown                            Yes     function Yes                                           Yes
virDomainSnapshotCreateXML                   No      function
virDomainSnapshotCurrent                     No      function
virDomainSnapshotDelete                      No      function
virDomainSnapshotFree                        No      function
virDomainSnapshotGetXMLDesc                  No      function
virDomainSnapshotListNames                   No      function
virDomainSnapshotLookupByName                No      function
virDomainSnapshotNum                         No      function
virDomainSuspend                             Yes     function Yes                                           Yes
virDomainUndefine                            Yes     function Yes                                           Yes
virDomainUpdateDeviceFlags                   No      function
virEventAddHandleFunc                        Yes     delegate Yes                                           Yes
virEventAddTimeoutFunc                       Yes     delegate Yes                                           Yes
virEventHandleCallback                       Yes     delegate Yes    virEventRegisterImpl                   Yes     Yes      Yes        Yes
virEventRegisterImpl                         Yes     function Yes    virEventRegisterImpl                   Yes     Yes      Yes        Yes
virEventRemoveHandleFunc                     Yes     delegate Yes                                           Yes
virEventRemoveTimeoutFunc                    Yes     delegate Yes                                           Yes
virEventTimeoutCallback                      Yes     delegate Yes    virEventRegisterImpl                   Yes     Yes      Yes        Yes
virEventUpdateHandleFunc                     Yes     delegate Yes                                           Yes
virEventUpdateTimeoutFunc                    Yes     delegate Yes                                           Yes
virFreeCallback                              Yes     function Yes    virEventRegisterImpl                   Yes     Yes      Yes        Yes
virGetVersion                                Yes     function Yes                                           Yes
virInitialize                                Yes     function Yes                                           Yes
virInterfaceCreate                           No      function
virInterfaceDefineXML                        No      function
virInterfaceDestroy                          No      function
virInterfaceFree                             No      function
virInterfaceGetConnect                       No      function
virInterfaceGetMACString                     No      function
virInterfaceGetName                          No      function
virInterfaceGetXMLDesc                       No      function
virInterfaceIsActive                         No      function
virInterfaceLookupByMACString                No      function
virInterfaceLookupByName                     No      function
virInterfaceRef                              No      function
virInterfaceUndefine                         No      function
virNWFilterDefineXML                         No      function
virNWFilterFree                              No      function
virNWFilterGetName                           No      function
virNWFilterGetUUID                           No      function
virNWFilterGetUUIDString                     No      function
virNWFilterGetXMLDesc                        No      function
virNWFilterLookupByName                      No      function
virNWFilterLookupByUUID                      No      function
virNWFilterLookupByUUIDString                No      function
virNWFilterRef                               No      function
virNWFilterUndefine                          No      function
virNetworkCreate                             Yes     function Yes                                           Yes
virNetworkCreateXML                          Yes     function No                                            Maybe
virNetworkDefineXML                          Yes     function Yes                                           Yes
virNetworkDestroy                            Yes     function Yes                                           Yes
virNetworkFree                               Yes     function Yes                                           Yes
virNetworkGetAutostart                       Yes     function No                                            Maybe
virNetworkGetBridgeName                      Yes     function No                                            Maybe
virNetworkGetConnect                         Yes     function No                                            Maybe
virNetworkGetName                            Yes     function No                                            Maybe
virNetworkGetUUID                            No      function
virNetworkGetUUIDString                      Yes     function Yes                                           Yes
virNetworkGetXMLDesc                         Yes     function Yes                                           Yes
virNetworkIsActive                           Yes     function Yes                                           Yes
virNetworkIsPersistent                       Yes     function Yes                                           Yes
virNetworkLookupByName                       Yes     function Yes                                           Yes
virNetworkLookupByUUID                       Yes     function No                                            Maybe
virNetworkLookupByUUIDString                 Yes     function No                                            Maybe
virNetworkRef                                Yes     function No                                            Maybe
virNetworkSetAutostart                       Yes     function Yes                                           Yes
virNetworkUndefine                           Yes     function Yes                                           Yes
virNodeDeviceCreateXML                       No      function
virNodeDeviceDestroy                         No      function
virNodeDeviceDettach                         No      function
virNodeDeviceFree                            No      function
virNodeDeviceGetName                         No      function
virNodeDeviceGetParent                       No      function
virNodeDeviceGetXMLDesc                      Yes     function Yes                                           Yes
virNodeDeviceListCaps                        No      function
virNodeDeviceLookupByName                    Yes     function Yes                                           Yes
virNodeDeviceNumOfCaps                       No      function
virNodeDeviceReAttach                        No      function
virNodeDeviceRef                             No      function
virNodeDeviceReset                           No      function
virNodeGetCellsFreeMemory                    No      function
virNodeGetFreeMemory                         Yes     function No                                            Maybe
virNodeGetInfo                               Yes     function No                                            Maybe
virNodeGetSecurityModel                      No      function
virNodeListDevices                           Yes     function Yes                                           Yes
virNodeNumOfDevices                          Yes     function Yes                                           Yes
virSecretDefineXML                           No      function
virSecretFree                                No      function
virSecretGetConnect                          No      function
virSecretGetUUID                             No      function
virSecretGetUUIDString                       No      function
virSecretGetUsageID                          No      function
virSecretGetUsageType                        No      function
virSecretGetValue                            No      function
virSecretGetXMLDesc                          No      function
virSecretLookupByUUID                        No      function
virSecretLookupByUUIDString                  No      function
virSecretLookupByUsage                       No      function
virSecretRef                                 No      function
virSecretSetValue                            No      function
virSecretUndefine                            No      function
virStoragePoolBuild                          Yes     function No                                            Maybe
virStoragePoolCreate                         Yes     function Yes                                           Yes
virStoragePoolCreateXML                      Yes     function No                                            Maybe
virStoragePoolDefineXML                      Yes     function Yes                                           Yes
virStoragePoolDelete                         Yes     function No                                            Maybe
virStoragePoolDestroy                        Yes     function Yes                                           Yes
virStoragePoolFree                           Yes     function Yes                                           Yes
virStoragePoolGetAutostart                   Yes     function No                                            Maybe
virStoragePoolGetConnect                     Yes     function No                                            Maybe
virStoragePoolGetInfo                        Yes     function Yes                                           Yes
virStoragePoolGetName                        Yes     function Yes                                           Yes
virStoragePoolGetUUID                        Yes     function No                                            Maybe
virStoragePoolGetUUIDString                  Yes     function Yes                                           Yes
virStoragePoolGetXMLDesc                     Yes     function Yes                                           Yes
virStoragePoolIsActive                       Yes     function Yes                                           Yes
virStoragePoolIsPersistent                   Yes     function Yes                                           Yes
virStoragePoolListVolumes                    Yes     function Yes                                           Yes
virStoragePoolLookupByName                   Yes     function Yes                                           Yes
virStoragePoolLookupByUUID                   Yes     function No                                            Maybe
virStoragePoolLookupByUUIDString             Yes     function No                                            Maybe
virStoragePoolLookupByVolume                 Yes     function No                                            Maybe
virStoragePoolNumOfVolumes                   Yes     function Yes                                           Yes
virStoragePoolRef                            Yes     function No                                            Maybe
virStoragePoolRefresh                        Yes     function No                                            Maybe
virStoragePoolSetAutostart                   Yes     function Yes                                           Yes
virStoragePoolUndefine                       Yes     function Yes                                           Yes
virStorageVolCreateXML                       Yes     function Yes                                           Yes
virStorageVolCreateXMLFrom                   Yes     function No                                            Maybe
virStorageVolDelete                          Yes     function Yes                                           Yes
virStorageVolFree                            Yes     function No                                            Maybe
virStorageVolGetConnect                      Yes     function No                                            Maybe
virStorageVolGetInfo                         Yes     function Yes                                           Yes
virStorageVolGetKey                          Yes     function Yes                                           Yes
virStorageVolGetName                         Yes     function Yes                                           Yes
virStorageVolGetPath                         Yes     function Yes                                           Yes
virStorageVolGetXMLDesc                      Yes     function Yes                                           Yes
virStorageVolLookupByKey                     Yes     function Yes                                           Yes
virStorageVolLookupByName                    Yes     function Yes                                           Yes
virStorageVolLookupByPath                    Yes     function Yes                                           Yes
virStorageVolRef                             Yes     function No                                            No
virStorageVolWipe                            No      function
virStreamAbort                               No      function
virStreamEventAddCallback                    No      function
virStreamEventCallback                       No      delegate
virStreamEventRemoveCallback                 No      function
virStreamEventUpdateCallback                 No      function
virStreamFinish                              No      function
virStreamFree                                No      function
virStreamNew                                 No      function
virStreamRecv                                No      function
virStreamRecvAll                             No      function
virStreamRef                                 No      function
virStreamSend                                No      function
virStreamSendAll                             No      function
virStreamSinkFunc                            No      delegate
virStreamSourceFunc                          No      delegate
virGetLastError                              Yes     function Yes    virConnectSetErrorFunc                 Yes     Yes      Yes        Yes
virConnSetErrorFunc                          Yes     function Yes    virConnectSetErrorFunc                 Yes     Yes      Yes        Yes
virErrorFunc                                 Yes     delegate Yes    virConnectSetErrorFunc, virDomainInfos Yes     Yes      Yes        Yes
============================================ ======= ======== ====== ====================================== ======= ======== ========== ============

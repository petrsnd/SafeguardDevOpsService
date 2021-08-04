﻿using System.Collections.Generic;
using OneIdentity.DevOps.Data;
using OneIdentity.DevOps.Data.Spp;
using OneIdentity.SafeguardDotNet;
using A2ARetrievableAccount = OneIdentity.DevOps.Data.Spp.A2ARetrievableAccount;
#pragma warning disable 1591

namespace OneIdentity.DevOps.Logic
{
    public interface ISafeguardLogic
    {
        DevOpsSecretsBroker DevOpsSecretsBrokerCache { get; }

        ISafeguardConnection Connect();
        ISafeguardConnection CertConnect();
        SafeguardDevOpsConnection GetAnonymousSafeguardConnection();
        SafeguardDevOpsConnection GetSafeguardConnection();
        SafeguardDevOpsLogon GetSafeguardLogon();
        SafeguardDevOpsConnection SetSafeguardData(string token, SafeguardData safeguardData);
        void DeleteSafeguardData();

        bool IsLoggedIn();
        bool ValidateLogin(string token, bool tokenOnly = false);
        bool PauseBackgroundMaintenance { get; set; }

        CertificateInfo GetCertificateInfo(CertificateType certificateType);
        void InstallCertificate(CertificateInfo certificatePfx, CertificateType certificateType);
        void RemoveClientCertificate();
        void RemoveWebServerCertificate();
        string GetCSR(int? size, string subjectName, string sanDns, string sanIp, CertificateType certificateType);

        object GetAvailableAccounts(ISafeguardConnection sgConnection, string filter, int? page, bool? count, int? limit, string orderby, string q);
        AssetAccount GetAccount(ISafeguardConnection sgConnection, int id);

        object GetAvailableA2ARegistrations(ISafeguardConnection sgConnection, string filter, int? page, bool? count, int? limit, string @orderby, string q);
        A2ARegistration GetA2ARegistration(ISafeguardConnection sgConnection, A2ARegistrationType registrationType);
        A2ARegistration SetA2ARegistration(ISafeguardConnection sgConnection, IMonitoringLogic monitoringLogic, IPluginsLogic pluginsLogic, IAddonLogic addonLogic, int id);
        A2ARetrievableAccount GetA2ARetrievableAccount(ISafeguardConnection sgConnection, int id, A2ARegistrationType registrationType);
        void DeleteA2ARetrievableAccount(ISafeguardConnection sgConnection, int id, A2ARegistrationType registrationType);
        IEnumerable<A2ARetrievableAccount> GetA2ARetrievableAccounts(ISafeguardConnection sgConnection, A2ARegistrationType registrationType);
        A2ARetrievableAccount GetA2ARetrievableAccountById(ISafeguardConnection sgConnection, A2ARegistrationType registrationType, int accountId);
        IEnumerable<A2ARetrievableAccount> AddA2ARetrievableAccounts(ISafeguardConnection sgConnection, IEnumerable<SppAccount> accounts, A2ARegistrationType registrationType);
        void RemoveA2ARetrievableAccounts(ISafeguardConnection sgConnection, IEnumerable<A2ARetrievableAccount> accounts, A2ARegistrationType registrationType);

        void RetrieveDevOpsSecretsBrokerInstance(ISafeguardConnection sgConnection);
        void AddSecretsBrokerInstance(ISafeguardConnection sgConnection);
        void CheckAndSyncSecretsBrokerInstance(ISafeguardConnection sgConnection);
        void CheckAndPushAddOnCredentials(ISafeguardConnection sgConnection);
        void CheckAndConfigureAddonPlugins(ISafeguardConnection sgConnection, IPluginsLogic pluginsLogic);
        void CheckAndSyncVaultCredentials(ISafeguardConnection sgConnection);

        Asset GetAsset(ISafeguardConnection sgConnection);
        AssetPartition GetAssetPartition(ISafeguardConnection sgConnection);

        ServiceConfiguration GetDevOpsConfiguration(ISafeguardConnection sgConnection);
        ServiceConfiguration ConfigureDevOpsService();
        void DeleteDevOpsConfiguration(ISafeguardConnection sgConnection, IAddonLogic addonLogic, bool secretsBrokerOnly);

        void RestartService();

        IEnumerable<CertificateInfo> GetTrustedCertificates();
        CertificateInfo GetTrustedCertificate(string thumbPrint);
        CertificateInfo AddTrustedCertificate(CertificateInfo certificate);
        void DeleteTrustedCertificate(string thumbPrint);
        IEnumerable<CertificateInfo> ImportTrustedCertificates(ISafeguardConnection sgConnection);
        void DeleteAllTrustedCertificates();
        void PingSpp(ISafeguardConnection sgConnection);
    }
}

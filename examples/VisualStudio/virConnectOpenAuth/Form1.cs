/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 * 
 * Sample code for :
 * Function :
 *      virConnectOpenAuth
 *      virConnectNumOfDefinedDomains
 *      virConnectListDefinedDomains
 *      virConnectNumOfDomains
 *      virDomainLookupByID
 *      virConnectListDomains
 *      virDomainGetName
 *      virConnectClose
 * Types :
 *      virConnectAuth
 *      virConnectCredential
 *      virConnectCredentialType
 */

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Libvirt;

namespace virConnectOpenAuth
{
    struct AuthData
    {
        public string user_name;
        public string password;
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Fill a structure to pass username and password to callbacks
            AuthData authData = new AuthData { password = tbPassword.Text, user_name = tbUsername.Text };

            // Fill a virConnectAuth structure
            virConnectAuth auth = new virConnectAuth
            {
                cbdata = authData,                  // The authData structure
                cb = AuthCallback,                  // the method called by callbacks
                CredTypes = new[]
                                {
                                    virConnectCredentialType.VIR_CRED_AUTHNAME,
                                    virConnectCredentialType.VIR_CRED_PASSPHRASE
                                }          // The list of credentials types
            };

            // Request the connection
            IntPtr conn = Connect.OpenAuth(tbURI.Text, ref auth, 0);

            if (conn != IntPtr.Zero)
            {
                // Get the number of defined (not running) domains
                int numOfDefinedDomains = Connect.NumOfDefinedDomains(conn);
                string[] definedDomainNames = new string[numOfDefinedDomains];
                if (Connect.ListDefinedDomains(conn, ref definedDomainNames, numOfDefinedDomains) == -1)
                {
                   MessageBox.Show("Unable to list defined domains", "List defined domains failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                // Add the domain names to the listbox
                foreach (string domainName in definedDomainNames)
                    lbDomains.Items.Add(domainName);

                // Get the number of running domains
                int numOfRunningDomain = Connect.NumOfDomains(conn);
                int[] runningDomainIDs = new int[numOfRunningDomain];
                if (Connect.ListDomains(conn, runningDomainIDs, numOfRunningDomain) == -1)
                {
                    MessageBox.Show("Unable to list running domains", "List running domains failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                // Add the domain names to the listbox
                foreach (int runningDomainID in runningDomainIDs)
                {
                    IntPtr domainPtr = Domain.LookupByID(conn, runningDomainID);
                    if (domainPtr == IntPtr.Zero)
                    {
                        MessageBox.Show("Unable to lookup domains by id", "Lookup domain failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                        return;
                    }
                    string domainName = Domain.GetName(domainPtr);
                    if (string.IsNullOrEmpty(domainName))
                    {
                        MessageBox.Show("Unable to get domain name", "Get domain name failed", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                        return;
                    }
                    lbDomains.Items.Add(domainName);
                }
                Connect.Close(conn);
            }
            else
            {
                MessageBox.Show("Unable to connect", "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int AuthCallback(IntPtr creds, uint ncred, IntPtr cbdata)
        {
            // Get the AuthData structure
            AuthData authData = (AuthData)Marshal.PtrToStructure(cbdata, typeof(AuthData));
            int offset = 0;
            int credIndex = 0;

            while (credIndex < ncred)
            {
                IntPtr currentCred = new IntPtr(creds.ToInt32() + offset);

                virConnectCredential cred = (virConnectCredential) Marshal.PtrToStructure(currentCred, typeof(virConnectCredential));
                offset += Marshal.SizeOf(cred);
                switch (cred.type)
                {
                    case virConnectCredentialType.VIR_CRED_AUTHNAME:
                        // Fill the user name
                        cred.Result = authData.user_name;
                        cred.resultlen = (uint)authData.user_name.Length;
                        break;
                    case virConnectCredentialType.VIR_CRED_PASSPHRASE:
                        // Fill the password
                        cred.Result = authData.password;
                        cred.resultlen = (uint)authData.password.Length;
                        break;
                    default:
                        return -1;
                }
                // Write structure to the unmanaged address
                Marshal.StructureToPtr(cred, currentCred, true);

                credIndex++;
            }
            return 0;
        }
    }
}
